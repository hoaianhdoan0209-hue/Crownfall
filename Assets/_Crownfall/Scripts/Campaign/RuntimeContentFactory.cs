using System.Collections.Generic;
using Crownfall.Content;
using Crownfall.Units;
using UnityEngine;

namespace Crownfall.Campaign
{
    /// <summary>Creates transient UnitDefinitions from the locked first-playable records.</summary>
    public sealed class RuntimeContentFactory
    {
        private readonly Dictionary<string, UnitDefinition> _cache = new();

        public UnitDefinition Hero(string id)
        {
            if (_cache.TryGetValue(id, out var cached)) return cached;
            var record = FirstPlayableContentCatalog.FindHero(id);
            if (record == null) return null;
            var d = ScriptableObject.CreateInstance<UnitDefinition>();
            d.hideFlags = HideFlags.DontSave;
            d.unitId = record.Id; d.displayName = record.Name; d.maxHealth = record.HP; d.attack = record.Attack;
            d.skillPower = record.SkillPower; d.armor = record.Armor; d.magicResistance = record.MR;
            d.attackInterval = record.AttackSpeed <= 0 ? 1f : 1f / record.AttackSpeed; d.attackRange = record.Range;
            d.maxEnergy = record.MaxEnergy; d.critChance = record.CritChance;
            _cache[id] = d; return d;
        }

        public UnitDefinition Enemy(string id)
        {
            if (_cache.TryGetValue(id, out var cached)) return cached;
            var record = FirstPlayableContentCatalog.FindEnemy(id);
            if (record == null) return null;
            var d = ScriptableObject.CreateInstance<UnitDefinition>();
            d.hideFlags = HideFlags.DontSave;
            d.unitId = record.Id; d.displayName = record.Name; d.maxHealth = record.HP; d.attack = record.Attack;
            d.armor = record.Armor; d.magicResistance = record.MR;
            d.attackInterval = record.AttackSpeed <= 0 ? 1f : 1f / record.AttackSpeed; d.attackRange = record.Range;
            _cache[id] = d; return d;
        }
    }
}
