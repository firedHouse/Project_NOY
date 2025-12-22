using System.Collections.Generic;
using UnityEngine;

public static class ReactionDamageProcesser
{

    //각 원소 발생 데미지 메서드들 -> 과부하는 턴마다 데미지이나, 이는 다른 곳에서 처리해볼 생각이다.
    public static void ApplyVaporize(BattleUnit target)
    {
        target.TakeDamage(target.MaxHP * 0.1f);
    }
 
    public static void ApplyElectroShock(IEnumerable<BattleUnit> enemyTeam)
    {
        foreach (var enemy in enemyTeam)
        {
            if (!enemy.IsDead)
            enemy.TakeDamage(enemy.MaxHP * 0.07f);
            
        }
    }

    // 과부하 상태일 때 즉시 발동할 과부하 데미지
    public static void ApplyOverloadNow(IEnumerable<BattleUnit> enemyTeam)
    {
        foreach(var enemy in enemyTeam)
        {
            if (!enemy.IsDead)
            enemy.TakeDamage(enemy.MaxHP * 0.04f);
        }
    }

    /// 과부하 상태일 때 턴마다 입히는 데미지
    public static void ApplyOverloadDot(IEnumerable<BattleUnit> enemyTeam)
    {
        foreach (var enemy in enemyTeam)
        {
            if (!enemy.IsDead)
            enemy.TakeDamage(enemy.MaxHP * 0.04f);
        }
    }
}
