using Crownfall.Battle; using Crownfall.Battle.Grid; using Crownfall.Core; using Crownfall.Units; using UnityEngine;
namespace Crownfall.AI {
public sealed class UnitBrain : MonoBehaviour {
 Unit unit; BattleController battle; BattleClock clock; UnitMovementController movement; UnitCombatController combat; Crownfall.Skills.SkillController skill; AttackPositionResolver positions;
 float thinkTimer; int failedPaths; const float ThinkInterval=.15f;
 public void Initialize(Unit unit,BattleController battle,BattleClock clock){this.unit=unit;this.battle=battle;this.clock=clock;movement=GetComponent<UnitMovementController>()??gameObject.AddComponent<UnitMovementController>();combat=GetComponent<UnitCombatController>()??gameObject.AddComponent<UnitCombatController>();movement.Initialize(unit,clock);combat.Initialize(unit,battle.Damage,clock);skill=GetComponent<Crownfall.Skills.SkillController>()??gameObject.AddComponent<Crownfall.Skills.SkillController>();skill.Initialize(unit,battle,clock);positions=new AttackPositionResolver(battle.Grid,battle.Pathfinder);}
 public void Tick(){if(unit==null||unit.IsDead||battle.State!=BattleState.Combat)return;combat.Tick();movement.Tick();skill.Tick();thinkTimer-=clock.DeltaTime;if(thinkTimer>0)return;thinkTimer=ThinkInterval;if(unit.CurrentTarget==null||unit.CurrentTarget.IsDead)Acquire();if(unit.CurrentTarget==null)return;if(skill.Busy)return;if(combat.IsInRange(unit.CurrentTarget)){movement.Cancel();combat.TryBasicAttack(unit.CurrentTarget);failedPaths=0;return;}if(movement.IsMoving)return;var destination=positions.FindDestination(unit,unit.CurrentTarget);if(destination==null){OnPathFailure();return;}var path=battle.Pathfinder.FindPath(unit.CurrentCell,destination,unit);if(path==null||path.Count==0){OnPathFailure();return;}if(!movement.MoveAlong(path))OnPathFailure();else failedPaths=0;}
 void Acquire(){unit.CurrentTarget=battle.Targeting.FindNearestReachable(unit,battle.Units,battle.Pathfinder);}
 void OnPathFailure(){failedPaths++;if(failedPaths>=3){unit.CurrentTarget=null;failedPaths=0;}}
 public void Stop(){movement?.Cancel();}
}}
