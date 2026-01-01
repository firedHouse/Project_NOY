using System.Resources;
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
    [SerializeField] Image firstMark;
    //[SerializeField] Image firstMark;
    [Header("표식2")]
    [SerializeField] Image secondMark;
    //[SerializeField] Image secondMark;

    Sprite waterMark;
    Sprite fireMark;
    Sprite elecMark;

    private void Awake()
    {
        ImageColor(firstMark, 0f);
        ImageColor(secondMark, 0f);
        waterMark = Resources.Load<Sprite>("Image/Spum_Icon2");
        fireMark = Resources.Load<Sprite>("Image/Spum_Icon1");
        elecMark = Resources.Load<Sprite>("Image/Spum_Icon4");
        if(waterMark == null)
        {
            Debug.LogError($"[MonsterInfoView] 속성 아이콘 없음 / Spum_Icon2, Spum_Icon1, Spum_Icon4를 Image 폴더 안에 넣어주세요.)");
        }
    }

    public void ImageColor(Image target, float n)
    {
        Color color = target.color;
        color.a = n;
        target.color = color;
    }

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

    Sprite imageLink = null;
    bool isFirstMark = false;
    bool isSecondMark = false;
    bool isElementReaction;

    public void UpdateMark(ElementType element, BattleUnit unit)
    {
        //원소반응이 일어나면 true가 됨
        if (isElementReaction == false)
        {
            //부여된 속성
            //첫번째는 그냥 등록
            if (isFirstMark == false)
            {
                ImageLik(element);
                UpdateFirstMark();

                if (imageLink == null)
                { 
                    Debug.Log($"[MonsterInfoView] 대상 : {unit.UnitName} 표식 스프라이트 없음");
                    return;
                }
                Debug.Log($"[MonsterInfoView] UI : {unit.UnitName} 에게 {imageLink.name} 표식");
                return;
            }

            ImageLik(element);
            //두번째 속성
            //첫번째 속성이랑 같은지 비교
            if (firstMark.sprite.name == imageLink.name)
            {
                //같으면 리턴
                return;
            }

            UpdateSecondMark();
            if (imageLink == null)
            {
                Debug.Log($"[MonsterInfoView] 대상 : {unit.UnitName} 표식 스프라이트 없음");
                return;
            }
            Debug.Log($"[MonsterInfoView] UI : {unit.UnitName} 에게 {imageLink.name} 표식");

        }
    }

    public void SUpdateMark(ElementReaction skill, BattleUnit unit)
    {

        //두번째에서만
        if (isElementReaction == false)
        {
            SImageLik(skill);

            //두번째 속성
            //첫번째 속성이랑 같은지 비교
            //다르면 마크 추가
            

            if (firstMark.sprite == null || imageLink == null)
            {
                //같으면 리턴
                Debug.Log($"[MonsterInfoView] 대상 : {unit.UnitName} 첫번째 표식 없음");
                return;
            }
            if (firstMark.sprite.name == imageLink.name)
            {
                //같으면 리턴
                Debug.Log($"[MonsterInfoView] 대상 : {unit.UnitName} 첫번째 표식과 동일 표식");
                return;
            }

            UpdateSecondMark();
            Debug.Log($"[MonsterInfoView] UI : {unit.UnitName} 에게 {imageLink.name} 표식");
        }
    }

    public void InitMark(BattleUnit unit)
    {
        firstMark.sprite = null;
        secondMark.sprite = null;
        ImageColor(firstMark, 0f);
        ImageColor(secondMark, 0f);

        isFirstMark = false;
        isSecondMark = false;
        imageLink = null;
        Debug.Log($"[MonsterInfoView] {unit.UnitName}표식 초기화");
    }

    public void IsMarkReaction(bool isReaction)
    {
        isElementReaction = isReaction;
        Debug.Log($"[MonsterInfoView] 원소 반응 여부 : {isElementReaction}");
    }

    public void UpdateFirstMark()
    {
        if (imageLink == null)
        {
            return;
        }
        firstMark.sprite = imageLink;
        ImageColor(firstMark, 1f);
        isFirstMark = true;
    }

    public void UpdateSecondMark()
    {
        if (imageLink == null)
        {
            return;
        }
        secondMark.sprite = imageLink;
        ImageColor(secondMark, 1f);
        isSecondMark = true;
    }

    public void ImageLik(ElementType element)
    {
        switch (element)
        {
            case ElementType.Fire:
                imageLink = fireMark;
                break;
            case ElementType.Water:
                imageLink = waterMark;
                break;
            case ElementType.Electric:
                imageLink = elecMark;
                break;
            case ElementType.None:
                imageLink = null;
                Debug.Log($"[MonsterInfoView] 무속성");
                break;
        }
    }

    public void SImageLik(ElementReaction element)
    {
        imageLink = null;
        if (firstMark.sprite == null)
        {
            Debug.Log($"[MonsterInfoView] 첫번째 마크 비어있음");
            return;
        }

        switch (element)
        {
            case ElementReaction.None:
                imageLink = null;

                break;
            //증발
            case ElementReaction.Vaporize:
                {
                    if (firstMark.sprite.name == waterMark.name)
                    {
                        imageLink = fireMark;
                    }
                    else if (firstMark.sprite.name == fireMark.name)
                    {
                        imageLink = waterMark;
                    }

                }
                break;

            //감전
            case ElementReaction.ElectroShock:
                {
                    if (firstMark.sprite.name == waterMark.name)
                    {
                        imageLink = elecMark;
                    }
                    else if (firstMark.sprite.name == elecMark.name)
                    {
                        imageLink = waterMark;
                    }

                }
                break;

            //과부하
            case ElementReaction.Overload:
                {
                    if (firstMark.sprite.name == fireMark.name)
                    {
                        imageLink = elecMark;
                    }
                    else if (firstMark.sprite.name == elecMark.name)
                    {
                        imageLink = fireMark;
                    }

                }
                break;
        }
    }
}
