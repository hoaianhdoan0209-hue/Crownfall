#if UNITY_INCLUDE_TESTS
using NUnit.Framework; using Crownfall.Content; using Crownfall.Campaign;
namespace Crownfall.Tests { public sealed class ContentCatalogTests {
 [Test] public void CanonicalContent_HasNoValidationErrors(){ Assert.That(ContentValidator.Validate(), Is.Empty); }
 [Test] public void FirstPlayable_HasSixHeroesAndEightRegularEnemies(){ Assert.AreEqual(6,FirstPlayableContentCatalog.Heroes.Length); Assert.AreEqual(9,FirstPlayableContentCatalog.Enemies.Length); }
 [Test] public void StageThree_OffersCanonicalArtifacts(){ var e=FirstPlayableContentCatalog.FindEncounter(CampaignStageId.GoblinPass); CollectionAssert.AreEquivalent(new[]{CanonicalIds.WarDrum,CanonicalIds.ArcaneCrown,CanonicalIds.BloodChalice},e.ArtifactChoices); }
 [Test] public void StageFour_UsesBrakkGuest(){ Assert.AreEqual(CanonicalIds.Brakk,FirstPlayableContentCatalog.FindEncounter(CampaignStageId.WarfangEncounter).GuestHeroId); }
 [Test] public void Gorruk_UsesSevenBoardCapacity(){ Assert.AreEqual(7,FirstPlayableContentCatalog.FindEncounter(CampaignStageId.Gorruk).BoardCapacity); }
}}
#endif
