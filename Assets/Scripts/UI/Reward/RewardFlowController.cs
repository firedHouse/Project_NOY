using UnityEngine;

public class RewardFlowController : MonoBehaviour
{
    [SerializeField] private TargetSelectUI targetSelectUI;
    [SerializeField] private RewardManager rewardManager;
    //골드 부족 UI 필요함

    private object currentItem;
    private bool isLocked = false;
    //읽기 전용 프로퍼티로 제작
    public bool IsLocked => isLocked;

    //유료 아이템을 선택한 경우 골드 차감 후 타겟 선택 오픈 -> 골드 차감 타이밍 조금 미뤄야함.
    public void OnPaidItemSelected(object data)
    {
        if (isLocked)
        { return; }

        var item = (RunTimeItem)data;
        if(!EconomyManager.Instance.CanSpendGold(item.itemData.itemCost))
        {
            Debug.Log("골드 부족");
            return;
        }

        isLocked = true;
        currentItem = item;
        targetSelectUI.Open(item, ApplyItem, CancelSelection);
    }
    //무료 아이템 선택 시 바로 사용할 수 있게 타겟 선택 오픈
    public void OnFreeItemSelected(object data)
    {
        if (isLocked)
        { return; }

        isLocked = true;
        currentItem = data;
        targetSelectUI.Open(data, ApplyItem, CancelSelection);
    }
    //아이템 적용
    private void ApplyItem(BattleUnit target, Skill skill)
    {
        bool sucess = false;

        if (currentItem is RunTimeItem item)
        {
            sucess = UsableItemExcution.Use(item, target, skill);

            if (sucess && item.itemData.itemCost > 0)
            {
                EconomyManager.Instance.SpendGold(item.itemData.itemCost);
            }
        }
        else if (currentItem is RunTimeRelic relic)
        {
            target.GetComponent<RelicComponent>()?.Equip(relic);
            rewardManager.MarkRelicUsed(relic);
            sucess = true;
        }

        if (!sucess)
        {
            targetSelectUI.ReOpen();
        }

        isLocked = false;
    }
    private void CancelSelection()
    {
        Debug.Log("아이템 선택 취소");
        ResetFlow();
    }

    private void ResetFlow()
    {
        currentItem = null;
        isLocked = false;
    }

}
