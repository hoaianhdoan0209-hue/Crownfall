using Crownfall.Battle.Combat; using Crownfall.Battle.Grid; using Crownfall.Core; using UnityEngine;
namespace Crownfall.Units {
public sealed class UnitCombatController : MonoBehaviour {
 Unit unit; DamageSystem damage; BattleClock clock; float cooldown;
 public void Initialize(Unit unit,DamageSystem damage,BattleClock clock){this.unit=unit;this.damage=damage;this.clock=clock;cooldown=0;}
 public void Tick(){if(clock==null||clock.DeltaTime<=0)return;cooldown=Mathf.Max(0,cooldown-clock.DeltaTime);}
 public bool IsInRange(Unit target){return unit?.CurrentCell!=null&&target?.CurrentCell!=null&&GridMath.Manhattan(unit.CurrentCell.Coordinate,target.CurrentCell.Coordinate)<=unit.Definition.attackRange;}
 public bool TryBasicAttack(Unit target){if(unit==null||target==null||unit.IsDead||target.IsDead||!unit.CanBasicAttack||cooldown>0||!IsInRange(target))return false;unit.SetState(UnitState.Attacking);var result=damage.Apply(unit,target,unit.Definition.attack,DamageType.Physical,true);if(result.FinalDamage>0)unit.Energy?.Gain(unit.Definition.energyPerBasicAttack);cooldown=Mathf.Max(.05f,unit.Definition.attackInterval);if(!unit.IsDead)unit.SetState(UnitState.Idle);return true;}
}}
