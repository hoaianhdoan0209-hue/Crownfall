using System.Collections.Generic; using UnityEngine;
namespace Crownfall.Campaign {
[CreateAssetMenu(fileName="Encounter", menuName="Crownfall/Campaign/Encounter")]
public sealed class EncounterDefinition : ScriptableObject { public string encounterId; public List<WaveDefinition> waves=new(); public bool bossEncounter; }
}
