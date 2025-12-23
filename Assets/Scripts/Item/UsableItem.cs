using UnityEngine;

public class UsableItem : MonoBehaviour
{
    private ItemData itemData;

    public void Initialize(ItemData data)
    {
        itemData = data;
    }

    //아이템 사용 -> HP : BattleUnit 필요 / PP : Skill 필요
    //이것만 가져가서 사용하면 됨.
    public bool Use(BattleUnit unit, Skill skill)
    {
        if (itemData == null)
        {             
            Debug.LogError("아이템 데이터가 없습니다.");
            return false;
        }
        
        if(!TryApplyUsableItem(unit, skill))
        {
            Debug.LogWarning($"[{itemData.itemName}] 아이템을 사용할 수 없습니다.");
            return false;
        }

        Debug.Log($"{itemData.itemName} 사용 성공");
        return true;

    }

    //아이템 효과 적용
    private bool TryApplyUsableItem(BattleUnit unit, Skill skill)
    {
        switch(itemData.useItemType)
        {
            case UsableItemType.HPPotion:
                if(unit == null || unit.IsDead)
                {
                    Debug.LogError("HP 회복 아이템 사용 시 유닛이 필요합니다.");
                    return false;
                }
                unit.Heal(itemData.value);
                Debug.Log($"[{itemData.itemName}] 사용: {unit.UnitName}의 HP가 {itemData.value}만큼 회복되었습니다.");
                return true;
            case UsableItemType.PPPotion:
                if(skill == null || !skill.IsValid())
                {
                    Debug.LogError("PP 회복 아이템 사용 시 스킬이 필요합니다.");
                    return false;
                }
                skill.RestorePP(itemData.value);
                Debug.Log($"[{itemData.itemName}] 사용: 스킬의 PP가 {itemData.value}만큼 회복되었습니다.");
                return true;
            case UsableItemType.Revive:
                if(unit == null || !unit.IsDead)
                {
                    Debug.LogError("부활 아이템 사용 시 죽은 유닛이 필요합니다.");
                    return false;
                }
                // unit.Revive(itemData.value); -> 배틀유닛에 부활 메서드 추가 필요
                Debug.Log($"[{itemData.itemName}] 사용: {unit.UnitName}이(가) 부활하여 HP가 {itemData.value}만큼 회복되었습니다.");
                return true;
            default:
                Debug.LogError("알 수 없는 아이템 타입입니다.");
                return false;
        }
    }
}
