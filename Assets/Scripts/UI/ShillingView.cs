using TMPro;
using UnityEngine;

public class ShillingView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI shillingText;

    public void UpdateShilling(int amount)
    {
        shillingText.text = amount.ToString();
    }
}
