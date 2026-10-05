using UnityEngine;

namespace Crownfall.Core
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        private static GameBootstrap _instance;
        public static GameBootstrap Instance => _instance;
        public BattleClock Clock { get; private set; }
        private void Awake()
        {
            if (_instance != null && _instance != this) { Destroy(gameObject); return; }
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Clock = new BattleClock();
            RuntimeServices.Initialize();
        }
    }
}
