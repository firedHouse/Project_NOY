using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public partial class CharacterBattleInfoView
{
    string imageLink = "";
    int num;

    ElementType attackMark = ElementType.None;

    public void UpdateMark(ElementType element)
    {
        //원소 반응이 일어 났으면 다음 턴에 초기화
        if (num == 2)
        {
            InitMark();
            num = 0;
        }

        //빈 마크가 없을때까지 추가
        if (firstMark.text == "" && secondMark.text == "")
        {
            //부여된 속성
            //첫번째는 그냥 등록
            if (firstMark.text == "")
            {
                attackMark = element;
                ImageLik(attackMark);
                num = 1;
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
            num = 2;
        }
    }

    ElementReaction skillAttack;
    public void SUpdateMark(ElementReaction skill)
    {
        //원소 반응이 일어 났으면 다음 턴에 초기화
        if (num == 2)
        {
            InitMark();
            num = 0;
        }

        //두번째에서만
        if (secondMark.text == "")
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
            num = 2;
        }
    }


    public void InitMark()
    {
        firstMark.text = "";
        secondMark.text = "";
        Debug.Log("[MonsterInfoView] 표식 초기화");
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
