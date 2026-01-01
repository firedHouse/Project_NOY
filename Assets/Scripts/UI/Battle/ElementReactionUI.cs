using System.Resources;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public partial class CharacterBattleInfoView
{
    Sprite imageLink = null;
    bool isFirstMark = false;
    bool isSecondMark = false;
    bool isElementReaction;

    ElementType attackMark = ElementType.None;

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
        if (waterMark == null)
        {
            Debug.LogError($"[CharacterBattleInfoView] 속성 아이콘 없음 / Spum_Icon2, Spum_Icon1, Spum_Icon4를 Image 폴더 안에 넣어주세요.)");
        }
    }

    public void ImageColor(Image target, float n)
    {
        Color color = target.color;
        color.a = n;
        target.color = color;
    }


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
                Debug.Log($"[CharacterBattleInfoView] UI : {unit.UnitName} 에게 {imageLink.name} 표식");
                return;
            }

            ImageLik(attackMark);
            //두번째 속성
            //첫번째 속성이랑 같은지 비교
            if (firstMark.sprite.name == imageLink.name)
            {
                //같으면 리턴
                return;
            }

            //다르면 마크 추가
            attackMark = element;
            UpdateSecondMark();
            isSecondMark = true;
            Debug.Log($"[CharacterBattleInfoView] UI : {unit.UnitName} 에게 {imageLink.name} 표식");
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
            if (firstMark.sprite.name == imageLink.name)
            {
                //같으면 리턴
                return;
            }

            //다르면 마크 추가
            UpdateSecondMark();
            isSecondMark = true;
            Debug.Log($"[CharacterBattleInfoView] UI : {unit.UnitName} 에게 {imageLink.name} 표식");
        }
    }


    public void InitMark(BattleUnit unit)
    {
        firstMark.sprite = null;
        secondMark.sprite = null;
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
        if (imageLink == null)
        {
            return;
        }
        firstMark.sprite = imageLink;
        ImageColor(firstMark, 1f);
    }

    public void UpdateSecondMark()
    {
        if (imageLink == null)
        {
            return;
        }
        secondMark.sprite = imageLink;
        ImageColor(secondMark, 1f);
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
                Debug.Log($"[CharacterBattleInfoView] 무속성");
                break;
        }
    }

    public void SImageLik(ElementReaction element)
    {

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
