using System;
using System.Collections.Generic;
using Crownfall.Campaign;
namespace Crownfall.Content {
public static class FirstPlayableContentCatalog {
 public static readonly HeroRecord[] Heroes={
  H(CanonicalIds.Kael,"Kael","Solarian","Warrior",450,42,0,35,20,1f,1,100,.05f,"skill_infernal_cleave"),
  H(CanonicalIds.Seraphine,"Seraphine","Solarian","Support",320,15,0,15,40,.8f,4,110,.05f,"skill_radiant_grace"),
  H(CanonicalIds.Thorne,"Thorne","Verdant","Ranger",280,35,0,15,15,1.4f,5,100,.05f,"skill_toxic_volley"),
  H(CanonicalIds.Brakk,"Brakk","Wildclaw","Warrior",520,36,0,30,15,.9f,1,100,.05f,"skill_blood_frenzy"),
  H(CanonicalIds.Lyra,"Lyra","Astral","Mage",240,18,55,10,30,.7f,5,120,.05f,"skill_falling_stars"),
  H(CanonicalIds.Nyx,"Nyx","Eclipsed","Assassin",270,58,0,18,20,1.25f,1,80,.15f,"skill_shadow_execution")};
 public static readonly EnemyRecord[] Enemies={
  E(CanonicalIds.Goblin,"Goblin","SwarmMelee",150,22,8,5,1.0f,1), E(CanonicalIds.Archer,"Goblin Archer","Ranged",110,25,5,5,.9f,5),
  E(CanonicalIds.Tank,"Ironhide Tank","Tank",340,20,38,12,.65f,1), E(CanonicalIds.Shaman,"Goblin Shaman","Support",160,14,8,25,.7f,4),
  E(CanonicalIds.Skulker,"Skulker","Assassin",145,32,10,8,1.2f,1), E(CanonicalIds.Bannerman,"Bannerman","BannerSupport",210,18,18,15,.75f,1),
  E(CanonicalIds.Riftling,"Riftling","DeathBurst",120,20,5,20,1f,1), E(CanonicalIds.RiftHound,"Rift Hound","Hunter",190,30,12,10,1.25f,1),
  E(CanonicalIds.Gorruk,"Gorruk — The Iron Maw","Boss",4500,65,50,30,.75f,1)};
 public static readonly EncounterRecord[] Encounters={
  Enc(CampaignStageId.Prologue,6,false,null,null,W("prologue_w1",S(CanonicalIds.Goblin,4,4),S(CanonicalIds.Goblin,3,5),S(CanonicalIds.Goblin,4,5))),
  Enc(CampaignStageId.BurningRoad,6,false,null,null,W("s1_w1",S(CanonicalIds.Goblin,3,5),S(CanonicalIds.Goblin,4,5)),W("s1_w2",S(CanonicalIds.Goblin,2,5),S(CanonicalIds.Goblin,3,5),S(CanonicalIds.Goblin,4,5)),W("s1_w3",S(CanonicalIds.Goblin,2,5),S(CanonicalIds.Goblin,3,5),S(CanonicalIds.Goblin,4,5),S(CanonicalIds.Archer,3,4))),
  Enc(CampaignStageId.BrokenOutpost,6,false,null,null,W("s2_w1",S(CanonicalIds.Goblin,2,5),S(CanonicalIds.Goblin,4,5),S(CanonicalIds.Archer,3,4)),W("s2_w2",S(CanonicalIds.Tank,3,5),S(CanonicalIds.Archer,2,4),S(CanonicalIds.Archer,4,4)),W("s2_w3",S(CanonicalIds.Tank,3,5),S(CanonicalIds.Archer,2,4),S(CanonicalIds.Shaman,4,4))),
  Enc(CampaignStageId.GoblinPass,6,false,null,new[]{CanonicalIds.WarDrum,CanonicalIds.ArcaneCrown,CanonicalIds.BloodChalice},W("s3_w1",S(CanonicalIds.Goblin,2,5),S(CanonicalIds.Bannerman,3,5),S(CanonicalIds.Goblin,4,5)),W("s3_w2",S(CanonicalIds.Tank,3,5),S(CanonicalIds.Bannerman,3,4),S(CanonicalIds.Archer,2,4),S(CanonicalIds.Archer,4,4))),
  Enc(CampaignStageId.WarfangEncounter,6,false,CanonicalIds.Brakk,null,W("s4_w1",S(CanonicalIds.Goblin,2,5),S(CanonicalIds.Goblin,3,5),S(CanonicalIds.Tank,4,5)),W("s4_w2",S(CanonicalIds.Tank,3,5),S(CanonicalIds.Bannerman,3,4),S(CanonicalIds.Archer,1,4),S(CanonicalIds.Archer,5,4))),
  Enc(CampaignStageId.FalseEnemy,6,false,null,null,W("s5_w1",S(CanonicalIds.Tank,3,5),S(CanonicalIds.Goblin,2,5),S(CanonicalIds.Goblin,4,5),S(CanonicalIds.Skulker,3,3)),W("s5_w2",S(CanonicalIds.Tank,3,5),S(CanonicalIds.Skulker,2,4),S(CanonicalIds.Skulker,4,4),S(CanonicalIds.Shaman,3,3)),W("s5_w3",S(CanonicalIds.RiftHound,3,5),S(CanonicalIds.Riftling,2,4),S(CanonicalIds.Riftling,4,4),S(CanonicalIds.Skulker,3,3))),
  Enc(CampaignStageId.Gorruk,7,true,null,null,W("gorruk_w1",S(CanonicalIds.Gorruk,3,5))) };
 public static HeroRecord FindHero(string id)=>Array.Find(Heroes,x=>x.Id==id); public static EnemyRecord FindEnemy(string id)=>Array.Find(Enemies,x=>x.Id==id); public static EncounterRecord FindEncounter(string id)=>Array.Find(Encounters,x=>x.StageId==id);
 static HeroRecord H(string id,string n,string f,string r,float hp,float a,float sp,float ar,float mr,float asp,int range,int e,float c,string sk)=>new HeroRecord{Id=id,Name=n,Faction=f,Role=r,HP=hp,Attack=a,SkillPower=sp,Armor=ar,MR=mr,AttackSpeed=asp,Range=range,MaxEnergy=e,CritChance=c,SkillId=sk};
 static EnemyRecord E(string id,string n,string r,float hp,float a,float ar,float mr,float asp,int range)=>new EnemyRecord{Id=id,Name=n,Role=r,HP=hp,Attack=a,Armor=ar,MR=mr,AttackSpeed=asp,Range=range};
 static SpawnRecord S(string id,int x,int y)=>new SpawnRecord(id,x,y); static WaveRecord W(string id,params SpawnRecord[] s)=>new WaveRecord(id,s);
 static EncounterRecord Enc(string id,int cap,bool boss,string guest,string[] artifacts,params WaveRecord[] waves)=>new EncounterRecord{StageId=id,BoardCapacity=cap,Boss=boss,GuestHeroId=guest,ArtifactChoices=artifacts,Waves=waves};
}}
