using NUnit.Framework; using System.IO;
namespace Crownfall.Tests.Stabilization { public sealed class RuntimeBossHookInvariantTests {
 [Test] public void RuntimeSession_ContainsCanonicalGorrukHooks(){var p=Path.Combine(UnityEngine.Application.dataPath,"_Crownfall/Scripts/Battle/RuntimeBattleSession.cs");var s=File.ReadAllText(p);StringAssert.Contains("AttachGorruk",s);StringAssert.Contains("CanonicalIds.Gorruk",s);StringAssert.Contains("CanonicalIds.Archer",s);StringAssert.Contains("BossEnraged",s);}
}}
