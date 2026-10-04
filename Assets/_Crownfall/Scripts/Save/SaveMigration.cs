using System;

namespace Crownfall.Save
{
    public interface ISaveMigration { int FromVersion { get; } int ToVersion { get; } void Apply(SaveData data); }

    public static class SaveMigration
    {
        public static void MigrateToCurrent(SaveData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (data.SaveVersion > SaveVersions.Current) throw new InvalidOperationException("Save was created by a newer Crownfall version.");
            // Version 1 is the first playable schema. Future migrations must be explicit and sequential.
            data.SaveVersion = SaveVersions.Current;
        }
    }
}
