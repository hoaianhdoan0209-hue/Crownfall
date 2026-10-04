using System; using Crownfall.Units;
namespace Crownfall.Preparation {
[Serializable] public sealed class BattleHeroInstance {
 public string InstanceId{get;} public UnitDefinition Definition{get;} public int StarLevel{get;private set;}
 public BattleHeroInstance(UnitDefinition definition,int starLevel=1,string instanceId=null){Definition=definition??throw new ArgumentNullException(nameof(definition));StarLevel=Math.Max(1,Math.Min(5,starLevel));InstanceId=string.IsNullOrWhiteSpace(instanceId)?Guid.NewGuid().ToString("N"):instanceId;}
 public bool CanMergeWith(BattleHeroInstance other,int maxStar=5)=>other!=null&&!ReferenceEquals(this,other)&&Definition==other.Definition&&StarLevel==other.StarLevel&&StarLevel<maxStar;
 public void UpgradeStar(int maxStar=5){if(StarLevel>=maxStar)throw new InvalidOperationException("Hero is already at max star.");StarLevel++;}
}}
