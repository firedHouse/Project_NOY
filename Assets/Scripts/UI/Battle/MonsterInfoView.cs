using UnityEngine;
using UnityEngine.UI;

public class MonsterInfoView : MonoBehaviour
{
    [Header("MonsterInfoPresenter")]
    [SerializeField] private MonsterInfoPresenter presenter;

    [Header("몬스터 이름")]
    [SerializeField] private Text monsterNameText;
    [Header("몬스터 속성")] //현재 출력안됨
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
    [SerializeField] Text firstMark;
    //[SerializeField] Image firstMark;
    [Header("표식2")]
    [SerializeField] Text secondMark;
    //[SerializeField] Image secondMark;


    public void UpdateMonsterName(string text)
    {
        monsterNameText.text = text;
    }

    //표식
    public void UpdateElement(string text)
    {
        elementText.text = text;
    }

    //고유속성
    // public void UpdateElementClass(ElementUI monsterUI)
    // {
    //     elementText.text = monsterUI.ToString();
    // }



    //position : 전열중열후열
    public void UpdateMonsterClass(MonsterClass monsterClass)
    {
        classText.text = monsterClass.ToString();
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

    string imageLink = "";
    bool isFirstMark = false;
    bool isSecondMark = false;
    bool isElementReaction;

    ElementType attackMark = ElementType.None;

    public void UpdateMark(ElementType element, BattleUnit unit)
    {
        //빈 마크가 없을때까지 추가
        if (isElementReaction == false)
        {
            //부여된 속성
            //첫번째는 그냥 등록
            if(isFirstMark == false)
            {
                attackMark = element;
                ImageLik(attackMark);
                UpdateFirstMark();
                Debug.Log($"[MonsterInfoView] UI : {unit.UnitName} 에게 {imageLink} 표식");
                isFirstMark = true;
                return;
            }

            UpdateFirstMark();

            //두번째 속성
            //첫번째 속성이랑 같은지 비교
            if (attackMark == element)
            {
                //같으면 리턴
                return;
            }

            //다르면 마크 추가
            ImageLik(attackMark);
            UpdateSecondMark();
            isSecondMark = true;
            Debug.Log($"[MonsterInfoView] UI : {unit.UnitName} 에게 {imageLink} 표식");

        }
    }

    ElementReaction skillAttack;
    public void SUpdateMark(ElementReaction skill, BattleUnit unit)
    {
        //두번째에서만
        if (isElementReaction == false)
        {
           skillAttack = skill;
           SImageLik(skillAttack);

            //두번째 속성
            //첫번째 속성이랑 같은지 비교
            if (isFirstMark == true && firstMark.text == imageLink)
            {
                //같으면 리턴
                return;
            }

            //다르면 마크 추가
            UpdateSecondMark();
            isSecondMark = true;
            Debug.Log($"[MonsterInfoView] UI : {unit.UnitName} 에게 {imageLink} 표식");
        }
    }

    public void InitMark(BattleUnit unit)
    {
        firstMark.text = "";
        secondMark.text = "";
        UpdateFirstMark();
        UpdateSecondMark();
        isFirstMark = false;
        isSecondMark = false;
        Debug.Log($"[MonsterInfoView] {unit.UnitName}표식 초기화");
    }

    public void IsMarkReaction(bool isReaction)
    {
        isElementReaction = isReaction;
        Debug.Log($"[MonsterInfoView] 원소 반응 여부 : {isElementReaction}");
    }

    public void UpdateFirstMark()
    {
        firstMark.text = imageLink;
    }

    public void UpdateSecondMark()
    {
        secondMark.text = imageLink;
    }

    public void ImageLik(ElementType element)
    {
        switch (element)
        {
            case ElementType.Fire:
                imageLink = "불";
                break;
            case ElementType.Water:
                imageLink = "물";
                break;
            case ElementType.Electric:
                imageLink = "전기";
                break;
            case ElementType.None:
                imageLink = "";
                Debug.Log($"[MonsterInfoView] 무속성");
                break;
        }
    }

    public void SImageLik(ElementReaction element)
    {
         
        switch (element)
        {
            case ElementReaction.None:
                imageLink = "";

                break;
                //증발
            case ElementReaction.Vaporize:
                {
                    if (firstMark.text == "물")
                    {
                        imageLink = "불";
                    }
                    else if(firstMark.text == "불")
                    {
                        imageLink = "물";
                    }

                }
                break;

                //감전
            case ElementReaction.ElectroShock:
                {
                    if (firstMark.text == "물")
                    {
                        imageLink = "전기";
                    }
                    else if (firstMark.text == "전기")
                    {
                        imageLink = "물";
                    }

                }
                break;

                //과부하
            case ElementReaction.Overload:
                {
                    if (firstMark.text == "불")
                    {
                        imageLink = "전기";
                    }
                    else if (firstMark.text == "전기")
                    {
                        imageLink = "불";
                    }

                }
                break;
        }
    }
}
