using UnityEngine;
using UnityEngine.UI;

public class MonsterInfoView : MonoBehaviour
{
    [SerializeField] private Text monsterNameText;
    [SerializeField] private Text elementText;
    [SerializeField] private Text posistionText;
    [SerializeField] private Text monsterSpeedText;
    [SerializeField] private Text monsterPowerText;
    [SerializeField] private Slider HpSlider;

    public void UpdateMonsterName(string text)
    {
        monsterNameText.text = text;
    }

    public void UpdateElement(int type)
    {
        elementText.text = type.ToString();
    }

    //public void UpdatePosition(string text)
    //{
    //    posistionText.text = text;
    //}
    public void SetMaxHP(float hpAmount)
    {
        HpSlider.maxValue = hpAmount;
        HpSlider.value = hpAmount;
        Debug.Log($"[MonsterInfoView] 최대 Hp 설정 완료({HpSlider.maxValue})");
    }

    public void UpdateHPBar(float currentHP)
    {
        HpSlider.value = currentHP;
    }

    public void UpdateSpeed(int monsterSpeed)
    {
        monsterSpeedText.text = "스피드 : " + monsterSpeed.ToString();
    }

    public void UpdatePower(float monsterPower)
    {
        monsterPowerText.text = "공격력 : " + monsterPower.ToString();
    }
}
