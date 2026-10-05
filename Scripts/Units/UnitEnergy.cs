using System;
using UnityEngine;
namespace Crownfall.Units {
public sealed class UnitEnergy {
 public float Current{get;private set;} public float Max{get;}
 public event Action<float,float> Changed;
 public UnitEnergy(float max){Max=Mathf.Max(1,max);}
 public void Gain(float value){if(value<=0)return;Current=Mathf.Min(Max,Current+value);Changed?.Invoke(Current,Max);}
 public bool TrySpend(float value){if(value<0||Current<value)return false;Current-=value;Changed?.Invoke(Current,Max);return true;}
 public void Reset(){Current=0;Changed?.Invoke(Current,Max);}
}}
