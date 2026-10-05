using Crownfall.Battle; using Crownfall.Battle.Grid; using Crownfall.Core; using UnityEngine;
namespace Crownfall.Units {
public sealed class UnitFactory {
 readonly BattleController battle; readonly BattleClock clock;
 public UnitFactory(BattleController battle,BattleClock clock){this.battle=battle;this.clock=clock;}
 public Unit Spawn(UnitDefinition definition,TeamId team,GridCell cell,Transform parent=null){if(definition==null||cell==null||!cell.IsWalkable())return null;GameObject go=definition.prefab!=null?Object.Instantiate(definition.prefab,parent):new GameObject(definition.displayName??definition.unitId);var unit=go.GetComponent<Unit>()??go.AddComponent<Unit>();unit.Initialize(definition,team);if(go.GetComponentInChildren<Renderer>()==null){var visual=go.AddComponent<RuntimeUnitVisual>();visual.Bind(unit);}if(!unit.SetInitialCell(cell)){Object.Destroy(go);return null;}var brain=go.GetComponent<Crownfall.AI.UnitBrain>()??go.AddComponent<Crownfall.AI.UnitBrain>();battle.Register(unit);brain.Initialize(unit,battle,clock);battle.RegisterBrain(unit,brain);return unit;}
}}
