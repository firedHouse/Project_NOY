using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RewardUI : MonoBehaviour
{
    [Header("slots")]
    [SerializeField] private RewardItemSlotUI[] paidSlots;
    [SerializeField] private RewardItemSlotUI[] freeSlots;

    [SerializeField] private RewardFlowController flowController;
    [SerializeField] private RewardManager rewardManager;

    IEnumerator Start()
    {
        yield return null;
        Open(new List<ItemData>(), new List<object>());
    }

    public void Open(List<ItemData> PaidItem, List<object> FreeItem)
    {
        gameObject.SetActive(true);
        Debug.Log("보상 UI 열렸음 !");
        
        SetUpFree(FreeItem);
        SetUpPaid(PaidItem);
    }
    //유료 아이템 슬롯에 아이템 세팅
    private void SetUpPaid(List<ItemData> items)
    {
        Debug.Log("유료 아이템 세팅 중");
        for (int i = 0; i < paidSlots.Length; i++)
        {
            if(i >= items.Count)
            {
                paidSlots[i].Hide();
                continue;
            }

            ItemData data = items[i];

            paidSlots[i].SetItem(data, data.itemCost, data.itemName,
                Resources.Load<Sprite>(data.itemImage), 
                obj => flowController.OnPaidItemSelected((ItemData)obj));
        }
    }
    // 무료 아이템 슬롯에 아이템 세팅
    private void SetUpFree(List<object> items)
    {
        rewardManager.CreateFreeItems();
        Debug.Log("무료 아이템 세팅 중");
        for (int i = 0; i < freeSlots.Length; i++)
        {
            if (i >= items.Count)
            {
                freeSlots[i].Hide();
                continue;
            }

            object data = items[i];

            if(data is ItemData itemData)
            {
                freeSlots[i].SetItem(itemData, 0, itemData.itemName,
                    Resources.Load<Sprite>(itemData.itemImage),
                    obj => flowController.OnFreeItemSelected((ItemData)obj));
            }
            else if(data is ItemEquipData relic)
            {
                freeSlots[i].SetItem(relic, 0, relic.itemEquipName,
                    Resources.Load<Sprite>(relic.itemEquipImage),
                    obj => flowController.OnFreeItemSelected((ItemEquipData)obj));
            }
        }
    }
}
