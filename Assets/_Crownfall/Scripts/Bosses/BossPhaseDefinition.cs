using System; using UnityEngine;
namespace Crownfall.Bosses {
[Serializable] public sealed class BossPhaseDefinition { [Range(0,1)] public float healthThreshold=1f; public string phaseId; public bool triggerOnce=true; }
}
