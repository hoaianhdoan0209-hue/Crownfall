using System.Collections.Generic; using Crownfall.Units;
namespace Crownfall.Preparation {
public sealed class BattleDeck {
 readonly List<UnitDefinition> heroes=new(); public IReadOnlyList<UnitDefinition> Heroes=>heroes; public int MaxSize{get;}
 public BattleDeck(int maxSize=6){MaxSize=maxSize;}
 public bool TryAdd(UnitDefinition hero){if(hero==null||heroes.Count>=MaxSize||heroes.Contains(hero))return false;heroes.Add(hero);return true;}
 public bool Remove(UnitDefinition hero)=>heroes.Remove(hero);
}}
