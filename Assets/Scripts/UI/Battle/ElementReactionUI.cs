using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public partial class CharacterBattleInfoView
{
    string imageLink;
    bool isfirstMark = true;

    public void UpdateMark(int element)
    {
        //이미 같은 속성이며 ㄴ새로 부여 안되게
        if (isfirstMark == true)
        {
            UpdateCurrentMark(element);
            Debug.Log("[ElementReactionUI] 표식1부여");
        }

        else if (isfirstMark == false)
        {
            UpdateAttackMark(element);
            Debug.Log("[ElementReactionUI] 표식2부여");
        }
    }
    public void InitMark()
    {
        currentMark.text = "";
        attackMark.text = "";
        Debug.Log("[ElementReactionUI]표식 초기화");
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
        if(attackMark.text == imageLink && imageLink == "")
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
                //무속성
                imageLink = "";
                Debug.Log($"[ElementReactionUI] 무속성");
                break;
        }
        Debug.Log($"[ElementReactionUI] 표식 {imageLink} 부여");
    }
}
