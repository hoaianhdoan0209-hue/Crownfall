#if UNITY_INCLUDE_TESTS
using Crownfall.Core;
using NUnit.Framework;

namespace Crownfall.Tests
{
    public class SeededRandomServiceTests
    {
        [Test]
        public void SameSeedProducesSameSequence()
        {
            var a = new SeededRandomService(12345);
            var b = new SeededRandomService(12345);
            for (int i = 0; i < 100; i++) Assert.AreEqual(a.Range(0, 100000), b.Range(0, 100000));
        }
        [Test]
        public void StreamsAreIndependent()
        {
            var a = new BattleRandomStreams(777);
            var b = new BattleRandomStreams(777);
            a.Combat.Value(); a.Combat.Value();
            Assert.AreEqual(b.Shop.Range(0, 10000), a.Shop.Range(0, 10000));
        }
    }
}
#endif
