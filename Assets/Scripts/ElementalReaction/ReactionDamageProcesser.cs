using System.Collections.Generic;
using UnityEngine;

public static class ReactionDamageProcesser
{
    //증발 데미지
    public static void ApplyVaporize(BattleUnit target)
    {
        if (target == null || target.IsDead)
        { 
            return; 
        }
        float dmg = target.MaxHP * 0.1f;
        target.TakeDamage(dmg);
        Debug.Log($"증발: {target.UnitName} 에게 {dmg} 데미지 ");
    }

    public static void ApplyElectroShock(IEnumerable<BattleUnit> enemyTeam)
    {
        foreach (var enemy in enemyTeam)
        {
            if (!enemy.IsDead)
            {
                float dmg = enemy.MaxHP * 0.07f;
                enemy.TakeDamage(dmg);
                Debug.Log($"감전: {enemyTeam} 에게 {dmg}의 광역 데미지");
            }
        }
    }

    //12.23 과부하 광역 -> 단일로 변경
    public static void ApplyOverload(BattleUnit target)
    {
        if (target == null || target.IsDead)
        {
            return;
        }
        //매 턴
        float dmg = target.MaxHP * 0.04f;
        target.TakeDamage(dmg);
        Debug.Log($"과부하: {target.UnitName} 에게 {dmg} 데미지");
    }
}
