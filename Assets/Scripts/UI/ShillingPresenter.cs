using System;
using UnityEngine;

public class ShillingPresenter : MonoBehaviour
{
    [Tooltip("실링 뷰")]
    [SerializeField] private ShillingView shillingView;
    
    private void Start()
    {
        UpdateUI();

        // ShillingChanged += UpdateUI;
    }

    // 실링 변경 이벤트
    private void OnShillingChanged()
    {
        UpdateUI();
    }

    // UI 업데이트 
    public void UpdateUI()
    {
        Debug.Log($"[ShillingPresenter] 현재 실링 {ShillingManager.Instance.OutGameShilling}");
        shillingView.UpdateShilling(ShillingManager.Instance.OutGameShilling);
    }

    // 실링 감소
    // public void DecreaseShilling(int amount)
    // {
    //     // 테스트용 코드
    //     // shillingModel.Decrease(changeAmount);
    //     shillingModel.Decrease(amount);
    // }
}
