using System;
using UnityEngine;

public class ShillingModel : MonoBehaviour
{

    [SerializeField] private int currentShilling;
    public event Action ShillingChanged;

    public int CurrentShilling { get => currentShilling; set => currentShilling = value; }

    private int minShilling = 0;
    private int maxShilling = 999999;

    public void Awake()
    {
        // 초기값, 추후 변경
        currentShilling = 5000;
    }

    public void Increase(int amount)
    {
        currentShilling += amount;
        currentShilling = Mathf.Clamp(currentShilling, minShilling, maxShilling);

        ShillingChanged?.Invoke();
    }

    public void Decrease(int amount)
    {
        currentShilling -= amount;
        currentShilling = Mathf.Clamp(currentShilling, minShilling, maxShilling);

        ShillingChanged?.Invoke();
    }

    public void Restore()
    {
        // 저장되어 있는 실링 데이터 복구
        //shilling = 
        ShillingChanged?.Invoke();

    }
}
