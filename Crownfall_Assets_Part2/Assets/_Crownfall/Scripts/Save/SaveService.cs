using System;
using System.IO;
using UnityEngine;

namespace Crownfall.Save
{
    public sealed class SaveService
    {
        public const string FileName = "crownfall_save.json";
        private readonly string _path;
        private readonly string _tempPath;
        private readonly string _backupPath;

        public SaveService(string directory = null)
        {
            directory = string.IsNullOrWhiteSpace(directory) ? Application.persistentDataPath : directory;
            Directory.CreateDirectory(directory);
            _path = Path.Combine(directory, FileName);
            _tempPath = _path + ".tmp";
            _backupPath = _path + ".bak";
        }

        public bool HasSave => File.Exists(_path) || File.Exists(_backupPath);

        public void Save(SaveData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            data.SaveVersion = SaveVersions.Current;
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(_tempPath, json);
            var verified = JsonUtility.FromJson<SaveData>(File.ReadAllText(_tempPath));
            if (verified == null) throw new IOException("Temporary save verification failed.");
            if (File.Exists(_path)) File.Copy(_path, _backupPath, true);
            if (File.Exists(_path)) File.Delete(_path);
            File.Move(_tempPath, _path);
        }

        public SaveData LoadOrCreate()
        {
            SaveData data = TryLoad(_path) ?? TryLoad(_backupPath) ?? CreateNewGame();
            SaveMigration.MigrateToCurrent(data);
            Normalize(data);
            return data;
        }

        private static SaveData TryLoad(string path)
        {
            if (!File.Exists(path)) return null;
            try { return JsonUtility.FromJson<SaveData>(File.ReadAllText(path)); }
            catch { return null; }
        }

        public static SaveData CreateNewGame()
        {
            var data = new SaveData();
            data.Campaign.CurrentStageId = Campaign.CampaignStageId.Prologue;
            data.Heroes.UnlockedHeroIds.Add(Progression.ProgressionIds.HeroKael);
            data.Heroes.UnlockedHeroIds.Add(Progression.ProgressionIds.HeroSeraphine);
            return data;
        }

        private static void Normalize(SaveData data)
        {
            data.Account ??= new AccountProgress();
            data.Campaign ??= new CampaignProgress();
            data.Heroes ??= new HeroProgress();
            data.Rewards ??= new RewardLedger();
            data.Campaign.CompletedStageIds ??= new System.Collections.Generic.List<string>();
            data.Campaign.Flags ??= new System.Collections.Generic.List<string>();
            data.Campaign.CompletedChallenges ??= new System.Collections.Generic.List<string>();
            data.Heroes.UnlockedHeroIds ??= new System.Collections.Generic.List<string>();
            data.Rewards.CommittedTransactionIds ??= new System.Collections.Generic.List<string>();
        }
    }
}
