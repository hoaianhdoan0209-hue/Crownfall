using System.Collections.Generic; using Crownfall.AI; using Crownfall.Battle.Combat; using Crownfall.Battle.Grid; using Crownfall.Core; using Crownfall.Units; using UnityEngine;
namespace Crownfall.Battle {
public sealed class BattleController : MonoBehaviour {
 [SerializeField] GridSystem grid; public GridSystem Grid=>grid; public BattleState State{get;private set;}=BattleState.Loading; public IReadOnlyList<Unit> Units=>units;
 readonly List<Unit> units=new(); readonly Dictionary<Unit,Crownfall.AI.UnitBrain> brains=new(); BattleClock clock; public Pathfinder Pathfinder{get;private set;} public TargetingSystem Targeting{get;private set;} public DamageSystem Damage{get;private set;}
 public void Initialize(IRandomService combatRng){clock=new BattleClock();Pathfinder=new Pathfinder(grid);Targeting=new TargetingSystem(grid);Damage=new DamageSystem(combatRng);State=BattleState.Preparation;}
 public void Register(Unit u){if(u==null||units.Contains(u))return;units.Add(u);u.Died+=OnUnitDied;}
 public void RegisterBrain(Unit u,Crownfall.AI.UnitBrain brain){if(u!=null&&brain!=null)brains[u]=brain;}
 public BattleClock Clock=>clock;
 void Update(){if(State!=BattleState.Combat)return;for(int i=0;i<units.Count;i++){var u=units[i];if(u==null||u.IsDead)continue;if(brains.TryGetValue(u,out var b)&&b!=null)b.Tick();}}
 public void StartCombat(){if(State==BattleState.Preparation)State=BattleState.Combat;}
 public void PrepareNextWave(){if(State!=BattleState.Victory)return;for(int i=units.Count-1;i>=0;i--){var u=units[i];if(u==null||u.IsDead){if(u!=null)u.Died-=OnUnitDied;brains.Remove(u);units.RemoveAt(i);}}State=BattleState.Preparation;}
 void OnUnitDied(Unit u){if(brains.TryGetValue(u,out var b))b?.Stop();CheckResult();}
 void CheckResult(){if(State!=BattleState.Combat)return;bool p=false,e=false;foreach(var u in units){if(u==null||u.IsDead)continue;if(u.Team==TeamId.Player)p=true;else if(u.Team==TeamId.Enemy)e=true;}if(!e)State=BattleState.Victory;else if(!p)State=BattleState.Defeat;}
}
}
