using Crownfall.AI; using Crownfall.Units; using UnityEngine;
namespace Crownfall.Campaign {
public enum EnemyRole { SwarmMelee, Ranged, Tank, Support, Assassin, BannerSupport, DeathBurst, Hunter, Boss }
[CreateAssetMenu(fileName="Enemy", menuName="Crownfall/Campaign/Enemy Archetype")]
public sealed class EnemyArchetypeDefinition : ScriptableObject {
 public string enemyId; public EnemyRole role; public UnitDefinition unit; public AIProfileDefinition aiProfile;
 public bool openingLeap; public bool deathBurst; public float deathBurstDamage; public float auraAttackPercent;
}}
