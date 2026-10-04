namespace Crownfall.Core
{
    public static class SceneIds
    {
        public const string Bootstrap = "00_Bootstrap";
        public const string MainMenu = "01_MainMenu";
        public const string WorldMap = "02_WorldMap";
        public const string Battle = "03_Battle";
        public static readonly string[] FirstPlayableBuildOrder = { Bootstrap, MainMenu, WorldMap, Battle };
    }
}
