using System.Collections.Generic;
using UnityEngine;

public static class ReactionDamageProcesser
{

    //증발 데미지
    public static void ApplyVaporize(BattleUnit target)
    {
        target.TakeDamage(target.MaxHP * 0.1f);
        Debug.Log("증발 데미지");
    }
 
    public static void ApplyElectroShock(IEnumerable<BattleUnit> enemyTeam)
    {
        foreach (var enemy in enemyTeam)
        {
            if (!enemy.IsDead)
            enemy.TakeDamage(enemy.MaxHP * 0.07f);
            Debug.Log("감전 데미지");

        }
    }

    public static void Overload(IEnumerable<BattleUnit> enemyTeam)
    {
        foreach(var enemy in enemyTeam)
        {
            if (!enemy.IsDead)
            enemy.TakeDamage(enemy.MaxHP * 0.04f);
            Debug.Log("과부하 즉시 데미지");
        }
    }
}
