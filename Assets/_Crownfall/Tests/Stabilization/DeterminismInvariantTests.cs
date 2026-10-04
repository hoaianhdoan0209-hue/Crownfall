using NUnit.Framework;
using Crownfall.Core;

namespace Crownfall.Tests.Stabilization
{
    public sealed class DeterminismInvariantTests
    {
        [Test]
        public void SameSeed_ProducesSameSequence()
        {
            var a = new SeededRng(123456);
            var b = new SeededRng(123456);
            for (var i = 0; i < 1000; i++)
                Assert.AreEqual(a.Range(0, 1000000), b.Range(0, 1000000));
        }

        [Test]
        public void DifferentStreams_DoNotShareInstances()
        {
            var streams = new GameplayRngStreams(777);
            Assert.AreNotSame(streams.Combat, streams.Summon);
            Assert.AreNotSame(streams.Summon, streams.Shop);
            Assert.AreNotSame(streams.Shop, streams.Loot);
        }
    }
}
