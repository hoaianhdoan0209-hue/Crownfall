using System; using System.Collections.Generic; using Crownfall.Core; using UnityEngine;
namespace Crownfall.Campaign {
[Serializable] public sealed class EnemySpawnEntry { public EnemyArchetypeDefinition enemy; public GridCoordinate coordinate; }
[CreateAssetMenu(fileName="Wave", menuName="Crownfall/Campaign/Wave")]
public sealed class WaveDefinition : ScriptableObject { public string waveId; public List<EnemySpawnEntry> enemies=new(); public int victoryGold=5; }
}
