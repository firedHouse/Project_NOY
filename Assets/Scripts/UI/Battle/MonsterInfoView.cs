using UnityEngine;
using UnityEngine.UI;

public class MonsterInfoView : MonoBehaviour
{
    [Header("몬스터 이름")]
    [SerializeField] private Text monsterNameText;
    [Header("몬스터 속성")]
    [SerializeField] private Text elementText;
    [Header("몬스터 클래스")]
    [SerializeField] private Text classText;
    [Header("몬스터 스피드")]
    [SerializeField] private Text monsterSpeedText;
    [Header("몬스터 공격력")]
    [SerializeField] private Text monsterPowerText;
    [Header("몬스터 HP 바")]
    [SerializeField] private Slider HPSlider;

    public void UpdateMonsterName(string text)
    {
        monsterNameText.text = text;
    }

    public void UpdateElement(string text)
    {
        elementText.text = text;
    }

    //position : 전열중열후열
    public void UpdatePosition(UnitPosition position)
    {
    }

    //탱딜힐
    //public void UpdateCalss(CharacterPosition role)
    //{
    //    classText.text = role.ToString();
    //    Debug.Log($"{role.ToString()}");
    //}

    public void SetMaxHP(float maxHP)
    {
        HPSlider.maxValue = maxHP;
        HPSlider.value = maxHP;
        Debug.Log($"[MonsterInfoView] 최대 Hp 설정 완료({HPSlider.maxValue})");
    }

    public void UpdateHPBar(float currentHP)
    {
        HPSlider.value = currentHP;
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
