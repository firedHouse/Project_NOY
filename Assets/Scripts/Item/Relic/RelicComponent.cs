using System.Collections.Generic;
using UnityEngine;

public class RelicComponent : MonoBehaviour
{
    private BattleUnit owner;
    private RunTimeRelic currentRelic;

    public RunTimeRelic CurrentRelic => currentRelic;

    private float appliedHP;
    private float appliedAtk;
    private float appliedSpeed;

    private void Awake()
    {
        owner = GetComponent<BattleUnit>();

        if (owner == null)
        {
            Debug.LogError("유물은 플레이어 필요");
        }
    }

    //유물 장착 메서드
    public void Equip(RunTimeRelic newRelic)
    {
        if (newRelic == null || owner == null)
        { return; }

        RemoveCurrentRelic();

        currentRelic = newRelic;
        ApplyRelic(newRelic);

        Debug.Log($"{owner.UnitName} → 유물 장착: {newRelic.itemData.itemEquipName}");

    }

    private void ApplyRelic(RunTimeRelic newRelic)
    {
        float percent = newRelic.itemData.value;

        switch (newRelic.stateType)
        {
            case RelicStateType.HPBuff:
                appliedHP = owner.BaseMaxHP * percent;
                Debug.Log($"{owner.UnitName}에게 유물 장착, 체력 {appliedHP}만큼 증가");
                owner.IncreaseMaxHP(appliedHP);
                break;
            case RelicStateType.AttackBuff:
                appliedAtk = owner.BaseAttack * percent;
                Debug.Log($"{owner.UnitName}에게 유물 장착, 공격력 {appliedAtk}만큼 증가");
                owner.ApplyBuff(SkillType.AttackBuff, appliedAtk);
                break;
            case RelicStateType.SpeedBuff:
                appliedSpeed = owner.BaseSpeed * percent;
                Debug.Log($"{owner.UnitName}에게 유물 장착, 속도 {appliedSpeed}만큼 증가");
                owner.ApplyBuff(SkillType.SpeedBuff, appliedSpeed);
                break;
            case RelicStateType.AllStatBuff:
                appliedHP = owner.BaseMaxHP * percent;
                appliedAtk = owner.BaseAttack * percent;
                appliedSpeed = owner.BaseSpeed * percent;

                owner.IncreaseMaxHP(appliedHP);
                owner.ApplyBuff(SkillType.AttackBuff, appliedAtk);
                owner.ApplyBuff(SkillType.SpeedBuff, appliedSpeed);
                break;

        }
    }

    private void RemoveCurrentRelic()
    {
        if (currentRelic == null)
            return;

        if (appliedHP != 0)
            owner.IncreaseMaxHP(-appliedHP);

        if (appliedAtk != 0)
            owner.ApplyBuff(SkillType.AttackBuff, -appliedAtk);

        if (appliedSpeed != 0)
            owner.ApplyBuff(SkillType.SpeedBuff, -appliedSpeed);

        appliedHP = appliedAtk = appliedSpeed = 0;
        currentRelic = null;
    }
}
