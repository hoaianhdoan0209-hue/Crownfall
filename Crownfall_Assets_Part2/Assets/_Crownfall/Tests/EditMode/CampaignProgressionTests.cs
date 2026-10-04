#if UNITY_INCLUDE_TESTS
using Crownfall.Campaign;
using Crownfall.Progression;
using Crownfall.Save;
using NUnit.Framework;

namespace Crownfall.Tests
{
    public sealed class CampaignProgressionTests
    {
        [Test] public void NewGame_StartsWithKaelSeraphineAndPrologue()
        {
            var s = SaveService.CreateNewGame();
            Assert.AreEqual(CampaignStageId.Prologue, s.Campaign.CurrentStageId);
            CollectionAssert.Contains(s.Heroes.UnlockedHeroIds, ProgressionIds.HeroKael);
            CollectionAssert.Contains(s.Heroes.UnlockedHeroIds, ProgressionIds.HeroSeraphine);
        }

        [Test] public void GorrukReward_IsIdempotent_AndUnlocksLyra()
        {
            var s = SaveService.CreateNewGame(); var c = new CampaignService(new RewardService());
            c.CompleteStage(s, CampaignStageId.Gorruk); c.CompleteStage(s, CampaignStageId.Gorruk);
            Assert.AreEqual(1, s.Heroes.UnlockedHeroIds.FindAll(x => x == ProgressionIds.HeroLyra).Count);
            Assert.AreEqual(25, s.Account.Essence);
            CollectionAssert.Contains(s.Campaign.Flags, ProgressionIds.FlagChapterOneComplete);
        }

        [Test] public void MainPath_HasCanonicalOrder()
        {
            Assert.AreEqual(CampaignStageId.BurningRoad, ChapterOneDefinition.NextMainStage(CampaignStageId.Prologue));
            Assert.AreEqual(CampaignStageId.Gorruk, ChapterOneDefinition.NextMainStage(CampaignStageId.FalseEnemy));
            Assert.IsNull(ChapterOneDefinition.NextMainStage(CampaignStageId.Gorruk));
        }
    }
}
#endif
