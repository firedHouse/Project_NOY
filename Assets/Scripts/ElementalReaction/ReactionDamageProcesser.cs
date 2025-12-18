using UnityEngine;

public static class ReactionDamageProcesser
{
    public static void Apply(ElementReaction reaction, BattleUnit hitTarget, BattleUnit[] enemyTeam)
    {
        switch (reaction)
        {
            case ElementReaction.Varporize:
                ApplyVaporize(hitTarget);
                Debug.Log("증발 발생");
                break;
            case ElementReaction.ElectricShock:
                ApplyElectroShock(hitTarget, enemyTeam);
                Debug.Log("감전 발생");
                break;
            case ElementReaction.Overload:
                ApplyOverload(hitTarget, enemyTeam);
                Debug.Log("과부하 발생");
                break;
        }
    }
    //각 원소 발생 데미지 메서드들 -> 과부하는 턴마다 데미지이나, 이는 다른 곳에서 처리해볼 생각이다.
    //메서드가 아닌 그냥 스위치 문에 넣어도 되나, 이후 수정 및 가독성을 위해 메서드로 분리했다.
    private static void ApplyVaporize(BattleUnit target)
    {
        float damage = target.MaxHP * 0.1f;
        target.TakeDamage(damage);
    }
 
    private static void ApplyElectroShock(BattleUnit target, BattleUnit[] enemyTeam)
    { 
        foreach (var enemy in enemyTeam)
        {
            if (enemy != target && !enemy.IsDead)
            {
                enemy.TakeDamage(enemy.MaxHP * 0.07f);
            }
        }
    }
 
    private static void ApplyOverload(BattleUnit target, BattleUnit[] enemyTeam)
    {
        foreach (var enemy in enemyTeam)
        {
            if (enemy != target && !enemy.IsDead)
            {
                enemy.TakeDamage(enemy.MaxHP * 0.04f);
            }
        }
    }
}
