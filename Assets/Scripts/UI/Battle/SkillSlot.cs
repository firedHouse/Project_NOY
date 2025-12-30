using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//스킬 버튼에 붙일 스크립트
public class SkillSlot : MonoBehaviour
{
    public Button button;
    public Image icon;
    public Sprite currenSkillSprite;


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
        SKillResource(skill);
    }

    public void ChangeButtonAvailable(bool isAvailable)
    {
        button.interactable = isAvailable;
    }

    public void SKillResource(Skill skill)
    {
        if (skill == null || skill.Data.skillIcon == null)
        {
            Debug.Log("아이콘 없음 !");
            return;
        }

        Sprite sprite = ResourceManager.Instance.LoadSprite(skill.Data.skillIcon);

        if (sprite == null)
        {
            Debug.LogWarning($"{gameObject.name} 스킨 Sprite 로드 실패 : {skill.Data.skillIcon}");
        }

        currenSkillSprite = sprite;

        GetComponent<Image>().sprite = currenSkillSprite;
    }
}
