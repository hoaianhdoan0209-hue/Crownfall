using Crownfall.Campaign;
using Crownfall.Save;

namespace Crownfall.Core
{
    public sealed class BattleRewardView
    {
        public readonly int Gold, Essence, CrownShards; public readonly bool HeroUnlocked;
        public BattleRewardView(int gold, int essence, int shards, bool heroUnlocked) { Gold = gold; Essence = essence; CrownShards = shards; HeroUnlocked = heroUnlocked; }
    }

    public sealed class AppFlowService
    {
        private readonly SaveService _saveService;
        private readonly ISceneService _scenes;
        public SaveData CurrentSave { get; private set; }
        public string SelectedStageId { get; private set; }
        public BattleRewardView PendingReward { get; private set; }
        public bool CanContinue => _saveService.HasSave;

        public AppFlowService(SaveService saveService, ISceneService scenes)
        { _saveService = saveService; _scenes = scenes; }

        public void NewGame()
        {
            CurrentSave = SaveService.CreateNewGame();
            _saveService.Save(CurrentSave);
            _scenes.Load(SceneIds.WorldMap);
        }

        public bool Continue()
        {
            if (!_saveService.HasSave) return false;
            CurrentSave = _saveService.LoadOrCreate();
            _scenes.Load(SceneIds.WorldMap);
            return true;
        }

        public void SelectStage(string stageId)
        {
            if (string.IsNullOrWhiteSpace(stageId)) return;
            SelectedStageId = stageId;
            _scenes.Load(SceneIds.Battle);
        }

        public void CommitBattleVictory(CampaignService campaign, string stageId)
        {
            EnsureSave();
            int goldBefore = CurrentSave.Account.Gold;
            int essenceBefore = CurrentSave.Account.Essence;
            int shardsBefore = CurrentSave.Account.CrownShards;
            int heroesBefore = CurrentSave.Heroes.UnlockedHeroIds.Count;
            campaign.CompleteStage(CurrentSave, stageId);
            PendingReward = new BattleRewardView(CurrentSave.Account.Gold - goldBefore, CurrentSave.Account.Essence - essenceBefore, CurrentSave.Account.CrownShards - shardsBefore, CurrentSave.Heroes.UnlockedHeroIds.Count > heroesBefore);
            _saveService.Save(CurrentSave);
        }

        public void ReturnToMap() => _scenes.Load(SceneIds.WorldMap);
        public void ReturnToMenu() => _scenes.Load(SceneIds.MainMenu);

        private void EnsureSave()
        {
            if (CurrentSave == null) CurrentSave = _saveService.LoadOrCreate();
        }
    }
}
