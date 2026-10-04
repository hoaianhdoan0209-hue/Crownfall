using UnityEngine;
using UnityEngine.SceneManagement;

namespace Crownfall.Core
{
    /// <summary>Creates the persistent Crownfall runtime even while authored scenes are still shells.</summary>
    public static class RuntimeEntryPoint
    {
        private const string HostName = "Crownfall_Runtime";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void BeforeSceneLoad()
        {
            RuntimeServices.Initialize();
            if (GameObject.Find(HostName) != null) return;
            var host = new GameObject(HostName);
            Object.DontDestroyOnLoad(host);
            host.AddComponent<RuntimeSceneComposer>();
        }
    }
}
