using Crownfall.Save;

namespace Crownfall.Core
{
    public static class RuntimeServices
    {
        public static SaveService Save { get; private set; }
        public static ISceneService Scenes { get; private set; }
        public static AppFlowService Flow { get; private set; }
        public static void Initialize()
        {
            Save ??= new SaveService();
            Scenes ??= new SceneService();
            Flow ??= new AppFlowService(Save, Scenes);
        }
    }
}
