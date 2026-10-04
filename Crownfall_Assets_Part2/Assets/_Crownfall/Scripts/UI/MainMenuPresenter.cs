using Crownfall.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Crownfall.UI
{
    public sealed class MainMenuPresenter : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        private AppFlowService _flow;
        public void Bind(AppFlowService flow) { _flow = flow; Refresh(); }
        public void Refresh() { if (continueButton != null) continueButton.interactable = _flow != null && _flow.CanContinue; }
        public void OnNewGame() => _flow?.NewGame();
        public void OnContinue() => _flow?.Continue();
    }
}
