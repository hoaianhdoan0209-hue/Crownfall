using Crownfall.Battle;
using Crownfall.Campaign;
using Crownfall.Content;
using NUnit.Framework;

namespace Crownfall.Tests.Stabilization
{
    public sealed class RuntimeBattleIntegrationTests
    {
        [Test] public void EveryFirstPlayableEncounter_HasAtLeastOneWaveAndSpawn()
        {
            foreach(var e in FirstPlayableContentCatalog.Encounters){Assert.NotNull(e.Waves,e.StageId);Assert.Greater(e.Waves.Length,0,e.StageId);foreach(var w in e.Waves){Assert.NotNull(w.Spawns,w.Id);Assert.Greater(w.Spawns.Length,0,w.Id);}}
        }
        [Test] public void EverySpawn_ResolvesToEnemyContent()
        {
            foreach(var e in FirstPlayableContentCatalog.Encounters) foreach(var w in e.Waves) foreach(var s in w.Spawns) Assert.NotNull(FirstPlayableContentCatalog.FindEnemy(s.EnemyId),s.EnemyId);
        }
        [Test] public void RuntimeBattleBridgeTypes_ArePresent(){Assert.NotNull(typeof(RuntimeBattleSession));Assert.NotNull(typeof(RuntimeContentFactory));}
    }
}
