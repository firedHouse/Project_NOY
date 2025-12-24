using UnityEngine;

public static class RelicApplier
{   //렐릭이 장착됐을 때, 캐릭터한테 능력치를 더해주기 위한 계산 스크립트
    public static void Apply(BattleUnit target, ItemEquipData data)
    {
        if (target == null || data == null)
        {
            Debug.Log("장착할 유닛 혹은 유물의 데이터들 불러올 수 없습니다.");
            return;
        }

        switch(data.RelicStateType)
        {
            case RelicStateType.HPBuff:
                ApplyMaxHPUp(target, data.value);
                break;
            case RelicStateType.SpeedBuff:
                ApplySpeedUp(target, data.value);
                break;
            case RelicStateType.AttackBuff:
                ApplyPowerUp(target, data.value);
                break;
            case RelicStateType.AllStatBuff:
                ApplyMaxHPUp(target, data.value);
                ApplySpeedUp(target, data.value);
                ApplyPowerUp(target, data.value);
                break;
        }
    }

    //최대 HP를 건드려야하는데 구상 후 Heal이 아닌 다른것으로 변환 할 수도 있음.
    private static void ApplyMaxHPUp(BattleUnit unit, float percent)
    {
        float addHp = unit.MaxHP * percent;
        unit.Heal(addHp);
    }
    //버프 형식으로 넣었으나 이후 변화가 있을수도 있음.
    private static void ApplySpeedUp(BattleUnit unit, float percent)
    {
        float addSpeed = unit.Speed * percent;
        unit.ApplyBuff(SkillType.SpeedBuff, addSpeed);
    }
    private static void ApplyPowerUp(BattleUnit unit, float percent)
    {
        float addPower = unit.AttackPower * percent;
        unit.ApplyBuff(SkillType.AttackBuff, addPower);
    }
}
