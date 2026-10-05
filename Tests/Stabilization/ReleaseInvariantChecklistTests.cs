using NUnit.Framework;
using Crownfall.Content;

namespace Crownfall.Tests.Stabilization
{
    public sealed class ReleaseInvariantChecklistTests
    {
        [Test]
        public void CanonicalContent_ValidatesForFirstPlayable()
        {
            var errors = ContentValidator.Validate();
            Assert.IsEmpty(errors, string.Join("\n", errors));
        }

        [Test]
        public void Gorruk_RemainsCanonical()
        {
            var boss = FirstPlayableContentCatalog.FindEnemy(CanonicalIds.Gorruk);
            Assert.IsNotNull(boss);
            Assert.AreEqual(4500, boss.HP);
            Assert.AreEqual(65, boss.Attack);
            Assert.AreEqual(50, boss.Armor);
            Assert.AreEqual(30, boss.MR);
        }
    }
}
