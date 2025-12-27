using UnityEngine;

public static class UsableItemExcution
{
    public static bool Use(RunTimeItem item, BattleUnit unit, Skill skill)
    {
        switch (item.useType)
        {
            case UsableItemType.HPPotion:
                if (unit == null || unit.IsDead) { return false; }
                unit.Heal(item.itemData.value);
                return true;
            case UsableItemType.PPPotion:
                if (skill == null || !skill.IsValid()) { return false; }
                skill.RestorePP(item.itemData.value);
                return true;
            case UsableItemType.Revive:
                if (unit == null || !unit.IsDead) { return false; }
                unit.gameObject.SetActive(true);
                unit.Heal(item.itemData.value);
                return true;

        }
        return false;
    }
}
