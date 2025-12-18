using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetMaxHP(float hpAmount)
    {
        slider.maxValue = hpAmount;
        slider.value = hpAmount;
        Debug.Log($"[HPBarController] 최대 Hp 설정 완료({slider.maxValue})");
    }

    public void SetHP(float hpAmount)
    {
        slider.value = hpAmount;
    }
}
