using System;
using Crownfall.Save;

namespace Crownfall.Progression
{
    public sealed class RewardGrant
    {
        public int AccountGold;
        public int Essence;
        public int CrownShards;
        public string UnlockHeroId;
        public string AddFlag;
        public string CompleteChallengeId;
    }

    public sealed class RewardService
    {
        public bool Commit(SaveData save, string transactionId, RewardGrant grant)
        {
            if (save == null || grant == null) throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(transactionId)) throw new ArgumentException("Stable transaction id required.");
            if (save.Rewards.CommittedTransactionIds.Contains(transactionId)) return false;

            save.Account.Gold += Math.Max(0, grant.AccountGold);
            save.Account.Essence += Math.Max(0, grant.Essence);
            save.Account.CrownShards += Math.Max(0, grant.CrownShards);
            AddUnique(save.Heroes.UnlockedHeroIds, grant.UnlockHeroId);
            AddUnique(save.Campaign.Flags, grant.AddFlag);
            AddUnique(save.Campaign.CompletedChallenges, grant.CompleteChallengeId);
            save.Rewards.CommittedTransactionIds.Add(transactionId);
            return true;
        }

        private static void AddUnique(System.Collections.Generic.List<string> list, string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && !list.Contains(value)) list.Add(value);
        }
    }
}
