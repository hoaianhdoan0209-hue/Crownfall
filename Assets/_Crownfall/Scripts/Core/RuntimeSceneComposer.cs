using System;
using Crownfall.Campaign;
using Crownfall.Battle;
using Crownfall.UI;
using Crownfall.Content;
using Crownfall.Progression;
using Crownfall.Preparation;
using System.Collections.Generic;
using Crownfall.Save;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Crownfall.Core
{
    /// <summary>
    /// Source-authored first-playable UI composition. It intentionally does not replace combat simulation;
    /// the Battle screen exposes the canonical stage/wave state while combat presentation is integrated.
    /// </summary>
    public sealed class RuntimeSceneComposer : MonoBehaviour
    {
        private const string RootName = "Crownfall_RuntimeUI";
        private Font _font;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy() => SceneManager.sceneLoaded -= OnSceneLoaded;

        private void Start()
        {
            var active = SceneManager.GetActiveScene();
            if (active.name == SceneIds.Bootstrap) RuntimeServices.Scenes.Load(SceneIds.MainMenu);
            else Compose(active);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => Compose(scene);

        private void Compose(Scene scene)
        {
            var old = GameObject.Find(RootName);
            if (old != null) Destroy(old);
            EnsureEventSystem();
            switch (scene.name)
            {
                case SceneIds.MainMenu: ComposeMainMenu(); break;
                case SceneIds.WorldMap: ComposeWorldMap(); break;
                case SceneIds.Battle: ComposeBattle(); break;
            }
        }

        private void ComposeMainMenu()
        {
            var root = CreateRoot("CROWNFALL");
            AddLabel(root, "Tactical Auto-Battler — Chapter 1", 24);
            AddButton(root, "NEW GAME", () => RuntimeServices.Flow.NewGame());
            var continueButton = AddButton(root, "CONTINUE", () => RuntimeServices.Flow.Continue());
            continueButton.interactable = RuntimeServices.Flow.CanContinue;
            AddLabel(root, "First Playable runtime harness — no ads / payments / premium currency", 16);
        }

        private void ComposeWorldMap()
        {
            EnsureLoadedSave();
            var save = RuntimeServices.Flow.CurrentSave;
            var root = CreateRoot("CHAPTER 1");
            AddLabel(root, "Select the currently available stage.", 18);
            foreach (var stageId in ChapterOneDefinition.MainPath)
            {
                bool completed = save.Campaign.CompletedStageIds.Contains(stageId);
                bool current = save.Campaign.CurrentStageId == stageId;
                string suffix = completed ? "  ✓" : current ? "  ← CURRENT" : "  🔒";
                var button = AddButton(root, FriendlyStageName(stageId) + suffix, () => RuntimeServices.Flow.SelectStage(stageId));
                button.interactable = current || completed;
            }
            AddLabel(root, "Heroes: " + string.Join(", ", save.Heroes.UnlockedHeroIds), 15);
            AddButton(root, "BACK TO MENU", () => RuntimeServices.Flow.ReturnToMenu());
        }

        private void ComposeBattle()
        {
            EnsureLoadedSave();
            string stageId = RuntimeServices.Flow.SelectedStageId;
            var root = CreateRoot("BATTLE PREPARATION");
            if (string.IsNullOrWhiteSpace(stageId)) { AddLabel(root, "No stage selected.", 20); AddButton(root, "RETURN TO MAP", () => RuntimeServices.Flow.ReturnToMap()); return; }
            var encounter = FirstPlayableContentCatalog.FindEncounter(stageId);
            if (encounter == null) { AddLabel(root, "Unknown encounter: " + stageId, 20); AddButton(root, "RETURN TO MAP", () => RuntimeServices.Flow.ReturnToMap()); return; }

            var available = new List<string>();
            foreach (var id in RuntimeServices.Flow.CurrentSave.Heroes.UnlockedHeroIds) if (FirstPlayableContentCatalog.FindHero(id) != null && !available.Contains(id)) available.Add(id);
            if (!string.IsNullOrWhiteSpace(encounter.GuestHeroId) && FirstPlayableContentCatalog.FindHero(encounter.GuestHeroId) != null && !available.Contains(encounter.GuestHeroId)) available.Add(encounter.GuestHeroId);
            var prep = new RuntimePreparationSession(encounter, available);
            var formation = new RuntimeFormationPlan(encounter.BoardCapacity);
            formation.AutoFill(available);

            AddLabel(root, FriendlyStageName(stageId), 24);
            AddLabel(root, $"Board cap: {encounter.BoardCapacity}   Waves: {encounter.Waves.Length}" + (encounter.Boss ? "   BOSS" : ""), 17);
            var status = AddLabel(root, "", 15);
            var shop = AddLabel(root, "", 14);
            var bench = AddLabel(root, "", 14);
            var heroButtons = new Dictionary<string, Button>(); Button startButton = null;
            Action refresh = null;
            refresh = () => {
                status.text = $"Gold {prep.Preparation.Economy.Gold}   Formation {formation.Count}/{formation.Capacity}" + (formation.CanStartCombat ? " — READY" : "");
                var offers=new List<string>(); for(int i=0;i<prep.Shop.Offers.Count;i++){var o=prep.Shop.Offers[i];var h=FirstPlayableContentCatalog.FindHero(o.HeroId);offers.Add($"{i+1}:{(h!=null?h.Name:o.HeroId)} {(o.IsPurchased?"SOLD":o.Price+"g")}");}
                shop.text="Shop " + (prep.Shop.IsLocked?"[LOCKED] ":"") + string.Join(" | ",offers) + $"   Refresh {prep.Shop.RerollCost}g";
                var slots=new List<string>();for(int i=0;i<prep.Preparation.Bench.Size;i++){var h=prep.BenchAt(i);slots.Add(h==null?$"{i+1}:—":$"{i+1}:{prep.HeroName(h)} ★{h.StarLevel}");}bench.text="Bench: "+string.Join(" | ",slots);
                foreach(var pair in heroButtons){var r=FirstPlayableContentCatalog.FindHero(pair.Key);var t=pair.Value.GetComponentInChildren<Text>();if(t!=null)t.text=(formation.Contains(pair.Key)?"[DEPLOYED] ":"[BENCH] ")+(r!=null?r.Name:pair.Key);}
                if(startButton!=null)startButton.interactable=formation.CanStartCombat;
            };
            prep.Changed += refresh;
            foreach(var id in available){string heroId=id;heroButtons[heroId]=AddButton(root,heroId,()=>{formation.TryToggle(heroId);refresh();});}
            AddButton(root,"QUICK SUMMON — 3G",()=>prep.TryQuickSummon());
            for(int i=0;i<3;i++){int offer=i;AddButton(root,"BUY SHOP SLOT "+(i+1),()=>prep.TryBuy(offer));}
            AddButton(root,"REFRESH SHOP",()=>prep.TryRefresh()); AddButton(root,"LOCK / UNLOCK SHOP",()=>prep.ToggleLock());
            AddButton(root,"MERGE BENCH 1 + 2",()=>prep.TryMergeBench(0,1)); AddButton(root,"SELL BENCH 1",()=>prep.TrySellBench(0));
            startButton=AddButton(root,"START COMBAT",()=>{
                if(!formation.CanStartCombat)return;
                foreach(var b in heroButtons.Values)b.gameObject.SetActive(false); startButton.gameObject.SetActive(false); status.text="Starting combat...";
                var continueButton=AddButton(root,"CONTINUE",null);continueButton.gameObject.SetActive(false);
                var simulation=new GameObject("Crownfall_BattleSession");var session=simulation.AddComponent<RuntimeBattleSession>();var hud=simulation.AddComponent<RuntimeBattleHud>();session.Initialize(stageId,formation.Slots);hud.Bind(session,status,continueButton);
            });
            AddButton(root,"RETURN TO MAP",()=>RuntimeServices.Flow.ReturnToMap()); refresh();
        }

        private void EnsureLoadedSave()
        {
            if (RuntimeServices.Flow.CurrentSave == null) RuntimeServices.Flow.Continue();
        }

        private static string DescribeWave(WaveRecord wave)
        {
            if (wave == null || wave.Spawns == null) return "empty";
            var parts = new string[wave.Spawns.Length];
            for (int i = 0; i < wave.Spawns.Length; i++)
            {
                var enemy = FirstPlayableContentCatalog.FindEnemy(wave.Spawns[i].EnemyId);
                parts[i] = enemy != null ? enemy.Name : wave.Spawns[i].EnemyId;
            }
            return string.Join(" + ", parts);
        }

        private static string FriendlyStageName(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return "Unknown Stage";
            return id.Replace("stage_", "").Replace("_", " ").ToUpperInvariant();
        }

        private Transform CreateRoot(string title)
        {
            var canvasGo = new GameObject(RootName, typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            var canvas = canvasGo.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvasGo.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1280, 720);

            var panel = new GameObject("Panel", typeof(RectTransform), typeof(Image), typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
            panel.transform.SetParent(canvasGo.transform, false);
            var rect = (RectTransform)panel.transform;
            rect.anchorMin = new Vector2(.16f, .08f); rect.anchorMax = new Vector2(.84f, .92f);
            rect.offsetMin = rect.offsetMax = Vector2.zero;
            panel.GetComponent<Image>().color = new Color(.055f, .06f, .08f, .96f);
            var layout = panel.GetComponent<VerticalLayoutGroup>();
            layout.padding = new RectOffset(28, 28, 24, 24); layout.spacing = 12;
            layout.childAlignment = TextAnchor.UpperCenter; layout.childControlHeight = true; layout.childForceExpandHeight = false;
            panel.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            AddLabel(panel.transform, title, 34);
            return panel.transform;
        }

        private Text AddLabel(Transform parent, string value, int size)
        {
            var go = new GameObject("Label", typeof(RectTransform), typeof(Text), typeof(LayoutElement));
            go.transform.SetParent(parent, false);
            var text = go.GetComponent<Text>();
            text.font = _font; text.text = value; text.fontSize = size; text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.white; text.horizontalOverflow = HorizontalWrapMode.Wrap; text.verticalOverflow = VerticalWrapMode.Overflow;
            go.GetComponent<LayoutElement>().preferredHeight = Math.Max(32, size * 2.2f);
            return text;
        }

        private Button AddButton(Transform parent, string label, Action action)
        {
            var go = new GameObject("Button_" + label, typeof(RectTransform), typeof(Image), typeof(Button), typeof(LayoutElement));
            go.transform.SetParent(parent, false);
            go.GetComponent<Image>().color = new Color(.18f, .20f, .25f, 1f);
            var button = go.GetComponent<Button>();
            if (action != null) button.onClick.AddListener(() => action());
            var le = go.GetComponent<LayoutElement>(); le.preferredHeight = 52; le.minHeight = 48;
            var textGo = new GameObject("Text", typeof(RectTransform), typeof(Text));
            textGo.transform.SetParent(go.transform, false);
            var rt = (RectTransform)textGo.transform; rt.anchorMin = Vector2.zero; rt.anchorMax = Vector2.one; rt.offsetMin = rt.offsetMax = Vector2.zero;
            var text = textGo.GetComponent<Text>(); text.font = _font; text.text = label; text.fontSize = 18; text.alignment = TextAnchor.MiddleCenter; text.color = Color.white;
            return button;
        }

        private static void EnsureEventSystem()
        {
            if (FindFirstObjectByType<EventSystem>() != null) return;
            var go = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
            DontDestroyOnLoad(go);
        }
    }
}
