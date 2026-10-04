using System;
using Crownfall.Core;
using Crownfall.Save;

namespace Crownfall.Campaign
{
    public sealed class VerticalSliceOrchestrator
    {
        private readonly CampaignService _campaign;
        private readonly SaveService _saveService;
        public SaveData Save { get; private set; }
        public StageSession ActiveStage { get; private set; }

        public VerticalSliceOrchestrator(CampaignService campaign, SaveService saveService)
        { _campaign = campaign ?? throw new ArgumentNullException(nameof(campaign)); _saveService = saveService ?? throw new ArgumentNullException(nameof(saveService)); }

        public void StartNewGame() { Save = SaveService.CreateNewGame(); _saveService.Save(Save); }
        public void Continue() { Save = _saveService.LoadOrCreate(); }

        public StageSession BeginStage(StageSessionFactory factory, string stageId, int startingGold)
        {
            if (Save == null) Continue();
            if (Save.Campaign.CurrentStageId != stageId && !Save.Campaign.CompletedStageIds.Contains(stageId))
                throw new InvalidOperationException("Stage is not currently available: " + stageId);
            ActiveStage = factory.Create(stageId, startingGold);
            return ActiveStage;
        }

        public bool CompleteWave()
        {
            if (ActiveStage == null) return false;
            return ActiveStage.CompleteCurrentWave();
        }

        public void CommitStageVictory(bool noBacklineDeath = false)
        {
            if (Save == null || ActiveStage == null || !ActiveStage.IsComplete)
                throw new InvalidOperationException("Cannot commit victory before every wave is complete.");
            _campaign.CompleteStage(Save, ActiveStage.Encounter.StageId, noBacklineDeath);
            _saveService.Save(Save);
            ActiveStage = null;
        }
    }
}
