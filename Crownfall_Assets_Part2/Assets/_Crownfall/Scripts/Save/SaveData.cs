using System;
using System.Collections.Generic;

namespace Crownfall.Save
{
    [Serializable]
    public sealed class SaveData
    {
        public int SaveVersion = SaveVersions.Current;
        public AccountProgress Account = new AccountProgress();
        public CampaignProgress Campaign = new CampaignProgress();
        public HeroProgress Heroes = new HeroProgress();
        public RewardLedger Rewards = new RewardLedger();
    }

    [Serializable] public sealed class AccountProgress { public int Level = 1; public int Experience; public int Gold; public int Essence; public int CrownShards; }
    [Serializable] public sealed class CampaignProgress { public string CurrentStageId; public List<string> CompletedStageIds = new List<string>(); public List<string> Flags = new List<string>(); public List<string> CompletedChallenges = new List<string>(); }
    [Serializable] public sealed class HeroProgress { public List<string> UnlockedHeroIds = new List<string>(); }
    [Serializable] public sealed class RewardLedger { public List<string> CommittedTransactionIds = new List<string>(); }
    public static class SaveVersions { public const int Current = 1; }
}
