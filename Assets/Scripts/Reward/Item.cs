using UnityEngine;
using UnityEngine.Rendering;

public class Item : MonoBehaviour
{
    private ItemData itemData;

    public void Initialize(ItemData data)
    {
        itemData = data;
        // 추가 초기화 작업 수행
    }

    public void Use(BattleUnit unit = null, Skill skill = null)
    {
        if(itemData == null)
        {
            Debug.LogWarning("아이템 데이터를 확인할 수 없습니다.");
            return;
        }
        if(!CanUse(unit))
        {
            Debug.LogWarning($"아이템을 사용할 수 없습니다. {itemData.itemName} 이 유닛에게 {unit.UnitName}.");
            return;
        }

        
        //일단 일시 중지

    }

    private bool CanUse(BattleUnit unit)
    {
        switch(itemData.useItemType)
        {
            case UseItemType.HPUp:
            case UseItemType.PPUp:
                return !unit.IsDead;
            default:
                return false;
        }

    }

    private void ApplyEffect(BattleUnit unit)
    {
        
    }
}
