using UnityEngine; namespace Crownfall.Skills { public abstract class SkillEffectDefinition:ScriptableObject { public abstract void Execute(SkillContext context,Crownfall.Units.Unit target); } }
