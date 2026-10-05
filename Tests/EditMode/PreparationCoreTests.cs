using Crownfall.Core; using Crownfall.Economy; using Crownfall.Preparation; using Crownfall.Units; using NUnit.Framework; using UnityEngine;
namespace Crownfall.Tests {
public sealed class PreparationCoreTests {
 UnitDefinition Hero(string id){var h=ScriptableObject.CreateInstance<UnitDefinition>();h.unitId=id;return h;}
 [Test] public void Bench_Is_Canonical_Six(){var b=new BenchController(6);Assert.AreEqual(6,b.Size);}
 [Test] public void Merge_Two_Equals_One_And_Caps_At_Five(){var d=Hero("hero_kael");var m=new MergeService(5);var r=m.Merge(new BattleHeroInstance(d,4),new BattleHeroInstance(d,4));Assert.NotNull(r);Assert.AreEqual(5,r.StarLevel);Assert.IsNull(m.Merge(r,new BattleHeroInstance(d,5)));Object.DestroyImmediate(d);}
 [Test] public void QuickSummon_Validates_Before_Spending(){var d=Hero("hero_kael");var deck=new BattleDeck();deck.TryAdd(d);var eco=new BattleEconomy(3);var bench=new BenchController(1);var rng=new SeededRandomService(1);var s=new QuickSummonSystem(deck,eco,bench,rng,3);Assert.NotNull(s.TrySummon());Assert.AreEqual(0,eco.Gold);Assert.IsNull(s.TrySummon());Assert.AreEqual(0,eco.Gold);Object.DestroyImmediate(d);}
 [Test] public void StarScale_Uses_Canonical_Separate_Multipliers(){var s=StarScalingTable.Get(4);Assert.AreEqual(3.10f,s.Hp,.001f);Assert.AreEqual(2.80f,s.Attack,.001f);Assert.AreEqual(2.65f,s.Skill,.001f);}
 [Test] public void Sell_Cannot_Exceed_Gold_Cap(){var e=new BattleEconomy(98,99);e.AddGold(31);Assert.AreEqual(99,e.Gold);}
}}
