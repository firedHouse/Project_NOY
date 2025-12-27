using System;
using UnityEngine;
using UnityEngine.UI;

public class ShillingView : MonoBehaviour
{
    [SerializeField] private Text shillingText;
    

    public void UpdateShilling(int amount)
    {
        Debug.Log("[ShillingView] 실링 업데이트");
        shillingText.text = amount.ToString();
    }
}
