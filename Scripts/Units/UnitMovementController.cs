using System.Collections.Generic; using Crownfall.Battle.Grid; using Crownfall.Core; using UnityEngine;
namespace Crownfall.Units {
public sealed class UnitMovementController : MonoBehaviour {
 Unit unit; BattleClock clock; List<GridCell> path; int pathIndex; GridCell reserved;
 public bool IsMoving=>path!=null&&pathIndex<path.Count;
 public void Initialize(Unit unit,BattleClock clock){this.unit=unit;this.clock=clock;}
 public bool MoveAlong(List<GridCell> newPath){Cancel();if(unit==null||unit.IsDead||newPath==null||newPath.Count==0)return false;path=newPath;pathIndex=0;return ReserveNext();}
 public void Tick(){if(!IsMoving||clock==null||clock.DeltaTime<=0||unit.IsDead)return;var next=path[pathIndex];unit.SetState(UnitState.Moving);unit.transform.position=Vector3.MoveTowards(unit.transform.position,next.WorldPosition,unit.Definition.moveSpeed*clock.DeltaTime);if((unit.transform.position-next.WorldPosition).sqrMagnitude<=.0001f){if(!unit.EnterCell(next)){Cancel();return;}reserved=null;pathIndex++;if(pathIndex>=path.Count){path=null;unit.SetState(UnitState.Idle);}else if(!ReserveNext())Cancel();}}
 bool ReserveNext(){var next=path[pathIndex];if(!next.TryReserve(unit)){path=null;return false;}reserved=next;return true;}
 public void Cancel(){if(reserved!=null)reserved.ClearReservation(unit);reserved=null;path=null;pathIndex=0;if(unit!=null&&!unit.IsDead)unit.SetState(UnitState.Idle);}
 void OnDisable(){Cancel();}
}}
