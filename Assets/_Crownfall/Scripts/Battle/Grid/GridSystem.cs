using System.Collections.Generic; using Crownfall.Core; using UnityEngine;
namespace Crownfall.Battle.Grid {
public sealed class GridSystem : MonoBehaviour {
 [SerializeField] GameRulesDefinition rules; readonly Dictionary<GridCoordinate,GridCell> cells=new();
 static readonly GridCoordinate[] D={new(1,0),new(-1,0),new(0,1),new(0,-1)};
 public IEnumerable<GridCell> Cells=>cells.Values;
 public void Initialize(GameRulesDefinition r){rules=r; BuildGrid();}
 void Awake(){ if(rules!=null) BuildGrid(); }
 public void BuildGrid(){ cells.Clear(); if(rules==null)return; for(int y=0;y<rules.gridHeight;y++) for(int x=0;x<rules.gridWidth;x++){var c=new GridCoordinate(x,y); cells[c]=new GridCell(c, CoordinateToWorld(c));}}
 public bool IsValid(GridCoordinate c)=>rules!=null&&c.X>=0&&c.Y>=0&&c.X<rules.gridWidth&&c.Y<rules.gridHeight;
 public GridCell GetCell(GridCoordinate c)=>cells.TryGetValue(c,out var cell)?cell:null;
 public Vector3 CoordinateToWorld(GridCoordinate c)=>new(c.X,c.Y,0);
 public IEnumerable<GridCell> GetNeighbors(GridCell cell){foreach(var d in D){var n=GetCell(new GridCoordinate(cell.Coordinate.X+d.X,cell.Coordinate.Y+d.Y)); if(n!=null) yield return n;}}
}}
