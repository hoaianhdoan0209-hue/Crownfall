using Crownfall.Battle.Combat; using NUnit.Framework;
namespace Crownfall.Tests.EditMode {
public sealed class CombatCoreTests {
 [Test] public void Armor100_HalvesPhysicalDamage(){Assert.That(DamageSystem.Mitigate(100,100),Is.EqualTo(50).Within(.001));}
 [Test] public void NegativeArmor_IncreasesDamage(){Assert.That(DamageSystem.Mitigate(100,-50),Is.EqualTo(133.333f).Within(.01));}
 [Test] public void NegativeArmor_IsClampedAtMinus80(){Assert.That(DamageSystem.Mitigate(100,-999),Is.EqualTo(DamageSystem.Mitigate(100,-80)).Within(.001));}
}
}
