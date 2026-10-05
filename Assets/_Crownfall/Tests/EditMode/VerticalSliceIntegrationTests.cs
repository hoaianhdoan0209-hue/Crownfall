#if UNITY_INCLUDE_TESTS
using NUnit.Framework;
using Crownfall.Campaign;
using Crownfall.Content;
using Crownfall.Progression;
using Crownfall.Save;

namespace Crownfall.Tests.EditMode
{
    public sealed class VerticalSliceIntegrationTests
    {
        [Test]
        public void Catalog_HasCompleteChapterOneMainPath()
        {
            foreach (var id in ChapterOneDefinition.MainPath)
                Assert.NotNull(FirstPlayableContentCatalog.FindEncounter(id), "Missing encounter: " + id);
        }

        [Test]
        public void MainPath_ProgressesToChapterComplete_AndRewardsAreIdempotent()
        {
            var save = SaveService.CreateNewGame();
            var rewards = new RewardService();
            var campaign = new CampaignService(rewards);
            Assert.AreEqual(CampaignStageId.Prologue, save.Campaign.CurrentStageId);

            foreach (var stage in ChapterOneDefinition.MainPath)
            {
                Assert.AreEqual(stage, save.Campaign.CurrentStageId);
                campaign.CompleteStage(save, stage, stage == CampaignStageId.FalseEnemy);
            }

            Assert.IsNull(save.Campaign.CurrentStageId);
            CollectionAssert.Contains(save.Heroes.UnlockedHeroIds, ProgressionIds.HeroThorne);
            CollectionAssert.Contains(save.Heroes.UnlockedHeroIds, ProgressionIds.HeroBrakk);
            CollectionAssert.Contains(save.Heroes.UnlockedHeroIds, ProgressionIds.HeroLyra);
            CollectionAssert.Contains(save.Campaign.Flags, ProgressionIds.FlagChapterOneComplete);
            Assert.IsTrue(campaign.CanEnterNyxTrial(save));

            int essence = save.Account.Essence;
            int txCount = save.Rewards.CommittedTransactionIds.Count;
            campaign.CompleteStage(save, CampaignStageId.Gorruk);
            Assert.AreEqual(essence, save.Account.Essence);
            Assert.AreEqual(txCount, save.Rewards.CommittedTransactionIds.Count);
        }

        [Test]
        public void GorrukEncounter_UsesSevenUnitBoard_AndCanonicalBoss()
        {
            var e = FirstPlayableContentCatalog.FindEncounter(CampaignStageId.Gorruk);
            Assert.NotNull(e); Assert.AreEqual(7, e.BoardCapacity); Assert.IsTrue(e.Boss);
            var boss = FirstPlayableContentCatalog.FindEnemy(CanonicalIds.Gorruk);
            Assert.AreEqual(4500f, boss.HP); Assert.AreEqual(65f, boss.Attack);
        }
    }
}
#endif
