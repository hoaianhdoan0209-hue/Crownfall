using System;
using System.Collections.Generic;
using Crownfall.Battle.Grid;
using Crownfall.Campaign;
using Crownfall.Content;
using Crownfall.Core;
using Crownfall.Preparation;
using Crownfall.Bosses;
using Crownfall.Units;
using UnityEngine;

namespace Crownfall.Battle
{
    /// <summary>Runtime bridge from the canonical encounter records into the existing combat simulation.</summary>
    public sealed class RuntimeBattleSession : MonoBehaviour
    {
        public BattleController Battle { get; private set; }
        public EncounterRecord Encounter { get; private set; }
        public int WaveIndex { get; private set; } = -1;
        public bool IsFinished { get; private set; }
        public bool PlayerWon { get; private set; }
        public event Action Changed;

        private UnitFactory _units;
        private RuntimeContentFactory _content;
        private readonly List<Unit> _waveUnits = new();
        private RuntimeFormationSlot[] _formation;
        private GorrukBossController _gorruk;

        public void Initialize(string stageId, IEnumerable<string> unlockedHeroIds)
        {
            Encounter = FirstPlayableContentCatalog.FindEncounter(stageId) ?? throw new InvalidOperationException("Unknown encounter: " + stageId);
            _content = new RuntimeContentFactory();
            var ids = new List<string>();
            if (unlockedHeroIds != null) foreach (var id in unlockedHeroIds) if (_content.Hero(id) != null && !ids.Contains(id)) ids.Add(id);
            if (!string.IsNullOrWhiteSpace(Encounter.GuestHeroId) && !ids.Contains(Encounter.GuestHeroId)) ids.Add(Encounter.GuestHeroId);
            var fallback = new RuntimeFormationPlan(Encounter.BoardCapacity);
            fallback.AutoFill(ids);
            Initialize(stageId, fallback.Slots);
        }

        public void Initialize(string stageId, IEnumerable<RuntimeFormationSlot> formation)
        {
            Encounter = FirstPlayableContentCatalog.FindEncounter(stageId) ?? throw new InvalidOperationException("Unknown encounter: " + stageId);
            _content = new RuntimeContentFactory();
            var valid = new List<RuntimeFormationSlot>();
            if (formation != null)
            {
                foreach (var slot in formation)
                {
                    if (slot == null || _content.Hero(slot.HeroId) == null || valid.Count >= Encounter.BoardCapacity) continue;
                    valid.Add(slot);
                }
            }
            _formation = valid.ToArray();
            BuildSimulation();
            SpawnNextWave();
        }

        private void BuildSimulation()
        {
            var rules = ScriptableObject.CreateInstance<GameRulesDefinition>(); rules.hideFlags = HideFlags.DontSave;
            CreateRuntimeArena(rules);
            var gridGo = new GameObject("BattleGrid"); gridGo.transform.SetParent(transform, false);
            var grid = gridGo.AddComponent<GridSystem>(); grid.Initialize(rules);
            Battle = gameObject.AddComponent<BattleController>();
            var field = typeof(BattleController).GetField("grid", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            field?.SetValue(Battle, grid);
            Battle.Initialize(new SeededRandomService(StableSeed(Encounter.StageId)));
            _units = new UnitFactory(Battle, Battle.Clock);
        }

        private void CreateRuntimeArena(GameRulesDefinition rules)
        {
            var cam = Camera.main;
            if (cam == null)
            {
                var cameraGo = new GameObject("BattleCamera", typeof(Camera));
                cameraGo.tag = "MainCamera";
                cam = cameraGo.GetComponent<Camera>();
            }
            cam.orthographic = true;
            cam.orthographicSize = 4.2f;
            cam.transform.position = new Vector3((rules.gridWidth - 1) * .5f, (rules.gridHeight - 1) * .5f, -10f);
            cam.backgroundColor = new Color(.075f, .095f, .065f, 1f);

            var tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            tex.SetPixel(0, 0, Color.white); tex.Apply();
            var pixel = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(.5f, .5f), 1f);

            for (int y = -1; y <= rules.gridHeight; y++)
            for (int x = -1; x <= rules.gridWidth; x++)
            {
                var tile = new GameObject($"Arena_{x}_{y}", typeof(SpriteRenderer));
                tile.transform.SetParent(transform, false);
                tile.transform.position = new Vector3(x, y, 1f);
                tile.transform.localScale = Vector3.one * .96f;
                var sr = tile.GetComponent<SpriteRenderer>();
                sr.sprite = pixel; sr.sortingOrder = -20;
                bool border = x < 0 || y < 0 || x >= rules.gridWidth || y >= rules.gridHeight;
                if (border)
                {
                    float n = ((x * 17 + y * 31) & 3) * .012f;
                    sr.color = new Color(.09f + n, .17f + n, .075f, 1f);
                    tile.transform.localScale = new Vector3(1.18f, 1.18f, 1f);
                }
                else
                {
                    bool alt = ((x + y) & 1) == 0;
                    sr.color = alt ? new Color(.28f, .31f, .20f, 1f) : new Color(.245f, .285f, .18f, 1f);
                }
            }
        }

