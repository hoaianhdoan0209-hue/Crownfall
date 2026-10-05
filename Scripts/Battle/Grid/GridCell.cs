using Crownfall.Core; using Crownfall.Units; using UnityEngine;
namespace Crownfall.Battle.Grid {
public sealed class GridCell {
 public GridCoordinate Coordinate { get; } public Vector3 WorldPosition { get; }
 public Unit Occupant { get; private set; } public Unit ReservedBy { get; private set; } public bool IsBlocked { get; private set; }
 public bool IsWalkable(Unit requester=null) => !IsBlocked && (Occupant==null || Occupant==requester) && (ReservedBy==null || ReservedBy==requester);
 public GridCell(GridCoordinate c, Vector3 p){Coordinate=c;WorldPosition=p;}
 public bool TryOccupy(Unit u){ if(u==null || (Occupant!=null && Occupant!=u)) return false; Occupant=u; if(ReservedBy==u) ReservedBy=null; return true; }
 public void Leave(Unit u){ if(Occupant==u) Occupant=null; }
 public bool TryReserve(Unit u){ if(u==null || IsBlocked || (Occupant!=null && Occupant!=u) || (ReservedBy!=null && ReservedBy!=u)) return false; ReservedBy=u; return true; }
 public void ClearReservation(Unit u){ if(ReservedBy==u) ReservedBy=null; }
 public void SetBlocked(bool value){IsBlocked=value;}
}}
