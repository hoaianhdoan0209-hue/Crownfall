#if UNITY_INCLUDE_TESTS
using NUnit.Framework;
using Crownfall.Content;
using Crownfall.Preparation;
namespace Crownfall.Tests.Stabilization {
 public sealed class RuntimePreparationEconomyTests {
  [Test] public void Session_UsesCanonicalDeck_AndStartsWithOffers(){var e=FirstPlayableContentCatalog.Encounters[1];var s=new RuntimePreparationSession(e,new[]{CanonicalIds.Kael,CanonicalIds.Seraphine});Assert.AreEqual(2,s.Preparation.Deck.Heroes.Count);Assert.AreEqual(3,s.Shop.Offers.Count);Assert.AreEqual(RuntimePreparationSession.StartingGold,s.Preparation.Economy.Gold);}
  [Test] public void QuickSummon_SpendsGoldAndAddsBenchHero(){var s=new RuntimePreparationSession(FirstPlayableContentCatalog.Encounters[1],new[]{CanonicalIds.Kael});Assert.IsTrue(s.TryQuickSummon());Assert.AreEqual(7,s.Preparation.Economy.Gold);Assert.IsNotNull(s.BenchAt(0));}
 }
}
#endif
