using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KalkuzSystems.Stats
{
  [Serializable]
  public struct Damage
  {
    [SerializeField] private Vector2 damageRange;
    [SerializeField] private DamageType damageType;

    public Vector2 DamageRange => damageRange;
    public DamageType DamageType => damageType;

    public float Sample()
    {
      return Random.Range(damageRange.x, damageRange.y);
    }

    public Damage Convert(DamageType newType, float percentage)
    {
      return new Damage
      {
        damageRange = damageRange * percentage,
        damageType = newType
      };
    }

    public static Damage operator +(Damage lhs, Vector2 rhs)
    {
      lhs.damageRange += rhs;
      return lhs;
    }

    public static Damage operator -(Damage lhs, Vector2 rhs)
    {
      return lhs + -rhs;
    }

    public static Damage operator *(Damage lhs, float rhs)
    {
      lhs.damageRange *= rhs;
      return lhs;
    }

    public static Damage operator /(Damage lhs, float rhs)
    {
      return lhs * (1f / rhs);
    }
  }
}