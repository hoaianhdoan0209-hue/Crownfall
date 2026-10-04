using System;
using System.Collections.Generic;
using Crownfall.Core;

namespace Crownfall.Preparation
{
    [Serializable]
    public sealed class RuntimeFormationSlot
    {
        public string HeroId { get; }
        public GridCoordinate Cell { get; }
        public RuntimeFormationSlot(string heroId, GridCoordinate cell) { HeroId = heroId; Cell = cell; }
    }

    /// <summary>Transient pre-battle formation. It deliberately lives outside SaveData: a formation belongs to one battle attempt.</summary>
    public sealed class RuntimeFormationPlan
    {
        private readonly List<RuntimeFormationSlot> _slots = new();
        public IReadOnlyList<RuntimeFormationSlot> Slots => _slots;
        public int Capacity { get; }
        public int Count => _slots.Count;
        public bool CanStartCombat => Count > 0;

        public RuntimeFormationPlan(int capacity) { Capacity = Math.Max(1, capacity); }

        public bool Contains(string heroId)
        {
            foreach (var slot in _slots) if (slot.HeroId == heroId) return true;
            return false;
        }

        public bool TryToggle(string heroId)
        {
            if (string.IsNullOrWhiteSpace(heroId)) return false;
            for (int i = 0; i < _slots.Count; i++)
            {
                if (_slots[i].HeroId != heroId) continue;
                _slots.RemoveAt(i);
                Repack();
                return true;
            }
            if (_slots.Count >= Capacity) return false;
            _slots.Add(new RuntimeFormationSlot(heroId, CellFor(_slots.Count)));
            return true;
        }

        public void AutoFill(IEnumerable<string> heroIds)
        {
            if (heroIds == null) return;
            foreach (var id in heroIds)
            {
                if (_slots.Count >= Capacity) break;
                if (!Contains(id) && !string.IsNullOrWhiteSpace(id)) _slots.Add(new RuntimeFormationSlot(id, CellFor(_slots.Count)));
            }
        }

        private void Repack()
        {
            for (int i = 0; i < _slots.Count; i++) _slots[i] = new RuntimeFormationSlot(_slots[i].HeroId, CellFor(i));
        }

        private static GridCoordinate CellFor(int index) => new GridCoordinate(index % 7, index / 7);
    }
}
