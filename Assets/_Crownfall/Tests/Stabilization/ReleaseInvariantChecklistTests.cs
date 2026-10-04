using NUnit.Framework;
using Crownfall.Content;

namespace Crownfall.Tests.Stabilization
{
    public sealed class ReleaseInvariantChecklistTests
    {
        [Test]
        public void CanonicalContent_ValidatesForFirstPlayable()
        {
            var catalog = Chapter1ContentCatalog.CreateCanonical();
            var result = ContentValidator.Validate(catalog);
            Assert.IsTrue(result.IsValid, string.Join("\n", result.Errors));
        }

        [Test]
        public void Gorruk_RemainsCanonical()
        {
            var catalog = Chapter1ContentCatalog.CreateCanonical();
            var boss = catalog.GetEnemy("boss_gorruk");
            Assert.AreEqual(4500, boss.MaxHealth);
            Assert.AreEqual(65, boss.Attack);
            Assert.AreEqual(50, boss.Armor);
            Assert.AreEqual(30, boss.MagicResistance);
        }
    }
}
