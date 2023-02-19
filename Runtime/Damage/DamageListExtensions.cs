using System.Collections.Generic;

namespace KalkuzSystems.Stats
{
  public static class DamageListExtensions
  {
    public static void AddStacked(this List<Damage> list, Damage item)
    {
      for (var i = 0; i < list.Count; i++)
        if (item.DamageType == list[i].DamageType)
        {
          list[i] += item.DamageRange;
          return;
        }
    }

    public static void SubtractStacked(this List<Damage> list, Damage item, bool destroyIfNegative = false)
    {
      for (var i = 0; i < list.Count; i++)
        if (item.DamageType == list[i].DamageType)
        {
          if (destroyIfNegative && list[i].DamageRange.x < item.DamageRange.x)
            list.RemoveAt(i);
          else
            list[i] -= item.DamageRange;
          return;
        }
    }
  }
}