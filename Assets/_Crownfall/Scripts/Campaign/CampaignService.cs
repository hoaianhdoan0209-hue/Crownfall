using System;
using Crownfall.Progression;
using Crownfall.Save;

namespace Crownfall.Campaign
{
    public sealed class CampaignService
    {
        private readonly RewardService _rewards;
        public CampaignService(RewardService rewards) { _rewards = rewards ?? throw new ArgumentNullException(nameof(rewards)); }

        public void CompleteStage(SaveData save, string stageId, bool noBacklineDeath = false)
        {
            if (save == null) throw new ArgumentNullException(nameof(save));
            if (string.IsNullOrWhiteSpace(stageId)) throw new ArgumentException(nameof(stageId));
            AddUnique(save.Campaign.CompletedStageIds, stageId);

            switch (stageId)
            {
                case CampaignStageId.Prologue: break;
                case CampaignStageId.BurningRoad:
                    _rewards.Commit(save, "reward:ch1:stage1:thorne", new RewardGrant { UnlockHeroId = ProgressionIds.HeroThorne }); break;
                case CampaignStageId.WarfangEncounter:
                    _rewards.Commit(save, "reward:ch1:stage4:brakk", new RewardGrant { UnlockHeroId = ProgressionIds.HeroBrakk }); break;
                case CampaignStageId.FalseEnemy:
                    if (noBacklineDeath) _rewards.Commit(save, "reward:ch1:challenge:no_backline_death", new RewardGrant { CompleteChallengeId = ProgressionIds.ChallengeNoBacklineDeath });
                    break;
                case CampaignStageId.Gorruk:
                    _rewards.Commit(save, "reward:ch1:gorruk:lyra", new RewardGrant { UnlockHeroId = ProgressionIds.HeroLyra, AddFlag = ProgressionIds.FlagChapterOneComplete, Essence = 25 });
                    break;
            }

            save.Campaign.CurrentStageId = ChapterOneDefinition.NextMainStage(stageId);
        }

        public bool CanEnterBuriedSunTemple(SaveData save) => save.Campaign.Flags.Contains(ProgressionIds.FlagTempleStatueSpared);
        public bool CanEnterNyxTrial(SaveData save) => save.Campaign.Flags.Contains(ProgressionIds.FlagChapterOneComplete) && save.Campaign.CompletedChallenges.Contains(ProgressionIds.ChallengeNoBacklineDeath);

        public void CompleteNyxTrial(SaveData save) => _rewards.Commit(save, "reward:ch1:nyx_trial", new RewardGrant { UnlockHeroId = ProgressionIds.HeroNyx });

        private static void AddUnique(System.Collections.Generic.List<string> list, string value) { if (!list.Contains(value)) list.Add(value); }
    }
}
