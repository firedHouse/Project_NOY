using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class RewardUI : MonoBehaviour
{
    [Header("slots")]
    [SerializeField] private RewardItemSlotUI[] paidSlots;
    [SerializeField] private RewardItemSlotUI[] freeSlots;

    [Header("Description Panel")]
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Text descriptionText;

    [Header("Stage Text")]
    [SerializeField] private Text stageInfoText;

    [SerializeField] private RewardFlowController flowController;
    [SerializeField] private RewardManager rewardManager;

    [SerializeField] private Text currentGold;

    private void OnEnable()
    {
        ResetSlots();
        HideDescription();
    }


    public void Open(bool hasDeadTeam)
    {

        gameObject.SetActive(true);
        Debug.Log("보상 UI 열렸음 !");
        var freeItems = rewardManager.CreateFreeItems();
        var paidItems = rewardManager.CreatePaidItems(hasDeadTeam);

        currentGold.text = EconomyManager.Instance.RunGold.ToString();

        UpdateStageInfo();

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

            Sprite icon = ResourceManager.Instance.LoadSprite(item.itemData.itemImage);

            paidSlots[i].SetItem(item, item.itemData.itemCost, item.itemData.itemName,
                icon, flowController.OnPaidItemSelected);
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
                Sprite icon = ResourceManager.Instance.LoadSprite(item.itemData.itemImage);

                freeSlots[i].SetItem(item, 0, item.itemData.itemName,
                    icon, flowController.OnFreeItemSelected);
            }
            else if (items[i] is RunTimeRelic relic)
            {
                Sprite icon = ResourceManager.Instance.LoadSprite(relic.itemData.itemEquipImage);

                freeSlots[i].SetItem(relic, 0, relic.itemData.itemEquipName,
                    icon, flowController.OnFreeItemSelected);
            }
        }
    }
    private void UpdateStageInfo()
    {
        if (StageManager.Instance == null)
        {
            stageInfoText.text = "";
            return;
        }

        if (StageManager.Instance.CurrentRound == 5)
        {
            stageInfoText.text += "(Boss)";
            return;
        }

        int stage = StageManager.Instance.CurrentStage;
        int round = StageManager.Instance.CurrentRound;

        stageInfoText.text = $"스테이지 {stage}-{round}";
    }

    public void ShowDescription(object item)
    {
        if (item is RunTimeItem runTimeItem)
        {
            var data = runTimeItem.itemData;
            descriptionText.text = data.itemTextUI;
        }
        else if (item is RunTimeRelic relic)
        {
            var data = relic.itemData;
            descriptionText.text = data.itemEquipTextUI;
        }
    }
    public void HideDescription()
    {
        descriptionText.text = "";
    }

    public void OnClickNextStage()
    {
        StageManager.Instance.OnRewardProcessCompleted();
        Destroy(gameObject);
    }
}
