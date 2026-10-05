using System.Collections.Generic; using Crownfall.Units;
namespace Crownfall.Battle.Grid {
public sealed class Pathfinder {
 readonly GridSystem grid; public Pathfinder(GridSystem g){grid=g;}
 public List<GridCell> FindPath(GridCell start, GridCell goal, Unit mover){
  if(start==null||goal==null||start==goal) return new List<GridCell>();
  var open=new List<GridCell>{start}; var came=new Dictionary<GridCell,GridCell>(); var g=new Dictionary<GridCell,int>{{start,0}};
  while(open.Count>0){int bi=0,bf=int.MaxValue; for(int i=0;i<open.Count;i++){int f=g[open[i]]+GridMath.Manhattan(open[i].Coordinate,goal.Coordinate); if(f<bf){bf=f;bi=i;}}
   var cur=open[bi]; open.RemoveAt(bi); if(cur==goal) return Reconstruct(came,cur,start);
   foreach(var n in grid.GetNeighbors(cur)){if(n!=goal&&!n.IsWalkable(mover))continue; int ng=g[cur]+1; if(!g.TryGetValue(n,out var old)||ng<old){came[n]=cur;g[n]=ng;if(!open.Contains(n))open.Add(n);}}
  } return null;
 }
 static List<GridCell> Reconstruct(Dictionary<GridCell,GridCell> came,GridCell cur,GridCell start){var p=new List<GridCell>(); while(cur!=start){p.Add(cur);cur=came[cur];}p.Reverse();return p;}
}}
