using Crownfall.Core; using Crownfall.Economy;
namespace Crownfall.Preparation {
public sealed class PreparationController {
 public BenchController Bench{get;} public BoardState Board{get;} public BattleEconomy Economy{get;} public BattleDeck Deck{get;} public MergeService Merges{get;}
 public PreparationController(GameRulesDefinition rules,int startingGold){Bench=new BenchController(rules.benchSize);Board=new BoardState(rules.baseBoardCapacity);Economy=new BattleEconomy(startingGold,rules.goldCap);Deck=new BattleDeck(rules.maxCoreDeckSize);Merges=new MergeService(rules.maxStar);}
 public bool TryDeployFromBench(int benchIndex,GridCoordinate cell){var h=Bench.Remove(benchIndex);if(h==null)return false;if(Board.TryPlace(h,cell))return true;Bench.TryPlace(h,benchIndex);return false;}
 public bool TryReturnToBench(GridCoordinate cell){if(!Bench.HasRoom)return false;var h=Board.Remove(cell);if(h==null)return false;if(Bench.TryPlaceFirst(h)>=0)return true;Board.TryPlace(h,cell);return false;}
 public bool TrySwapBoard(GridCoordinate a,GridCoordinate b)=>Board.Swap(a,b);
 public bool TryMergeBench(int source,int target){var a=Bench.Get(source);var b=Bench.Get(target);var result=Merges.Merge(a,b);if(result==null)return false;Bench.Remove(source);Bench.Remove(target);if(!Bench.TryPlace(result,target)){Bench.TryPlace(a,source);Bench.TryPlace(b,target);return false;}return true;}
 public bool TryMergeBoard(GridCoordinate source,GridCoordinate target){var a=Board.Get(source);var b=Board.Get(target);var result=Merges.Merge(a,b);if(result==null)return false;Board.Remove(source);Board.Remove(target);if(!Board.TryPlace(result,target)){Board.TryPlace(a,source);Board.TryPlace(b,target);return false;}return true;}
 public bool TrySellBench(int index){var h=Bench.Remove(index);if(h==null)return false;Economy.AddGold(SellService.ValueFor(h.StarLevel));return true;}
 public bool TrySellBoard(GridCoordinate cell){var h=Board.Remove(cell);if(h==null)return false;Economy.AddGold(SellService.ValueFor(h.StarLevel));return true;}
 public bool CanStartCombat=>Board.Count>0;
}}
