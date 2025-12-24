using UnityEngine;
using UnityEngine.UI;

public class GrowthSkillView : MonoBehaviour
{
    [Header("GrowthPresenter")]
    [SerializeField] private CharacterListPresenter presenter;

    [Header("고유속성 출력 배열/리소스(물/불/번개/무속)")]
    [SerializeField] private Text elementImage;
    //[SerializeField] private Sprite[] elementImageResources = new Sprite[4];

    [Header("캐릭터 이름/코드네임")]
    [SerializeField] private Text charName;
    [SerializeField] private Text codeName;

    [Header("별 출력 배열/리소스")]
    [SerializeField] private Text[] starImage = new Text[3];
    //[SerializeField] private Sprite yellowStar;
    //[SerializeField] private Sprite grayStar;

    [Header("일러스트출력")]
    [SerializeField] private Image illust;

    [Header("스킬출력")]
    [SerializeField] private Text[] skill = new Text[3];
    //[SerializeField] private Sprite[] skillImage = new Sprite[3];

    [Header("스킬이름/설명")]
    [SerializeField] private Text skillNameText;
    [SerializeField] private Text skillInfoText;

    //이름, 코드네임
    public void CharacterName(CharacterListModel model)
    {
        if (charName == null || codeName == null)
        {
            Debug.Log("[GrowthSkillView] 이름, 코드네임 오보젝트가 없습니다.");
            return;
        }
        charName.text = model.CharacterName;
        codeName.text = model.CharacterCodeName;
        Debug.Log($"[GrowthSkillView] 이름 : {model.CharacterName}");
        Debug.Log($"[GrowthSkillView] 코드네임 : {model.CharacterCodeName}");
    }

    //고유속성 출력 : ElementUI elementUI
    //0불 /1물 / 2번개 / 3무속성
    public void CharacterElement(CharacterListModel model)
    {
        if (elementImage == null)
        {
            Debug.Log("[GrowthSkillView] 속성(테스트버전-텍스트)오브젝트가 없습니다.");
            return;
        }
        elementImage.text = $"{model.Element}";
        Debug.Log($"[GrowthSkillView] 속성 : {model.Element}");
    }

    //별 이미지 갱신
    public void GradeSet(CharacterListModel model)
    {
        if (starImage[0] == null)
        {
            Debug.Log("[GrowthSkillView] 별 이미지(테스트버전-텍스트)오보젝트가 없습니다.");
            return;
        }
        //if(yellowStar == null || grayStar ==null)
        //{
        //    Debug.Log("[GrowthView] 별 이미지 정보 없음");
        //    return;
        //}
        //if (grade > 3)
        //{
        //    Debug.Log("[GrowthView] 등급 최대치 넘어감");
        //    return;
        //}

        //등급만큼 노란별
       
        for (int i = 0; i < model.Level; i++)
        {
            starImage[i].text = $"★";
            //starImage[i].sprite = yellowStar;
        }

        for (int i = model.Level; i < 3; i++)
        {
            starImage[i].text = $"☆";
            //starImage[i].sprite = grayStar;
        }
    }

    //학년별 일러스트
    public void CharacterIllust(CharacterListModel model)
    {
        Debug.Log("[GrowthView] : 일러스트 변경");
        //illust.sprite = illustImage[grade];
    }

    //스킬
    public void CharacterSkill(CharacterListModel model)
    {
        //for (int i = 0; i < 3; i++)
        //{
        //    //skill[i].sprite = skillImage[i];
        //}

        //for (int i = 0; i < 3; i++)
        //{
        //    skill[i].sprite = Resources.Load<Sprite>();
        //}
    }

    //스킬 마우스오버
    public void OnFristSkillMouseOver()
    {
        if (skillNameText == null || skillInfoText == null)
        {
            Debug.Log("[GrowthSkillView] 스킬(테스트버전-텍스트)오보젝트가 없습니다.");
            return;
        }
        //skillNameText.text = $"{model.Skills[0].Data.skillName}";
        //skillInfoText.text = $"{model.Skills[0].Data.skillTooltip }";
        skillNameText.text = $"테스트 : 첫번째 스킬";
        skillInfoText.text = $"이것은 첫번째 스킬이다";
    }
    public void OnScecondSkillMouseExit()
    {
        if (skillNameText == null || skillInfoText == null)
        {
            Debug.Log("[GrowthSkillView] 스킬(테스트버전-텍스트)오보젝트가 없습니다.");
            return;
        }
        //skillNameText.text = $"{model.Skills[1].Data.skillName}";
        //skillInfoText.text = $"{model.Skills[1].Data.skillTooltip }";
        skillNameText.text = $"테스트 : 두번째 스킬";
        skillInfoText.text = $"이것은 두번째 스킬이다";

    }
    public void OnThirdSkillMouseExit()
    {
        if (skillNameText == null || skillInfoText == null)
        {
            Debug.Log("[GrowthSkillView] 스킬(테스트버전-텍스트)오보젝트가 없습니다.");
            return;
        }
        //skillNameText.text = $"{model.Skills[2].Data.skillName}";
        //skillInfoText.text = $"{model.Skills[2].Data.skillTooltip }";
        skillNameText.text = $"테스트 : 세번째 스킬";
        skillInfoText.text = $"이것은 세번째 스킬이다";

    }


}
