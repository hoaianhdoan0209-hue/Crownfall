using Crownfall.Core;
using UnityEngine;

namespace Crownfall.UI
{
    public sealed class RewardPresenter : MonoBehaviour
    {
        public BattleRewardView DisplayedReward { get; private set; }
        private AppFlowService _flow;
        public void Bind(AppFlowService flow) { _flow = flow; DisplayedReward = flow?.PendingReward; }
        // Presentation only: reward mutation happens before this presenter receives RewardResult.
        public void OnContinue() => _flow?.ReturnToMap();
    }
}
