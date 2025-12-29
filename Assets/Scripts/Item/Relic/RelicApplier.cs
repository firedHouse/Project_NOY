using Unity.VisualScripting;
using UnityEngine;

public static class RelicApplier
{   //렐릭이 장착됐을 때, 캐릭터한테 능력치를 더해주기 위한 계산 스크립트
    public static void Apply(BattleUnit target, RunTimeRelic data)
    {
        if (target == null || data == null)
        {
            Debug.Log("장착할 유닛 혹은 유물의 데이터들 불러올 수 없습니다.");
            return;
        }

        switch (data.stateType)
        {
            case RelicStateType.HPBuff:
                ApplyMaxHPUp(target, data.itemData.value);
                break;
            case RelicStateType.SpeedBuff:
                ApplySpeedUp(target, data.itemData.value);
                break;
            case RelicStateType.AttackBuff:
                ApplyPowerUp(target, data.itemData.value);
                break;
            case RelicStateType.AllStatBuff:
                ApplyMaxHPUp(target, data.itemData.value);
                ApplySpeedUp(target, data.itemData.value);
                ApplyPowerUp(target, data.itemData.value);
                break;
        }
    }

    public static void Remove(BattleUnit target, RunTimeRelic data)
    {
        if (target == null || data == null)
        {
            return;
        }

        switch (data.stateType)
        {
            case RelicStateType.HPBuff:
                RemoveMaxHPUp(target, data.itemData.value);
                break;
            case RelicStateType.SpeedBuff:
                RemoveSpeedUp(target, data.itemData.value);
                break;
            case RelicStateType.AttackBuff:
                RemovePowerUp(target, data.itemData.value);
                break;
            case RelicStateType.AllStatBuff:
                RemoveMaxHPUp(target, data.itemData.value);
                RemoveSpeedUp(target, data.itemData.value);
                RemovePowerUp(target, data.itemData.value);
                break;

        }
    }

    private static void ApplyMaxHPUp(BattleUnit unit, float percent)
    {
        float addHp = unit.MaxHP * percent;
        unit.IncreaseMaxHP(addHp);
        Debug.Log($"{unit}에게 장착 체력이 {addHp}만큼 상승");
    }
    //버프 형식으로 넣었으나 이후 변화가 있을수도 있음.
    private static void ApplySpeedUp(BattleUnit unit, float percent)
    {
        float addSpeed = unit.Speed * percent;
        unit.ApplyBuff(SkillType.SpeedBuff, addSpeed);
        Debug.Log($"{unit}에게 장착 속도가 {addSpeed}만큼 상승");
    }
    private static void ApplyPowerUp(BattleUnit unit, float percent)
    {
        float addPower = unit.AttackPower * percent;
        unit.ApplyBuff(SkillType.AttackBuff, addPower);
        Debug.Log($"{unit}에게 장착 공격력이 {addPower}만큼 상승");
    }
    private static void RemoveMaxHPUp(BattleUnit unit, float percent)
    {
        float addHp = unit.MaxHP * percent;
        unit.IncreaseMaxHP(-addHp);
        Debug.Log($"{unit}에게 해제, 체력이 {addHp}만큼 상승");
    }
    //버프 형식으로 넣었으나 이후 변화가 있을수도 있음.
    private static void RemoveSpeedUp(BattleUnit unit, float percent)
    {
        float addSpeed = unit.Speed * percent;
        unit.ApplyBuff(SkillType.SpeedBuff, -addSpeed);
        Debug.Log($"{unit}에게 해제, 속도가 {addSpeed}만큼 상승");
    }
    private static void RemovePowerUp(BattleUnit unit, float percent)
    {
        float addPower = unit.AttackPower * percent;
        unit.ApplyBuff(SkillType.AttackBuff, -addPower);
        Debug.Log($"{unit}에게 해제, 공격력이 {addPower}만큼 하락");
    }
}
