#if UNITY_EDITOR
using Crownfall.Core;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Crownfall.EditorTools
{
    public static class FirstPlayableSceneValidator
    {
        [MenuItem("Crownfall/Validate First Playable Scenes")]
        public static void Validate()
        {
            var configured = EditorBuildSettings.scenes;
            var expected = SceneIds.FirstPlayableBuildOrder;
            if (configured.Length < expected.Length) throw new System.InvalidOperationException("First Playable requires four build scenes.");
            for (int i = 0; i < expected.Length; i++)
            {
                var path = configured[i].path;
                if (!configured[i].enabled || !System.IO.Path.GetFileNameWithoutExtension(path).Equals(expected[i]))
                    throw new System.InvalidOperationException($"Build scene {i} must be enabled and named {expected[i]}.");
            }
            Debug.Log("Crownfall First Playable scene order is valid.");
        }
    }
}
#endif
