#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Crownfall.Editor
{
    public static class CrownfallWindowsBuilder
    {
        private static readonly string[] ReleaseScenes =
        {
            "Assets/_Crownfall/Scenes/00_Bootstrap.unity",
            "Assets/_Crownfall/Scenes/01_MainMenu.unity",
            "Assets/_Crownfall/Scenes/02_WorldMap.unity",
            "Assets/_Crownfall/Scenes/03_Battle.unity"
        };

        [MenuItem("Crownfall/Build/Prepare Release Settings")]
        public static void PrepareReleaseSettings()
        {
            ValidateReleaseScenes();
            EditorBuildSettings.scenes = ReleaseScenes
                .Select(path => new EditorBuildSettingsScene(path, true))
                .ToArray();

            PlayerSettings.companyName = "Crownfall";
            PlayerSettings.productName = "Crownfall";
            PlayerSettings.bundleVersion = "1.0.0";
            PlayerSettings.SetScriptingBackend(NamedBuildTarget.Standalone, ScriptingImplementation.IL2CPP);
            PlayerSettings.SetArchitecture(NamedBuildTarget.Standalone, 1); // x86_64
            AssetDatabase.SaveAssets();
            Debug.Log("Crownfall release settings prepared. Scenes: " + string.Join(", ", ReleaseScenes));
        }

        [MenuItem("Crownfall/Build/Windows x64 Release")]
        public static void BuildWindowsRelease()
        {
            PrepareReleaseSettings();

            string root = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string outDir = Path.Combine(root, "Builds", "Windows");
            Directory.CreateDirectory(outDir);
            string exePath = Path.Combine(outDir, "Crownfall.exe");

            var options = new BuildPlayerOptions
            {
                scenes = ReleaseScenes,
                locationPathName = exePath,
                target = BuildTarget.StandaloneWindows64,
                targetGroup = BuildTargetGroup.Standalone,
                options = BuildOptions.StrictMode
            };

            BuildReport report = BuildPipeline.BuildPlayer(options);
            if (report.summary.result != BuildResult.Succeeded)
                throw new Exception("Crownfall Windows build failed: " + report.summary.result +
                                    ". Errors: " + report.summary.totalErrors);

            if (!File.Exists(exePath))
                throw new FileNotFoundException("Unity reported success but Crownfall.exe was not found.", exePath);

            Debug.Log($"Crownfall Windows x64 build complete: {exePath} ({report.summary.totalSize} bytes)");
        }

        private static void ValidateReleaseScenes()
        {
            var missing = ReleaseScenes.Where(path => !File.Exists(Path.GetFullPath(path))).ToArray();
            if (missing.Length > 0)
                throw new FileNotFoundException("Missing Crownfall release scene(s): " + string.Join(", ", missing));
        }
    }
}
#endif
