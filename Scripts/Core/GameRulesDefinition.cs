using UnityEngine;

namespace Crownfall.Core
{
    [CreateAssetMenu(fileName = "GameRules", menuName = "Crownfall/Core/Game Rules")]
    public sealed class GameRulesDefinition : ScriptableObject
    {
        [Min(1)] public int gridWidth = 7;
        [Min(1)] public int gridHeight = 6;
        [Min(0.01f)] public float cellSize = 1f;
        [Min(1)] public int benchSize = 6;
        [Min(1)] public int maxCoreDeckSize = 6;
        [Min(1)] public int baseBoardCapacity = 6;
        [Min(1)] public int bossBoardCapacity = 7;
        [Range(1, 5)] public int maxStar = 5;
        [Min(0)] public int quickSummonCost = 3;
        [Min(0)] public int shopHeroCost = 4;
        [Min(1)] public int shopOfferCount = 3;
        [Min(1)] public int goldCap = 99;
        [Min(0f)] public float maxAttackSpeed = 3.5f;
        [Range(0f, 1f)] public float maxTenacity = 0.70f;
        [Range(0f, 1f)] public float maxDodge = 0.50f;
    }
}
