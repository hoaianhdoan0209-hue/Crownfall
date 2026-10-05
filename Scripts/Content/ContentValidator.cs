using System.Collections.Generic;
namespace Crownfall.Content {
public static class ContentValidator {
 public static List<string> Validate(){ var errors=new List<string>(); var ids=new HashSet<string>();
  foreach(var h in FirstPlayableContentCatalog.Heroes){ if(string.IsNullOrWhiteSpace(h.Id)||!ids.Add(h.Id)) errors.Add("Invalid/duplicate hero id: "+h.Id); }
  foreach(var e in FirstPlayableContentCatalog.Enemies){ if(string.IsNullOrWhiteSpace(e.Id)||!ids.Add(e.Id)) errors.Add("Invalid/duplicate enemy id: "+e.Id); }
  var enemyIds=new HashSet<string>(); foreach(var e in FirstPlayableContentCatalog.Enemies) enemyIds.Add(e.Id);
  var stageIds=new HashSet<string>(); foreach(var enc in FirstPlayableContentCatalog.Encounters){ if(!stageIds.Add(enc.StageId)) errors.Add("Duplicate stage: "+enc.StageId); if(enc.BoardCapacity<1||enc.BoardCapacity>7) errors.Add("Invalid board cap: "+enc.StageId); if(enc.Waves==null||enc.Waves.Length==0) errors.Add("No waves: "+enc.StageId); else foreach(var w in enc.Waves) foreach(var s in w.Spawns) if(!enemyIds.Contains(s.EnemyId)) errors.Add("Missing enemy "+s.EnemyId+" in "+enc.StageId); }
  var g=FirstPlayableContentCatalog.FindEnemy(CanonicalIds.Gorruk); if(g==null||g.HP!=4500||g.Attack!=65||g.Armor!=50||g.MR!=30||g.AttackSpeed!=.75f) errors.Add("Gorruk canonical stats mismatch");
  var boss=FirstPlayableContentCatalog.FindEncounter(Crownfall.Campaign.CampaignStageId.Gorruk); if(boss==null||boss.BoardCapacity!=7||!boss.Boss) errors.Add("Gorruk encounter mismatch");
  return errors; }
}}
