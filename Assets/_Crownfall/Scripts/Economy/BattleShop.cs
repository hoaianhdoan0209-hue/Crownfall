using System;
using System.Collections.Generic;
using Crownfall.Core;
using Crownfall.Preparation;

namespace Crownfall.Economy {
 [Serializable] public sealed class ShopOffer { public string HeroId; public int Price=4; public bool IsPurchased; public ShopOffer Clone()=>new ShopOffer{HeroId=HeroId,Price=Price,IsPurchased=IsPurchased}; }
 public sealed class BattleShop {
  public const int OfferCount=3; readonly BattleDeck deck; readonly BattleEconomy economy; readonly BenchController bench; readonly IRandomService rng;
  public readonly List<ShopOffer> Offers=new List<ShopOffer>(OfferCount); public bool IsLocked{get;private set;} public int PaidRerollsThisWave{get;private set;} public bool FreeRefreshConsumed{get;private set;}
  public int RerollCost => PaidRerollsThisWave>=5?2:1;
  public BattleShop(BattleDeck d,BattleEconomy e,BenchController b,IRandomService r){deck=d;economy=e;bench=b;rng=r;}
  public void BeginWave(){PaidRerollsThisWave=0; FreeRefreshConsumed=false; if(!IsLocked){RefreshInternal();FreeRefreshConsumed=true;}}
  public void ToggleLock()=>IsLocked=!IsLocked;
  public bool TryManualRefresh(){ if(!FreeRefreshConsumed){FreeRefreshConsumed=true;RefreshInternal();return true;} int cost=RerollCost;if(!economy.TrySpend(cost))return false;PaidRerollsThisWave++;RefreshInternal();return true; }
  public bool TryBuy(int index, Func<string,BattleHeroInstance> factory){ if(index<0||index>=Offers.Count||Offers[index].IsPurchased||bench.IsFull)return false;var o=Offers[index];if(!economy.TrySpend(o.Price))return false;var h=factory(o.HeroId);if(!bench.TryPlace(h)){economy.AddGold(o.Price);return false;}o.IsPurchased=true;return true; }
  public void Restore(List<ShopOffer> offers,bool locked,int paid,bool free){Offers.Clear();foreach(var o in offers)Offers.Add(o.Clone());IsLocked=locked;PaidRerollsThisWave=paid;FreeRefreshConsumed=free;}
  void RefreshInternal(){Offers.Clear();for(int i=0;i<OfferCount;i++)Offers.Add(new ShopOffer{HeroId=deck.HeroIds[rng.Range(0,deck.HeroIds.Count)],Price=4});}
 }
}
