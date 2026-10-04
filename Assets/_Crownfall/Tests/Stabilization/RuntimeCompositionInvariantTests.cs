using Crownfall.Core;
using NUnit.Framework;

namespace Crownfall.Tests.Stabilization
{
    public sealed class RuntimeCompositionInvariantTests
    {
        [Test]
        public void CanonicalSceneOrder_RemainsLocked()
        {
            CollectionAssert.AreEqual(new[] { "00_Bootstrap", "01_MainMenu", "02_WorldMap", "03_Battle" }, SceneIds.FirstPlayableBuildOrder);
        }

        [Test]
        public void RuntimeCompositionTypes_ArePresent()
        {
            Assert.NotNull(typeof(RuntimeEntryPoint));
            Assert.NotNull(typeof(RuntimeSceneComposer));
        }
    }
}
