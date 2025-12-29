using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static class UsableItemExcution
{
    public static bool Use(RunTimeItem item, BattleUnit unit, Skill skill)
    {
        switch (item.useType)
        {
            case UsableItemType.HPPotion:
                return UseHPPotion(item, unit);
            case UsableItemType.PPPotion:
                return UsePPPotion(item, skill);
            case UsableItemType.Revive:
                return UseRevive(item, unit);

        }

        return false;
    }
    private static bool UseHPPotion(RunTimeItem item, BattleUnit unit)
    {
        if (unit == null || unit.IsDead)
        { return false; }

        unit.Heal(item.itemData.value);
        return true;
    }
    private static bool UsePPPotion(RunTimeItem item, Skill skill)
    {
        if (skill == null || !skill.IsValid())
        { return false; }

        skill.RestorePP(item.itemData.value);
        return true;
    }
    private static bool UseRevive(RunTimeItem item, BattleUnit unit)
    {
        if (unit == null || !unit.IsDead)
        { return false; }

        unit.gameObject.SetActive(true);

        unit.ForceRevive(item.itemData.value);

        if (unit is Character character)
        {
            ReviveFlowController.instance?.StartRevivalFlow(character);
        }

        return true;
    }
}
