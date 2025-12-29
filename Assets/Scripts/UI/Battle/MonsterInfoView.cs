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

    [Header("표식1")]
    [SerializeField] Text currentMark;
    //[SerializeField] Image currentMark;
    [Header("표식2")]
    [SerializeField] Text attackMark;
    //[SerializeField] Image attackMark;


    public void UpdateMonsterName(string text)
    {
        monsterNameText.text = text;
    }

    //표식
    //public void UpdateElement(string text)
    //{
    //    elementText.text = text;
    //}

    //고유속성
    //public void UpdateElementClass(string text)
    //{
    //    elementText.text = text;
    //}



    //position : 전열중열후열
    public void UpdatePosition(UnitPosition position)
    {
        classText.text = position.ToString();
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

    string imageLink = null;
    bool isfirstMark = true;

    public void UpdateMark(int element)
    {
        if (isfirstMark == true)
        {
            UpdateCurrentMark(element);
        }

        else if (isfirstMark == false)
        {
            UpdateAttackMark(element);
        }
    }

    public void InitMark()
    {
        currentMark.text = "";
        attackMark.text = "";
        Debug.Log("[MonsterInfoView] 표식 초기화");
    }

    public void UpdateCurrentMark(int element)
    {
        ImageLik(element);
        if (currentMark.text == imageLink && imageLink == "")
        {
            return;
        }
        currentMark.text = imageLink;
            isfirstMark = false;
            Debug.Log("[MonsterInfoView] 표식1 부여");
        
    }

    public void UpdateAttackMark(int element)
    {
        ImageLik(element);
        if (attackMark.text == imageLink && imageLink == "")
        {
            return;
        }
        attackMark.text = imageLink;
            isfirstMark = true;
            Debug.Log("[MonsterInfoView] 표식2 부여");
        
    }

    public void ImageLik(int element)
    {
        switch (element)
        {
            case 6:
                imageLink = "불";
                break;
            case 7:
                imageLink = "물";
                break;
            case 8:
                imageLink = "전기";
                break;
            case 9:
                imageLink = "";
                break;
        }
        Debug.Log($"[MonsterInfoView] 표식 {imageLink} 부여");
    }

}
