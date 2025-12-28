using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class RewardUI : MonoBehaviour
{
    [Header("slots")]
    [SerializeField] private RewardItemSlotUI[] paidSlots;
    [SerializeField] private RewardItemSlotUI[] freeSlots;

    [SerializeField] private RewardFlowController flowController;
    [SerializeField] private RewardManager rewardManager;

    [SerializeField] private Text currentGold;

    private void OnEnable()
    {
        ResetSlots();
    }

    private IEnumerator Start()
    {
        yield return null;

        Open(false);
    }

    public void Open(bool hasDeadTeam)
    {
        gameObject.SetActive(true);
        Debug.Log("보상 UI 열렸음 !");
        var freeItems = rewardManager.CreateFreeItems();
        var paidItems = rewardManager.CreatePaidItems(hasDeadTeam);

        currentGold.text = EconomyManager.Instance.RunGold.ToString();

        SetUpFree(freeItems);
        SetUpPaid(paidItems);
    }
    private void ResetSlots()
    {
        foreach (var slot in paidSlots)
        { slot.Hide(); }
        foreach (var slot in freeSlots)
        { slot.Hide(); }
    }

    //유료 아이템 슬롯에 아이템 세팅
    private void SetUpPaid(List<RunTimeItem> items)
    {
        Debug.Log("유료 아이템 세팅 중");
        for (int i = 0; i < paidSlots.Length && i < items.Count; i++)
        {
            var item = items[i];

            paidSlots[i].SetItem(item, item.itemData.itemCost, item.itemData.itemName,
                Resources.Load<Sprite>(item.itemData.itemImage),
                flowController.OnPaidItemSelected);
        }
    }
    // 무료 아이템 슬롯에 아이템 세팅
    private void SetUpFree(List<object> items)
    {
        Debug.Log("무료 아이템 세팅 중");
        for (int i = 0; i < freeSlots.Length && i < items.Count; i++)
        {
            if (items[i] is RunTimeItem item)
            {
                freeSlots[i].SetItem(item, 0, item.itemData.itemName,
                    Resources.Load<Sprite>(item.itemData.itemImage),
                    flowController.OnFreeItemSelected);
            }
            else if (items[i] is RunTimeRelic relic)
            {
                freeSlots[i].SetItem(relic, 0, relic.itemData.itemEquipName,
                    Resources.Load<Sprite>(relic.itemData.itemEquipImage),
                    flowController.OnFreeItemSelected);
            }
        }
    }

    public void OnClickNextStage()
    {
        StageManager.Instance.OnRewardProcessCompleted();
        Destroy(gameObject);
    }
}
