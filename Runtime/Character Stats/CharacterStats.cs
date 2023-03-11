using UnityEngine;
using UnityEngine.Events;

namespace KalkuzSystems.Stats
{
  public class CharacterStats : MonoBehaviour
  {
    [SerializeField] private bool autoInitialize;
    [SerializeField] private CharacterStatsData baseStats;

    [SerializeField] private UnityEvent onSpawn;
    [SerializeField] private UnityEvent onDodge;
    [SerializeField] private UnityEvent onDamaged;
    [SerializeField] private UnityEvent onCriticallyDamaged;
    [SerializeField] private UnityEvent onHeal;
    [SerializeField] private UnityEvent onDeath;

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
      onSpawn?.Invoke();
    }

    public bool ShouldDodge(float accuracy)
    {
      var isDodge = accuracy < Mathf.Clamp(Random.value, 0, 0.99f);
      if (isDodge) onDodge?.Invoke();

      return isDodge;
    }

    public void ApplyDamage(Damage damage, float critChance, float critMultiplier)
    {
      var isCrit = Mathf.Clamp(Random.value, 0, 0.99f) < critChance;

      var resistanceAmount =
        CurrentStats.CharacterResistances.Find(i => i.ResistantTo == damage.DamageType).PercentAmount;

      var netDamage = damage * (1f - resistanceAmount);
      if (isCrit) netDamage *= critMultiplier;

      var sampledDamage = netDamage.Sample();
      if (sampledDamage >= 0f)
      {
        if (isCrit)
        {
          onCriticallyDamaged?.Invoke();
        }
        else
        {
          onDamaged?.Invoke();
        }

        ChangeHealth(-sampledDamage);
      }
      else
      {
        ApplyHealing(-sampledDamage);
      }
    }

    public void ApplyHealing(float healAmount)
    {
      onHeal?.Invoke();
      ChangeHealth(healAmount);
    }

    private void Die()
    {
      IsDied = true;
      onDeath?.Invoke();
    }

    private void ChangeHealth(float changeAmount)
    {
      var resources = CurrentStats.CharacterResources;

      var baseHealthAmount =
        baseStats.CharacterResources.Find(i => i.ResourceType == CharacterResourceType.HEALTH).Amount;

      var currentHealthIndex = resources.FindIndex(i => i.ResourceType == CharacterResourceType.HEALTH);
      var currentHealthAmount = resources[currentHealthIndex].Amount;

      var newHealthAmount = Mathf.Clamp(currentHealthAmount + changeAmount, 0f, baseHealthAmount);

      resources[currentHealthIndex] = resources[currentHealthIndex].SetAmount(newHealthAmount);

      if (newHealthAmount == 0f) Die();
    }
  }
}