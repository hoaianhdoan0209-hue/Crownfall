using System; using Crownfall.Core; using Crownfall.Units;
namespace Crownfall.Bosses {
public sealed class GorrukBossController {
 readonly Unit unit; readonly BattleClock clock; readonly BossPhaseController phases=new(); float drRemaining; bool enrage;
 public event Action ReinforcementsRequested; public event Action Enraged; public event Action<float> GroundBreakerTelegraph;
 public GorrukBossController(Unit unit,BattleClock clock){this.unit=unit;this.clock=clock;}
 public bool ReinforcementPhaseTriggered=>phases.HasTriggered("gorruk_70"); public bool EnragePhaseTriggered=>phases.HasTriggered("gorruk_30"); public float DamageReduction=>drRemaining>0?GorrukDefinition.ReinforcementDamageReduction:0f; public bool IsEnraged=>enrage;
 public void Tick(){if(unit==null||unit.IsDead)return;float hp=unit.HealthPercent;if(phases.Evaluate("gorruk_70",GorrukDefinition.ReinforcementThreshold,hp)){drRemaining=GorrukDefinition.ReinforcementDamageReductionSeconds;ReinforcementsRequested?.Invoke();}if(phases.Evaluate("gorruk_30",GorrukDefinition.EnrageThreshold,hp)){enrage=true;Enraged?.Invoke();}if(drRemaining>0)drRemaining=Math.Max(0,drRemaining-(clock?.DeltaTime??0));}
 public float ReduceIncomingDamage(float damage)=>Math.Max(0,damage*(1f-DamageReduction));
 public void BeginGroundBreaker()=>GroundBreakerTelegraph?.Invoke(GorrukDefinition.GroundBreakerTelegraphSeconds);
}}
