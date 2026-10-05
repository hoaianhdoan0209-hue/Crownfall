using System;
namespace Crownfall.Content {
[Serializable] public sealed class HeroRecord { public string Id, Name, Faction, Role; public float HP, Attack, SkillPower, Armor, MR, AttackSpeed, CritChance; public int Range, MaxEnergy; public string SkillId; }
[Serializable] public sealed class EnemyRecord { public string Id, Name, Role; public float HP, Attack, Armor, MR, AttackSpeed; public int Range; }
[Serializable] public sealed class SpawnRecord { public string EnemyId; public int X,Y; public SpawnRecord(string id,int x,int y){EnemyId=id;X=x;Y=y;} }
[Serializable] public sealed class WaveRecord { public string Id; public SpawnRecord[] Spawns; public WaveRecord(string id, params SpawnRecord[] spawns){Id=id;Spawns=spawns;} }
[Serializable] public sealed class EncounterRecord { public string StageId; public int BoardCapacity; public bool Boss; public string GuestHeroId; public string[] ArtifactChoices; public WaveRecord[] Waves; }
}
