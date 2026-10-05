using System;
using UnityEngine;
namespace Crownfall.Economy {
public sealed class BattleEconomy {
 public int Gold{get;private set;} public int Cap{get;} public event Action<int> GoldChanged;
 public BattleEconomy(int startingGold,int cap=99){Cap=Mathf.Max(0,cap);Gold=Mathf.Clamp(startingGold,0,Cap);}
 public bool CanAfford(int amount)=>amount>=0&&Gold>=amount;
 public bool TrySpend(int amount){if(!CanAfford(amount))return false;Gold-=amount;GoldChanged?.Invoke(Gold);return true;}
 public int AddGold(int amount){if(amount<=0)return 0;int before=Gold;Gold=Mathf.Clamp(Gold+amount,0,Cap);GoldChanged?.Invoke(Gold);return Gold-before;}
 public void Restore(int gold){Gold=Mathf.Clamp(gold,0,Cap);GoldChanged?.Invoke(Gold);}
}}
