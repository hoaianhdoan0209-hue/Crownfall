using Crownfall.Core;
using Crownfall.Preparation;
using NUnit.Framework;

namespace Crownfall.Tests.Stabilization
{
    public sealed class RuntimePreparationIntegrationTests
    {
        [Test]
        public void FormationPlan_RespectsCapacity_AndRequiresDeployment()
        {
            var plan = new RuntimeFormationPlan(2);
            Assert.IsFalse(plan.CanStartCombat);
            Assert.IsTrue(plan.TryToggle("hero_a"));
            Assert.IsTrue(plan.CanStartCombat);
            Assert.IsTrue(plan.TryToggle("hero_b"));
            Assert.IsFalse(plan.TryToggle("hero_c"));
            Assert.AreEqual(2, plan.Count);
        }

        [Test]
        public void FormationPlan_RepackingKeepsPlayerSideCoordinatesStable()
        {
            var plan = new RuntimeFormationPlan(3);
            plan.TryToggle("hero_a"); plan.TryToggle("hero_b"); plan.TryToggle("hero_c");
            plan.TryToggle("hero_b");
            Assert.AreEqual(2, plan.Count);
            Assert.AreEqual(new GridCoordinate(0, 0), plan.Slots[0].Cell);
            Assert.AreEqual(new GridCoordinate(1, 0), plan.Slots[1].Cell);
        }
    }
}
