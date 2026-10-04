using Crownfall.Battle;
using Crownfall.Campaign;
using Crownfall.Progression;
using Crownfall.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Crownfall.UI
{
    public sealed class RuntimeBattleHud : MonoBehaviour
    {
        private RuntimeBattleSession _session;
        private Text _status;
        private Button _continue;
        private CampaignService _campaign;
        private bool _committed;

        public void Bind(RuntimeBattleSession session, Text status, Button continueButton)
        {
            _session=session; _status=status; _continue=continueButton; _campaign=new CampaignService(new RewardService());
            _session.Changed += Refresh; _continue.onClick.AddListener(Continue); Refresh();
        }
        private void OnDestroy(){if(_session!=null)_session.Changed-=Refresh;}
        private void Update(){if(_session!=null&&!_session.IsFinished)Refresh();}
        private void Refresh()
        {
            if(_session==null||_status==null)return;
            if(_session.IsFinished){_status.text=_session.PlayerWon?"VICTORY — encounter cleared":"DEFEAT — your formation was eliminated";_continue.gameObject.SetActive(true);}
            else {var boss=_session.Encounter.Boss?(_session.BossEnraged?"   GORRUK: ENRAGED":_session.BossReinforcementsTriggered?"   GORRUK: REINFORCEMENTS":"   GORRUK") : "";_status.text=$"Wave {_session.WaveIndex+1}/{_session.Encounter.Waves.Length}   Allies {_session.LivingPlayers}   Enemies {_session.LivingEnemies}"+boss;_continue.gameObject.SetActive(false);}
        }
        private void Continue()
        {
            if(_session==null)return;
            if(_session.PlayerWon&&!_committed){RuntimeServices.Flow.CommitBattleVictory(_campaign,_session.Encounter.StageId);_committed=true;}
            RuntimeServices.Flow.ReturnToMap();
        }
    }
}
