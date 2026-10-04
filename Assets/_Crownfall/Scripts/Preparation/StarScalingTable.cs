using UnityEngine;
namespace Crownfall.Preparation {
public readonly struct StarScale { public readonly float Hp,Attack,Skill; public StarScale(float hp,float attack,float skill){Hp=hp;Attack=attack;Skill=skill;} }
public static class StarScalingTable {
 public static StarScale Get(int star)=>star switch {1=>new(1f,1f,1f),2=>new(1.55f,1.45f,1.40f),3=>new(2.25f,2.05f,1.95f),4=>new(3.10f,2.80f,2.65f),5=>new(4.20f,3.75f,3.50f),_=>Get(Mathf.Clamp(star,1,5))};
}}
