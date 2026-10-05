using System; using Crownfall.Battle; using Crownfall.Battle.Grid; using Crownfall.Core; using Crownfall.Units;
namespace Crownfall.Campaign {
public sealed class WaveManager {
 readonly BattleController battle; readonly UnitFactory factory; public int WaveIndex{get;private set;}=-1; public event Action<int> WaveSpawned;
 public WaveManager(BattleController battle,UnitFactory factory){this.battle=battle;this.factory=factory;}
 public bool Spawn(WaveDefinition wave){if(wave==null)return false;WaveIndex++;foreach(var e in wave.enemies){if(e?.enemy?.unit==null)continue;GridCell cell=battle.Grid.GetCell(e.coordinate);if(cell!=null)factory.Spawn(e.enemy.unit,TeamId.Enemy,cell);}WaveSpawned?.Invoke(WaveIndex);return true;}
}}
