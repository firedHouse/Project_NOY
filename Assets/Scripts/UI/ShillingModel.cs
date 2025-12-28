using System;
using UnityEngine;

/// <summary>
///  12/27 : 실링 매니저 추가로 인해 이후로 사용하지 않음 해당 클래스 사용하는 부분 변경 필요
/// </summary>
public class ShillingModel : MonoBehaviour
{
    private int currentShilling;

    public int CurrentShilling => currentShilling;

    public event Action ShillingChanged;

    public void Start()
    {
        // 초기값, 추후 변경
        currentShilling = ShillingManager.Instance.OutGameShilling;
    }
    
    public void Decrease(int amount)
    {
        ShillingManager.Instance.SpendShilling(amount);
        ShillingChanged?.Invoke();
    }

    public void Restore()
    {
        // 저장되어 있는 실링 데이터 복구
        //shilling = 
        ShillingChanged?.Invoke();

    }
}
