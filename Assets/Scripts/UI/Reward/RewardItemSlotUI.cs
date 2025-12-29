using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardItemSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Components")]
    [SerializeField] private GameObject root;
    [SerializeField] private Button button;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemCostText;

    private RewardUI rewardUI;

    private object itemData;
    // 아이템 슬롯에 아이템 표시

    public void Awake()
    {
        rewardUI = GetComponentInParent<RewardUI>();
    }
    public void SetItem(object data, int cost, string name, Sprite icon, Action<object> onClink)
    {
        itemData = data;

        root.SetActive(true);

        itemNameText.text = name;
        itemCostText.text = cost > 0 ? cost.ToString() : "0";
        if (icon != null) { itemIcon.sprite = icon; }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClink?.Invoke(itemData));
        Debug.Log("아이템 불러왔음 !");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rewardUI?.ShowDescription(itemData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        rewardUI?.HideDescription();
    }


    public void Hide()
    {
        itemData = null;
        root.SetActive(false);
        button.onClick.RemoveAllListeners();
    }
}
