using System;
using Crownfall.Content;
using Crownfall.Core;
using Crownfall.Preparation;

namespace Crownfall.Campaign
{
    public sealed class StageSession
    {
        public EncounterRecord Encounter { get; }
        public PreparationController Preparation { get; }
        public int WaveIndex { get; private set; }
        public bool IsComplete => WaveIndex >= Encounter.Waves.Length;
        public WaveRecord CurrentWave => IsComplete ? null : Encounter.Waves[WaveIndex];

        public StageSession(EncounterRecord encounter, GameRulesDefinition rules, int startingGold)
        {
            Encounter = encounter ?? throw new ArgumentNullException(nameof(encounter));
            Preparation = new PreparationController(rules ?? throw new ArgumentNullException(nameof(rules)), startingGold);
            Preparation.Board.SetCapacity(encounter.BoardCapacity);
        }

        public bool CompleteCurrentWave()
        {
            if (IsComplete) return false;
            WaveIndex++;
            return true;
        }
    }
}
