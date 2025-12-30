using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//스킬 버튼에 붙일 스크립트
public class SkillSlot : MonoBehaviour
{
    public Button button;
    public Image icon;

    private Skill mySkill;

    public Skill MySkill
    {
        get => mySkill;
        set => mySkill = value;
    }

    //초기화
    //스킬 정보랑, 클릭시 실행될 함수 받아옴
    public void Setup(Skill skill, UnityAction<Skill> onClickCallback)
    {
        mySkill = skill;
        if (mySkill != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClickCallback(mySkill));
        }
    }

    public void ChangeButtonAvailable(bool isAvailable)
    {
        button.interactable = isAvailable;
    }
}
