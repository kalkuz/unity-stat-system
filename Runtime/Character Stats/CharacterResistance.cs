using System;
using UnityEngine;

namespace KalkuzSystems.Stats
{
  [Serializable]
  public struct CharacterResistance
  {
    [SerializeField] private DamageType resistantTo;
    [SerializeField] private float percentAmount;

    public DamageType ResistantTo => resistantTo;
    public float PercentAmount => percentAmount;
  }
}