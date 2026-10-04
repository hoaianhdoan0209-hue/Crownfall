using Crownfall.Core; using Crownfall.Economy; using Crownfall.Units;
namespace Crownfall.Preparation {
public sealed class QuickSummonSystem {
 readonly BattleDeck deck; readonly BattleEconomy economy; readonly BenchController bench; readonly IRandomService rng; readonly int cost;
 public QuickSummonSystem(BattleDeck deck,BattleEconomy economy,BenchController bench,IRandomService rng,int cost=3){this.deck=deck;this.economy=economy;this.bench=bench;this.rng=rng;this.cost=cost;}
 public BattleHeroInstance TrySummon(){if(deck.Heroes.Count==0||!bench.HasRoom||!economy.CanAfford(cost))return null;UnitDefinition pick=deck.Heroes[rng.Range(0,deck.Heroes.Count)];var hero=new BattleHeroInstance(pick);int slot=bench.TryPlaceFirst(hero);if(slot<0)return null;if(!economy.TrySpend(cost)){bench.Remove(slot);return null;}return hero;}
}}
