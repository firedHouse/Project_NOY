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

    [SerializeField] private RewardFlowController flowController;
    [SerializeField] private RewardManager rewardManager;
    [SerializeField] private RewardSkipConfirmUI skipConfirmUI;

    [SerializeField] private GameObject nextStageButton;
    [SerializeField] private GameObject recruitButton;

    [SerializeField] private RecruitUI recruitUI;

    [SerializeField] private Text currentGold;

    private bool isSkipConfirmOpen = false;

    public void Open(bool hasDeadTeam, bool isBossRound)
    {
        if (gameObject.activeSelf)
        { return; }

        gameObject.SetActive(true);

        flowController.ResetFreeItemState();

        ResetSlots();
        HideDescription();

        Debug.Log("보상 UI 열렸음 !");
        var freeItems = rewardManager.CreateFreeItems();
        var paidItems = rewardManager.CreatePaidItems(hasDeadTeam);

        currentGold.text = EconomyManager.Instance.RunGold.ToString();

        SetUpFree(freeItems);
        SetUpPaid(paidItems);

        UpdateButton(isBossRound);
    }

    public void Update()
    {
        currentGold.text = EconomyManager.Instance.RunGold.ToString();
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
        Debug.Log("[rewardUI] 다음 스테이지 버튼 클릭 됨");

        if (isSkipConfirmOpen)
        { return; }

        Debug.Log("체크 완료");

        if (!flowController.FreeItemUsed)
        {
            isSkipConfirmOpen = true;
            skipConfirmUI.Open(this); 
            return; 
        }

        Debug.Log("체크용 디버그");

        ProceedNextStage();
    }

    public void OnSkipConfirmClosed()
    {
        isSkipConfirmOpen = false;
    }

    public void ProceedNextStage()
    {
        gameObject.SetActive(false);
        flowController.ResetFreeItemState();
        StageManager.Instance.OnRewardProcessCompleted();
    }

    private void UpdateButton(bool isBossRound)
    {
        if (nextStageButton != null)
        {
            nextStageButton.gameObject.SetActive(!isBossRound);
        }
        if (recruitButton != null)
        {
            recruitButton.gameObject.SetActive(isBossRound);
        }
    }

    public void OnClickRecruit()
    {
        Debug.Log("영입 버튼 클릭");
        if (recruitUI == null)
        {
            Debug.LogError("recruitUI 연결되지 않음 !");
        }
        recruitUI.Open();
    }
}
