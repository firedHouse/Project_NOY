using System.Collections.Generic;
using UnityEngine;

public static class ReactionDamageProcesser
{

    //증발 데미지
    public static void ApplyVaporize(BattleUnit target)
    {
        if(target == null || target.IsDead)
        {  return; }

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

    public static void ApplyOverload(IEnumerable<BattleUnit> enemyTeam)
    {
        foreach(var enemy in enemyTeam)
        {
            if (!enemy.IsDead)
            enemy.TakeDamage(enemy.MaxHP * 0.04f);
            Debug.Log("과부하 데미지");
        }
    }
}
