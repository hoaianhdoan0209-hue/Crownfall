using Crownfall.Campaign;
using Crownfall.Core;
using UnityEngine;

namespace Crownfall.UI
{
    public sealed class BattlePresenter : MonoBehaviour
    {
        private AppFlowService _flow;
        private CampaignService _campaign;
        public void Bind(AppFlowService flow, CampaignService campaign) { _flow = flow; _campaign = campaign; }
        public void OnVictory(string stageId) { if (_flow != null && _campaign != null) _flow.CommitBattleVictory(_campaign, stageId); }
        public void OnExitToMap() => _flow?.ReturnToMap();
    }
}
