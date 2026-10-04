#if UNITY_EDITOR
using Crownfall.Bosses; using NUnit.Framework;
public sealed class BossPhaseControllerTests {
 [Test] public void ThresholdTriggersOnlyOnce(){var p=new BossPhaseController();Assert.IsTrue(p.Evaluate("p70",.7f,.69f));Assert.IsFalse(p.Evaluate("p70",.7f,.2f));}
 [Test] public void CrossingBothThresholdsCanTriggerBothOnce(){var p=new BossPhaseController();Assert.IsTrue(p.Evaluate("p70",.7f,.2f));Assert.IsTrue(p.Evaluate("p30",.3f,.2f));Assert.IsFalse(p.Evaluate("p70",.7f,.1f));Assert.IsFalse(p.Evaluate("p30",.3f,.1f));}
}
#endif
