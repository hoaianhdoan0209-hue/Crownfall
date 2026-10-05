using System; using System.Collections.Generic;
namespace Crownfall.Bosses {
public sealed class BossPhaseController {
 readonly HashSet<string> fired=new(); public event Action<string> PhaseTriggered;
 public bool Evaluate(string phaseId,float threshold,float healthPercent){if(string.IsNullOrEmpty(phaseId)||healthPercent>threshold||fired.Contains(phaseId))return false;fired.Add(phaseId);PhaseTriggered?.Invoke(phaseId);return true;}
 public bool HasTriggered(string phaseId)=>fired.Contains(phaseId); public void Reset()=>fired.Clear();
}}
