using Crownfall.Units;
namespace Crownfall.Battle.Grid {
public sealed class AttackPositionResolver {
 readonly GridSystem grid; readonly Pathfinder pathfinder;
 public AttackPositionResolver(GridSystem grid, Pathfinder pathfinder){this.grid=grid;this.pathfinder=pathfinder;}
 public GridCell FindDestination(Unit attacker, Unit target){
  if(attacker?.CurrentCell==null||target?.CurrentCell==null)return null;
  int range=attacker.Definition.attackRange;
  GridCell best=null; int bestLength=int.MaxValue;
  foreach(var cell in grid.Cells){
   if(!cell.IsWalkable(attacker))continue;
   int targetDistance=GridMath.Manhattan(cell.Coordinate,target.CurrentCell.Coordinate);
   if(targetDistance>range||targetDistance==0)continue;
   var path=pathfinder.FindPath(attacker.CurrentCell,cell,attacker);
   if(path==null)continue;
   if(path.Count<bestLength){best=cell;bestLength=path.Count;}
  }
  return best;
 }
}}
