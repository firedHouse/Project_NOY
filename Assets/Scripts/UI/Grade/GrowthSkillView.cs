using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public partial class GrowthSkillView : MonoBehaviour
{
    // [Header("GrowthPresenter")]
    // [SerializeField] private CharacterListPresenter presenter;

    [Header("고유속성 출력 배열/리소스(물/불/번개/무속)")]
    [SerializeField] private Text elementImage;
    [SerializeField] private Sprite[] elementImageResources = new Sprite[4];

    [Header("캐릭터 이름/코드네임")]
    [SerializeField] private Text charName;
    [SerializeField] private Text codeName;
    [SerializeField] private Text position;

    [Header("별 출력 배열/리소스")]
    [SerializeField] private Image[] starImage = new Image[3];

    [Header("일러스트출력")]
    [SerializeField] private Image illust;

    [Header("스킬출력")]
    [SerializeField] private Text[] skill = new Text[3];
    //[SerializeField] private Sprite[] skillImage = new Sprite[3];

    [Header("스킬이름/설명")]
    [SerializeField] private Text skillNameText;
    [SerializeField] private Text skillInfoText;

    [SerializeField] private List<SkillSlot> slots;

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
        CharacterPosition pos= (CharacterPosition)model.Position;
        position.text = pos.ToString();
        Debug.Log($"[GrowthSkillView] 이름 : {model.CharacterName}");
        Debug.Log($"[GrowthSkillView] 코드네임 : {model.CharacterCodeName}");
        Debug.Log($"[GrowthSkillView] 포지션 : {position.text}");
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
        int saveData = UserDataManager.Instance.GetCharacterGrade(model.CharacterID);
        Debug.Log($"[GrowthSkillView] 레벨 : {saveData}");

        // 하나씩 밝히는 느낌
        switch (saveData)
        {
            case 0:
                starImage[2].color = new Color32(26, 26, 26, 255);
                starImage[1].color = new Color32(26, 26, 26, 255);
                break;
            case 1:
                starImage[1].color = new Color(1f, 1f, 1f, 1f);
                break;
            case 2:
                starImage[1].color = new Color(1f, 1f, 1f, 1f);
                starImage[2].color = new Color(1f, 1f, 1f, 1f);
                break;
        }
    }

    //public void InitStar()
    //{

    //    for (int i = 0; i < 2; i++)
    //    {
    //        starImage[i].color = new Color(26, 26, 26, 255);
    //    }
    //}

    //학년별 일러스트
    int skinID;
    Sprite IllustSprite;
    string skinData;

    public void CharacterIllust(CharacterListModel model)
    {
        int saveData = UserDataManager.Instance.GetCharacterGrade(model.CharacterID);
        Debug.Log($"[CharacterListPresenter] {saveData}");

        //스킨 데이터 
        skinID = int.Parse(model.CharacterSkin);

        if (model.CharacterSkin == null || model.CharacterSkin == null)
        {
            Debug.Log("아이콘 없음 !");
            return;
        }

        //레벨1일때
        if (saveData == 0)
        {
            skinData = TableManager.Instance.SkinTable.Get(model.CharacterSkin).imageFull;
        }

        else if (saveData == 2)
        {
            skinID = skinID + 1;
            skinData = TableManager.Instance.SkinTable.Get(skinID.ToString()).imageFull;
        }

        Sprite sprite = ResourceManager.Instance.LoadSprite(skinData);

        if (sprite == null)
        {
            Debug.LogWarning($"{gameObject.name} 스킨 Sprite 로드 실패 : {sprite}");
        }

        illust.sprite = sprite;
    }



    //스킬
    public void CharacterSkill(CharacterListModel model)
    {
        if (skill[0] == null)
        {
            Debug.Log("[GrowthSkillView] 스킬 출력 오브젝트가 없습니다.");
            return;
        }
        // for (int i = 0; i < 3; i++)
        // { 
        //     skill[i].text = model.Skills[i].Data.skillName;
        //     //skill[i].sprite = skillImage[i];
        // }

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
