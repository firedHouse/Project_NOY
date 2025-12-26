using System;
using UnityEngine;
using UnityEngine.UI;

public class RewardItemSlotUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemCostText;

    private object itemData;
    // 아이템 슬롯에 아이템 표시
    public void SetItem(object data, int cost, string name, Sprite icon, Action<object> onClink)
    {   
        itemData = data;

        itemNameText.text = name;
        itemCostText.text = cost > 0 ? cost.ToString() : "0";
        itemIcon.sprite = icon;

        button.interactable = true;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClink?.Invoke(itemData));
        Debug.Log("아이템 불러왔음 !");
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        button.interactable = false;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
