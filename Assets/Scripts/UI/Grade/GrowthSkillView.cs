using UnityEngine;
using UnityEngine.UI;

public class GrowthSkillView : MonoBehaviour
{
    [Header("GrowthPresenter")]
    [SerializeField] private GrowthPresenter presenter;

    [Header("스킬출력")]
    [SerializeField] private Image[] skill = new Image[3];
    //[SerializeField] private Sprite[] skillImage = new Sprite[3];

    [Header("스킬이름/설명")]
    [SerializeField] private Text skillNameText;
    [SerializeField] private Text skillInfoText;

    //학년별 스킬 갱신
    public void CharacterSkill(CharacterData model)
    {
        for (int i = 0; i < 3; i++)
        {
            //skill[i].sprite = skillImage[i];
        }

        //for (int i = 0; i < 3; i++)
        //{
        //    skill[i].sprite = Resources.Load<Sprite>(model.Skills[i].Data.skillIcon);
        //}
    }

    //스킬 마우스오버
    public void OnFristSkillMouseOver()
    {
        //skillNameText.text = $"{model.Skills[0].Data.skillName}";
        //skillInfoText.text = $"{model.Skills[0].Data.skillTooltip }";
        skillNameText.text = $"테스트 : 첫번째 스킬";
        skillInfoText.text = $"이것은 첫번째 스킬이다";
    }
    public void OnScecondSkillMouseExit()
    {
        //skillNameText.text = $"{model.Skills[1].Data.skillName}";
        //skillInfoText.text = $"{model.Skills[1].Data.skillTooltip }";
        skillNameText.text = $"테스트 : 두번째 스킬";
        skillInfoText.text = $"이것은 두번째 스킬이다";

    }
    public void OnThirdSkillMouseExit()
    {
        //skillNameText.text = $"{model.Skills[2].Data.skillName}";
        //skillInfoText.text = $"{model.Skills[2].Data.skillTooltip }";
        skillNameText.text = $"테스트 : 세번째 스킬";
        skillInfoText.text = $"이것은 세번째 스킬이다";

    }
}
