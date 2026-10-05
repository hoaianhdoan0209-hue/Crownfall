using UnityEngine;
namespace Crownfall.Artifacts { public enum ArtifactKind{WarDrum,ArcaneCrown,BloodChalice,EmberCrown,VenomFlask,ShadowMirror,HolyBell,OathboundShield} [CreateAssetMenu(menuName="Crownfall/Artifact Definition")] public sealed class ArtifactDefinition:ScriptableObject { public string artifactId; public string displayName; public ArtifactKind kind; } }
