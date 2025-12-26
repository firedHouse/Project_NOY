using UnityEngine;

public class RewardFlowController : MonoBehaviour
{
    [SerializeField] private TargetSelectUI targetSelectUI;
    [SerializeField] private RewardManager rewardManager;
    //골드 부족 UI 필요함

    private object currentItem;
    private bool isLocked = false;

    //유료 아이템을 선택한 경우 골드 차감 후 타겟 선택 오픈
    public void OnPaidItemSelected(ItemData item)
    {
        if (isLocked)
        { return; }

        isLocked = true;

        if (!EconomyManager.Instance.SpendGold(item.itemCost))
        {
            isLocked = false;
            Debug.Log("골드 부족"); // UI로 띄워야 함
            return; // 나중에 UI 띄우게 되면 return은 그 UI에서 처리하는 것
        }

        currentItem = item;
        targetSelectUI.Open(item, ApplyItem);
    }
    //무료 아이템 선택 시 바로 사용할 수 있게 타겟 선택 오픈
    public void OnFreeItemSelected(object item)
    {
        if (isLocked)
        { return; }

        isLocked = true;

        currentItem = item;
        targetSelectUI.Open(item, ApplyItem);
    }
    //아이템 적용
    private void ApplyItem(BattleUnit target, Skill skill)
    {
        bool success = false;

        if (currentItem is ItemData item)
        {
            UsableItem usableItem = new UsableItem();
            usableItem.Initialize(item);
            usableItem.Use(target, skill);
        }
        else if(currentItem is ItemEquipData relic)
        {
            var relicComponent = target.GetComponent<RelicComponent>();
            relicComponent.Equip(relic);
        }
    }

}