        private void Update()
        {
            if (IsFinished || Battle == null) return;
            _gorruk?.Tick();
            if (Battle.State == BattleState.Victory) ResolveWaveVictory();
            else if (Battle.State == BattleState.Defeat) { IsFinished = true; PlayerWon = false; Changed?.Invoke(); }
        }

        private void ResolveWaveVictory()
        {
            if (WaveIndex + 1 >= Encounter.Waves.Length) { IsFinished = true; PlayerWon = true; Changed?.Invoke(); return; }
            SpawnNextWave();
        }

        private void SpawnNextWave()
        {
            ClearWaveObjects();
            _gorruk = null;
            WaveIndex++;
            SpawnHeroes();
            SpawnEnemies(Encounter.Waves[WaveIndex]);
            Battle.StartCombat();
            Changed?.Invoke();
        }

        private void SpawnHeroes()
        {
            if (_formation == null || _formation.Length == 0) return;
            for (int i = 0; i < _formation.Length; i++)
            {
                var definition = _content.Hero(_formation[i].HeroId);
                var requested = Battle.Grid.GetCell(_formation[i].Cell);
                var cell = requested != null && requested.IsWalkable() ? requested : FindFreeCell(i % 7, i / 7);
                var unit = _units.Spawn(definition, TeamId.Player, cell, transform);
                if (unit != null) _waveUnits.Add(unit);
            }
        }

        private void SpawnEnemies(WaveRecord wave)
        {
            if (wave?.Spawns == null) return;
            foreach (var spawn in wave.Spawns)
            {
                var definition = _content.Enemy(spawn.EnemyId);
                var cell = Battle.Grid.GetCell(new GridCoordinate(spawn.X, spawn.Y));
                if (cell == null || !cell.IsWalkable()) cell = FindAnyFreeCell(true);
                var unit = _units.Spawn(definition, TeamId.Enemy, cell, transform);
                if (unit != null)
                {
                    _waveUnits.Add(unit);
                    if (spawn.EnemyId == CanonicalIds.Gorruk) AttachGorruk(unit);
                }
            }
        }


        private void AttachGorruk(Unit boss)
        {
            _gorruk = new GorrukBossController(boss, Battle.Clock);
            _gorruk.ReinforcementsRequested += () =>
            {
                SpawnReinforcement(CanonicalIds.Goblin, 1, 5); SpawnReinforcement(CanonicalIds.Goblin, 5, 5);
                SpawnReinforcement(CanonicalIds.Archer, 1, 4); SpawnReinforcement(CanonicalIds.Archer, 5, 4);
                Changed?.Invoke();
            };
            _gorruk.Enraged += () => Changed?.Invoke();
        }

        private void SpawnReinforcement(string enemyId, int x, int y)
        {
            var definition = _content.Enemy(enemyId);
            var cell = Battle.Grid.GetCell(new GridCoordinate(x, y));
            if (cell == null || !cell.IsWalkable()) cell = FindAnyFreeCell(true);
            var unit = _units.Spawn(definition, TeamId.Enemy, cell, transform);
            if (unit != null) _waveUnits.Add(unit);
        }

        public bool BossEnraged => _gorruk != null && _gorruk.IsEnraged;
        public bool BossReinforcementsTriggered => _gorruk != null && _gorruk.ReinforcementPhaseTriggered;
        private GridCell FindFreeCell(int preferredX, int preferredY)
        {
            var preferred = Battle.Grid.GetCell(new GridCoordinate(preferredX, preferredY));
            return preferred != null && preferred.IsWalkable() ? preferred : FindAnyFreeCell(false);
        }

        private GridCell FindAnyFreeCell(bool enemySide)
        {
            GridCell fallback = null;
            foreach (var cell in Battle.Grid.Cells)
            {
                if (!cell.IsWalkable()) continue;
                fallback ??= cell;
                if (enemySide ? cell.Coordinate.Y >= 3 : cell.Coordinate.Y < 3) return cell;
            }
            return fallback;
        }

        private void ClearWaveObjects()
        {
            for (int i = 0; i < _waveUnits.Count; i++) if (_waveUnits[i] != null) Destroy(_waveUnits[i].gameObject);
            _waveUnits.Clear();
            if (Battle != null) Battle.PrepareNextWave();
        }

        public int LivingPlayers => CountLiving(TeamId.Player);
        public int LivingEnemies => CountLiving(TeamId.Enemy);
        private int CountLiving(TeamId team) { int n=0; foreach(var u in Battle.Units) if(u!=null&&!u.IsDead&&u.Team==team)n++; return n; }
        private static int StableSeed(string value) { unchecked { int h=17; if(value!=null) foreach(char c in value) h=h*31+c; return h; } }
    }
}
