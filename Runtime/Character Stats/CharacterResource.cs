using System;
using UnityEngine;

namespace KalkuzSystems.Stats
{

  [Serializable]
  public struct CharacterResource
  {
    [SerializeField] private CharacterResourceType resourceType;
    [SerializeField] private float amount;

    public CharacterResourceType ResourceType => resourceType;
    public float Amount => amount;

    public CharacterResource SetAmount(float amount)
    {
      this.amount = amount;
      return this;
    }
  }
}