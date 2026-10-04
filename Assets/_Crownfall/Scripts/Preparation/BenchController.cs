using System.Collections.Generic;
namespace Crownfall.Preparation {
public sealed class BenchController {
 readonly BattleHeroInstance[] slots; public int Size=>slots.Length; public BenchController(int size=6){slots=new BattleHeroInstance[size];}
 public BattleHeroInstance Get(int index)=>index>=0&&index<slots.Length?slots[index]:null;
 public int FindEmpty(){for(int i=0;i<slots.Length;i++)if(slots[i]==null)return i;return -1;} public bool HasRoom=>FindEmpty()>=0;
 public bool TryPlace(BattleHeroInstance hero,int index){if(hero==null||index<0||index>=slots.Length||slots[index]!=null||Contains(hero))return false;slots[index]=hero;return true;}
 public int TryPlaceFirst(BattleHeroInstance hero){int i=FindEmpty();return i>=0&&TryPlace(hero,i)?i:-1;}
 public BattleHeroInstance Remove(int index){if(index<0||index>=slots.Length)return null;var h=slots[index];slots[index]=null;return h;}
 public bool Contains(BattleHeroInstance hero){if(hero==null)return false;for(int i=0;i<slots.Length;i++)if(ReferenceEquals(slots[i],hero))return true;return false;}
 public bool Swap(int a,int b){if(a<0||b<0||a>=slots.Length||b>=slots.Length)return false;(slots[a],slots[b])=(slots[b],slots[a]);return true;}
 public IEnumerable<BattleHeroInstance> Enumerate(){for(int i=0;i<slots.Length;i++)if(slots[i]!=null)yield return slots[i];}
}}
