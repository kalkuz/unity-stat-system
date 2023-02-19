using System.Collections.Generic;
using UnityEngine;

namespace KalkuzSystems.Stats
{
  [CreateAssetMenu(menuName = "Kalkuz/Character Stats")]
  public class CharacterStatsData : ScriptableObject
  {
    [SerializeField] private string characterName;
    [SerializeField] private List<CharacterResource> characterResources;
    [SerializeField] private List<CharacterResistance> characterResistances;

    [SerializeField] [Space(20f)] private float movementSpeed;

    public string CharacterName => characterName;
    public List<CharacterResource> CharacterResources => characterResources;
    public List<CharacterResistance> CharacterResistances => characterResistances;

    public float MovementSpeed => movementSpeed;

    public virtual CharacterStatsData Copy()
    {
      var obj = Instantiate(this);

      obj.characterResources = new List<CharacterResource>(characterResources);
      obj.characterResistances = new List<CharacterResistance>(characterResistances);

      return obj;
    }
  }
}