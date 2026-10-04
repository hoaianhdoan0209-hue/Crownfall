using System; using Crownfall.Battle.Grid; using Crownfall.Core; using Crownfall.StatusEffects; using UnityEngine;
namespace Crownfall.Units {
public sealed class Unit : MonoBehaviour {
 public UnitDefinition Definition{get;private set;} public TeamId Team{get;private set;} public UnitState State{get;private set;}=UnitState.Idle;
 public float CurrentHealth{get;private set;} public UnitEnergy Energy{get;private set;} public ShieldSystem Shields{get;private set;} public StatusContainer Statuses{get;private set;} public bool IsDead=>State==UnitState.Dead; public GridCell CurrentCell{get;private set;} public Unit CurrentTarget{get;set;}
 public float HealthPercent=>Definition==null?0:CurrentHealth/Definition.maxHealth; public event Action<Unit> Died;
 public void Initialize(UnitDefinition d,TeamId t){Definition=d;Team=t;CurrentHealth=d.maxHealth;Energy=new UnitEnergy(d.maxEnergy);Shields=new ShieldSystem();Statuses=new StatusContainer(this);State=UnitState.Idle;}
 public bool SetInitialCell(GridCell cell){if(cell==null||!cell.TryOccupy(this))return false;CurrentCell=cell;transform.position=cell.WorldPosition;return true;}
 public bool EnterCell(GridCell cell){if(cell==null||!cell.TryOccupy(this))return false;var old=CurrentCell;CurrentCell=cell;old?.Leave(this);transform.position=cell.WorldPosition;return true;}
 public void ApplyHealthDamage(float value){if(IsDead)return;float applied=Mathf.Max(0,value);CurrentHealth=Mathf.Max(0,CurrentHealth-applied);if(applied>0)Energy?.Gain(Definition.energyPerHitTaken);if(CurrentHealth<=0)Die();}
 public void Heal(float value){if(IsDead)return;CurrentHealth=Mathf.Min(Definition.maxHealth,CurrentHealth+Mathf.Max(0,value));}
 public bool CanMove=>!IsDead&&!Statuses.Has(StatusType.Stun)&&!Statuses.Has(StatusType.Root); public bool CanBasicAttack=>!IsDead&&!Statuses.Has(StatusType.Stun)&&!Statuses.Has(StatusType.Disarm); public bool CanCastSkill=>!IsDead&&!Statuses.Has(StatusType.Stun)&&!Statuses.Has(StatusType.Silence);
 public void SetState(UnitState s){if(!IsDead)State=s;} void Die(){if(IsDead)return;State=UnitState.Dead;CurrentCell?.Leave(this);CurrentCell=null;CurrentTarget=null;Shields?.Clear();Statuses?.Clear();Died?.Invoke(this);}
}}
