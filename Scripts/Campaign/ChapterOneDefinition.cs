using System;
using System.Collections.Generic;

namespace Crownfall.Campaign
{
    public sealed class ChapterOneDefinition
    {
        public static readonly IReadOnlyList<string> MainPath = Array.AsReadOnly(new[]
        {
            CampaignStageId.Prologue,
            CampaignStageId.BurningRoad,
            CampaignStageId.BrokenOutpost,
            CampaignStageId.GoblinPass,
            CampaignStageId.WarfangEncounter,
            CampaignStageId.FalseEnemy,
            CampaignStageId.Gorruk
        });

        public static string NextMainStage(string completedStageId)
        {
            for (int i = 0; i < MainPath.Count - 1; i++)
                if (MainPath[i] == completedStageId) return MainPath[i + 1];
            return null;
        }
    }
}
