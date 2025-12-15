using TMPro;
using UnityEngine;

public class ShillingPresenter : MonoBehaviour
{
    [Tooltip("실링 뷰")]
    [SerializeField] private ShillingView shillingView;

    [Tooltip("실링 모델")]
    [SerializeField] private ShillingModel shillingModel;

    // test용 코드
    private int changeAmount = 5000;

    private void OnEnable()
    {
        shillingModel.ShillingChanged += OnShillingChanged;
        UpdateUI();
    }

    // 실링 변경 이벤트
    private void OnShillingChanged()
    {
        UpdateUI();
    }

    // UI 업데이트 
    private void UpdateUI()
    {
        shillingView.UpdateShilling(shillingModel.CurrentShilling);
    }

    // 실링 추가
    public void IncreaseShilling(int amount)
    {
        // 테스트용 코드
        shillingModel.Increase(changeAmount);
        //shillingModel.Increase(amount);
    }

    // 실링 감소
    public void DecreaseShilling(int amount)
    {
        // 테스트용 코드
        shillingModel.Decrease(changeAmount);
        //shillingModel.Decrease(amount);
    }
}
