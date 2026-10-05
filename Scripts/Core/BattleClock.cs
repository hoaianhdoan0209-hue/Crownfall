using UnityEngine;

namespace Crownfall.Core
{
    public sealed class BattleClock
    {
        public float Speed { get; private set; } = 1f;
        public bool IsPaused { get; private set; }
        public float DeltaTime => IsPaused ? 0f : Time.deltaTime * Speed;
        public void SetSpeed(float speed) => Speed = Mathf.Clamp(speed, 0.1f, 5f);
        public void SetPaused(bool paused) => IsPaused = paused;
    }
}
