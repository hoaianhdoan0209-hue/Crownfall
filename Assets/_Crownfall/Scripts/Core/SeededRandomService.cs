using System;

namespace Crownfall.Core
{
    public sealed class SeededRandomService : IRandomService
    {
        private readonly Random _random;
        public int Calls { get; private set; }
        public SeededRandomService(int seed) => _random = new Random(seed);
        public int Range(int minInclusive, int maxExclusive) { Calls++; return _random.Next(minInclusive, maxExclusive); }
        public float Value() { Calls++; return (float)_random.NextDouble(); }
    }
}
