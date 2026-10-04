using Crownfall.Core;
using UnityEngine;

namespace Crownfall.UI
{
    public sealed class WorldMapPresenter : MonoBehaviour
    {
        private AppFlowService _flow;
        public void Bind(AppFlowService flow) => _flow = flow;
        public void OnStageSelected(string stageId) => _flow?.SelectStage(stageId);
        public void OnBackToMenu() => _flow?.ReturnToMenu();
    }
}
