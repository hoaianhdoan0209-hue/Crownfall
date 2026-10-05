#if UNITY_INCLUDE_TESTS
using Crownfall.Battle.Combat; using NUnit.Framework;
namespace Crownfall.Tests { public sealed class SkillStatusTests { [Test] public void NegativeArmor_IncreasesDamage(){Assert.Greater(DamageSystem.Mitigate(100,-50),100);} [Test] public void Armor_ReducesDamage(){Assert.Less(DamageSystem.Mitigate(100,50),100);} } }
#endif
