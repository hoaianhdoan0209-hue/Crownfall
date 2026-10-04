using System.Collections.Generic; using Crownfall.Battle.Grid; using Crownfall.Units;
namespace Crownfall.AI {
public sealed class TargetingSystem {
 readonly GridSystem grid; public TargetingSystem(GridSystem g){grid=g;}
 public Unit FindNearestReachable(Unit requester,IReadOnlyList<Unit> units,Pathfinder pathfinder){Unit best=null;int bestD=int.MaxValue;foreach(var u in units){if(u==null||u.IsDead||u.Team==requester.Team||u.CurrentCell==null)continue;int d=GridMath.Manhattan(requester.CurrentCell.Coordinate,u.CurrentCell.Coordinate);if(d>bestD)continue;if(d>requester.Definition.attackRange&&!HasReachableAttackCell(requester,u,pathfinder))continue;if(d<bestD){best=u;bestD=d;}}return best;}
 bool HasReachableAttackCell(Unit a,Unit target,Pathfinder p){foreach(var c in grid.GetNeighbors(target.CurrentCell)){if(!c.IsWalkable(a))continue;var path=p.FindPath(a.CurrentCell,c,a);if(path!=null)return true;}return false;}
}
}
