using System;
using Crownfall.Content;
using Crownfall.Core;

namespace Crownfall.Campaign
{
    public sealed class StageSessionFactory
    {
        private readonly GameRulesDefinition _rules;
        public StageSessionFactory(GameRulesDefinition rules) { _rules = rules ?? throw new ArgumentNullException(nameof(rules)); }

        public StageSession Create(string stageId, int startingGold)
        {
            var encounter = FirstPlayableContentCatalog.FindEncounter(stageId);
            if (encounter == null) throw new InvalidOperationException("Unknown encounter: " + stageId);
            return new StageSession(encounter, _rules, startingGold);
        }
    }
}
