using UnityEngine;

namespace KalkuzSystems.Stats
{
  public abstract class BaseCharacterStats : MonoBehaviour
  {
    [SerializeField] private bool autoInitialize;
    [SerializeField] private CharacterStatsData baseStats;

    private StatModifiers statModifiers;

    public CharacterStatsData CurrentStats { get; private set; }

    public bool IsDied { get; private set; }

    private void Awake()
    {
      if (autoInitialize) Initialize();
    }

    public void Initialize()
    {
      CurrentStats = baseStats.Copy();

      IsDied = false;
    }

    public bool ShouldDodge(float accuracy)
    {
      return accuracy < Mathf.Clamp(Random.value, 0, 0.99f);
    }

    public void ApplyDamage(Damage damage, float critChance, float critMultiplier)
    {
      var isCrit = Mathf.Clamp(Random.value, 0, 0.99f) < critChance;

      var resistanceAmount = CurrentStats.CharacterResistances.Find(i => i.ResistantTo == damage.DamageType).PercentAmount;

      var netDamage = damage * (1f - resistanceAmount);
      if (isCrit) netDamage *= critMultiplier;

      var sampledDamage = netDamage.Sample();
      ChangeHealth(-sampledDamage);
    }

    public void ApplyHealing()
    {
    }

    private void Die()
    {
      IsDied = true;
    }

    private void ChangeHealth(float changeAmount)
    {
      var resources = CurrentStats.CharacterResources;

      var baseHealthAmount = baseStats.CharacterResources.Find(i => i.ResourceType == CharacterResourceType.HEALTH).Amount;

      var currentHealthIndex = resources.FindIndex(i => i.ResourceType == CharacterResourceType.HEALTH);
      var currentHealthAmount = resources[currentHealthIndex].Amount;

      var newHealthAmount = Mathf.Clamp(currentHealthAmount + changeAmount, 0f, baseHealthAmount);

      resources[currentHealthIndex] = resources[currentHealthIndex].SetAmount(newHealthAmount);

      if (newHealthAmount == 0f) Die();
    }
  }
}