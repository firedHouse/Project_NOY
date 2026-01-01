using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public partial class CharacterBattleInfoView
{
    string imageLink = "";
    bool isFirstMark = false;
    bool isSecondMark = false;
    bool isElementReaction;

    ElementType attackMark = ElementType.None;

    public void UpdateMark(ElementType element, BattleUnit unit)
    {
        //빈 마크가 없을때까지 추가
        //리엑션 없을때만 실행
        if (isElementReaction == false)
        {
            //부여된 속성
            //첫번째는 그냥 등록
            if (isFirstMark == false)
            {
                attackMark = element;
                ImageLik(attackMark);
                isFirstMark = true;
                UpdateFirstMark();
                Debug.Log($"[CharacterBattleInfoView] UI : {unit.UnitName} 에게 {imageLink} 표식");
                return;
            }

            UpdateFirstMark();

            //두번째 속성
            //첫번째 속성이랑 같은지 비교
            if (isFirstMark == true && attackMark == element)
            {
                //같으면 리턴
                return;
            }

            //다르면 마크 추가
            ImageLik(attackMark);
            UpdateSecondMark();
            isSecondMark = true;
            Debug.Log($"[CharacterBattleInfoView] UI : {unit.UnitName} 에게 {imageLink} 표식");
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
            if (firstMark.text == imageLink)
            {
                //같으면 리턴
                return;
            }

            //다르면 마크 추가
            UpdateSecondMark();
            isSecondMark = true;
            Debug.Log($"[CharacterBattleInfoView] UI : {unit.UnitName} 에게 {imageLink} 표식");
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
        Debug.Log($"[CharacterBattleInfoView] {unit.UnitName}표식 초기화");
    }
    public void IsMarkReaction(bool isReaction)
    {
        isElementReaction = isReaction;
        Debug.Log($"[CharacterBattleInfoView] 원소 반응 여부 : {isElementReaction}");
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
                Debug.Log($"[CharacterBattleInfoView] 무속성");
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
                    else if (firstMark.text == "불")
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
