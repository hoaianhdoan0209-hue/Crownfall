namespace Crownfall.Preparation {
public sealed class MergeService {
 readonly int maxStar; public MergeService(int maxStar=5){this.maxStar=maxStar;}
 public BattleHeroInstance Merge(BattleHeroInstance a,BattleHeroInstance b){if(a==null||!a.CanMergeWith(b,maxStar))return null;return new BattleHeroInstance(a.Definition,a.StarLevel+1);}
}}
