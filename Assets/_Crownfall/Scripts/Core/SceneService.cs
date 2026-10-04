using UnityEngine.SceneManagement;

namespace Crownfall.Core
{
    public interface ISceneService { void Load(string sceneId); }
    public sealed class SceneService : ISceneService
    {
        public void Load(string sceneId)
        {
            if (string.IsNullOrWhiteSpace(sceneId)) throw new System.ArgumentException("Scene id is required.");
            SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
        }
    }
}
