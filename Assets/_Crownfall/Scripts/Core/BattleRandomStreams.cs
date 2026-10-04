namespace Crownfall.Core
{
    public sealed class BattleRandomStreams
    {
        public IRandomService Combat { get; }
        public IRandomService Summon { get; }
        public IRandomService Shop { get; }
        public IRandomService Loot { get; }
        public BattleRandomStreams(int battleSeed)
        {
            Combat = new SeededRandomService(Derive(battleSeed, 101));
            Summon = new SeededRandomService(Derive(battleSeed, 211));
            Shop = new SeededRandomService(Derive(battleSeed, 307));
            Loot = new SeededRandomService(Derive(battleSeed, 401));
        }
        private static int Derive(int seed, int salt) => unchecked((seed * 486187739) ^ salt);
    }
}
