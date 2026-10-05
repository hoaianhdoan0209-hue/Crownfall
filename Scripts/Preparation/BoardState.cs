using System.Collections.Generic; using Crownfall.Core;
namespace Crownfall.Preparation {
public sealed class BoardState {
 readonly Dictionary<GridCoordinate,BattleHeroInstance> byCell=new(); readonly Dictionary<BattleHeroInstance,GridCoordinate> byHero=new(); public int Capacity{get;private set;} public int Count=>byHero.Count;
 public BoardState(int capacity=6){Capacity=capacity;} public void SetCapacity(int value){Capacity=value;}
 public BattleHeroInstance Get(GridCoordinate c)=>byCell.TryGetValue(c,out var h)?h:null; public bool Contains(BattleHeroInstance h)=>h!=null&&byHero.ContainsKey(h);
 public bool TryGetCoordinate(BattleHeroInstance h,out GridCoordinate c)=>byHero.TryGetValue(h,out c);
 public bool TryPlace(BattleHeroInstance h,GridCoordinate c){if(h==null||Count>=Capacity||Contains(h)||byCell.ContainsKey(c))return false;byCell[c]=h;byHero[h]=c;return true;}
 public BattleHeroInstance Remove(GridCoordinate c){if(!byCell.TryGetValue(c,out var h))return null;byCell.Remove(c);byHero.Remove(h);return h;}
 public bool Move(GridCoordinate from,GridCoordinate to){if(!byCell.TryGetValue(from,out var h)||byCell.ContainsKey(to))return false;byCell.Remove(from);byCell[to]=h;byHero[h]=to;return true;}
 public bool Swap(GridCoordinate a,GridCoordinate b){if(!byCell.TryGetValue(a,out var ha)||!byCell.TryGetValue(b,out var hb))return false;byCell[a]=hb;byCell[b]=ha;byHero[ha]=b;byHero[hb]=a;return true;}
 public IEnumerable<KeyValuePair<GridCoordinate,BattleHeroInstance>> Enumerate()=>byCell;
}}
