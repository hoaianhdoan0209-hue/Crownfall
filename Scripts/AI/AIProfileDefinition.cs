using UnityEngine;
namespace Crownfall.AI {
public enum TargetRule { Nearest, LowestHealth, HighestHealth, Backline, Farthest, Wounded }
public enum MovementStyle { Advance, HoldRange, OpeningLeap, ScreenFrontline }
[CreateAssetMenu(fileName="AIProfile", menuName="Crownfall/AI/Profile")]
public sealed class AIProfileDefinition : ScriptableObject {
 public string profileId; public TargetRule targetRule=TargetRule.Nearest; public MovementStyle movementStyle=MovementStyle.Advance;
 [Min(.05f)] public float thinkInterval=.15f; [Min(.05f)] public float retargetInterval=.5f; [Min(0)] public int preferredRange=1;
 public bool supportAllies; public bool openingAbilityOnce;
}}
