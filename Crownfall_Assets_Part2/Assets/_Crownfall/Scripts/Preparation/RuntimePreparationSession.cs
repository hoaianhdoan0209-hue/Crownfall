using System;
using System.Collections.Generic;
using Crownfall.Campaign;
using Crownfall.Content;
using Crownfall.Core;
using Crownfall.Economy;
using Crownfall.Units;

namespace Crownfall.Preparation
{
    /// <summary>Runtime adapter that exposes the existing preparation economy/shop/merge systems to the shell-authored UI.</summary>
    public sealed class RuntimePreparationSession
    {
        public const int StartingGold = 10;
        public PreparationController Preparation { get; }
        public BattleShop Shop { get; }
        public QuickSummonSystem QuickSummon { get; }
        public EncounterRecord Encounter { get; }
        public event Action Changed;
        private readonly RuntimeContentFactory _content = new RuntimeContentFactory();

        public RuntimePreparationSession(EncounterRecord encounter, IEnumerable<string> heroIds)
        {
            Encounter = encounter ?? throw new ArgumentNullException(nameof(encounter));
            var rules = UnityEngine.ScriptableObject.CreateInstance<GameRulesDefinition>();
            rules.hideFlags = UnityEngine.HideFlags.DontSave;
            Preparation = new PreparationController(rules, StartingGold);
            Preparation.Board.SetCapacity(encounter.BoardCapacity);
            if (heroIds != null) foreach (var id in heroIds) { var d=_content.Hero(id); if(d!=null) Preparation.Deck.TryAdd(d); }
            var rng = new SeededRandomService(StableSeed(encounter.StageId + ":prep"));
            Shop = new BattleShop(Preparation.Deck, Preparation.Economy, Preparation.Bench, rng);
            QuickSummon = new QuickSummonSystem(Preparation.Deck, Preparation.Economy, Preparation.Bench, rng, rules.quickSummonCost);
            Shop.BeginWave();
        }

        public bool TryQuickSummon(){var ok=QuickSummon.TrySummon()!=null;if(ok)Changed?.Invoke();return ok;}
        public bool TryBuy(int offer){var ok=Shop.TryBuy(offer,id=>new BattleHeroInstance(_content.Hero(id)));if(ok)Changed?.Invoke();return ok;}
        public bool TryRefresh(){var ok=Shop.TryManualRefresh();if(ok)Changed?.Invoke();return ok;}
        public void ToggleLock(){Shop.ToggleLock();Changed?.Invoke();}
        public bool TryMergeBench(int a,int b){var ok=Preparation.TryMergeBench(a,b);if(ok)Changed?.Invoke();return ok;}
        public bool TrySellBench(int i){var ok=Preparation.TrySellBench(i);if(ok)Changed?.Invoke();return ok;}
        public BattleHeroInstance BenchAt(int i)=>Preparation.Bench.Get(i);
        public string HeroName(BattleHeroInstance h)=>h?.Definition?.displayName ?? h?.Definition?.unitId ?? "Empty";
        public List<string> OwnedHeroIds(){var r=new List<string>();foreach(var h in Preparation.Bench.Enumerate()) if(h?.Definition!=null) r.Add(h.Definition.unitId);return r;}
        private static int StableSeed(string value){unchecked{int h=23;if(value!=null)foreach(char c in value)h=h*31+c;return h;}}
    }
}
