using Crownfall.Core; using Crownfall.Units; using UnityEngine;
namespace Crownfall.Battle.Combat {
public readonly struct DamageResult { public readonly float FinalDamage; public readonly bool Critical; public DamageResult(float d,bool c){FinalDamage=d;Critical=c;} }
public sealed class DamageSystem {
 readonly IRandomService rng; public DamageSystem(IRandomService r){rng=r;}
 public DamageResult Apply(Unit source,Unit target,float raw,DamageType type,bool canCrit){if(target==null||target.IsDead)return new(0,false); bool crit=canCrit&&source!=null&&rng.Value()<source.Definition.critChance; if(crit)raw*=source.Definition.critDamage; float def=type==DamageType.Physical?target.Definition.armor:target.Definition.magicResistance; float dmg=type==DamageType.True?raw:Mitigate(raw,def); float healthDamage=target.Shields==null?dmg:target.Shields.Absorb(dmg); target.ApplyHealthDamage(healthDamage); return new(healthDamage,crit);}
 public static float Mitigate(float raw,float defense){defense=Mathf.Max(-80,defense); float m=defense>=0?100f/(100f+defense):2f-100f/(100f-defense); return Mathf.Max(0,raw*m);}
}}
