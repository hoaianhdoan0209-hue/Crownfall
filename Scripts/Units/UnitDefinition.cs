using UnityEngine;
namespace Crownfall.Units {
[CreateAssetMenu(fileName="Unit", menuName="Crownfall/Units/Unit Definition")]
public sealed class UnitDefinition : ScriptableObject {
 public string unitId; public string displayName;
 [Min(1)] public float maxHealth=100; [Min(0)] public float attack=10; public float armor; public float magicResistance; public float skillPower;
 [Min(.05f)] public float attackInterval=1f; [Min(1)] public int attackRange=1; [Min(.1f)] public float moveSpeed=3f;
 [Range(0,1)] public float critChance=.05f; [Min(1)] public float critDamage=1.5f;
 [Min(1)] public float maxEnergy=100; [Min(0)] public float energyPerBasicAttack=10; [Min(0)] public float energyPerHitTaken=2;
 public Crownfall.Skills.SkillDefinition skill; public GameObject prefab;
}}
