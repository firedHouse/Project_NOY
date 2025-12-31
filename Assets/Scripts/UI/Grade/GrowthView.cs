using System;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public partial class GrowthView : MonoBehaviour
{
    [Header("GrowthPresenter")]
    [SerializeField] private CharacterListPresenter presenter;

    [Header("고유속성 출력 배열/리소스(물/불/번개/무속)")]
    [SerializeField] private Image elementImage;
    //[SerializeField] private Sprite[] elementImageResources = new Sprite[4];

    [Header("캐릭터 이름/코드네임")]
    [SerializeField] private Text charName;
    [SerializeField] private Text codeName;

    [Header("별 출력 배열/리소스")]
    [SerializeField] private Image[] starImage = new Image[3];
    [SerializeField] private Sprite yellowStar;
    [SerializeField] private Sprite grayStar;

    [Header("일러스트출력")]
    [SerializeField] private Image illust;

    [Header("실링부족텍스트")]
    [SerializeField] private GameObject notEnoughShilingPanel;
    [SerializeField] private Text notEnoughShilingText;
    [SerializeField] private Text OKPanel;

    [Header("성장버튼/텍스트")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Text upgradeCostText;

    [Header("한마디/정보")]
    [SerializeField] private Text lineText;
    [SerializeField] private Text infoText;


    //재사용 안된다면 삭제
    #region 실링 부족 경고창
    private void NotEnoughShiling()
    {
        notEnoughShilingText.text = "실링이 부족합니다.";
    }

    private void OKText()
    {
        OKPanel.text = "인정";
    }
    #endregion

    public void OnClickUpgradeButton()
    {
        Debug.Log("[GrowthView] 성장 버튼 클릭됨");
        presenter.IsCanUpgrade();
    }

    //확인 버튼 클릭 액션
    public void OnOKButton(bool buttonActive)
    {
        //패널 닫기
        OnNotEnoughShilling(buttonActive);
    }

    //이름, 코드네임
    public void CharacterName(CharacterListModel model)
    {
        if (charName == null || codeName == null)
        {
            Debug.Log("[GrowthView] 이름, 코드네임 오보젝트가 없습니다.");
            return;
        }

        charName.text = model.CharacterName;
        codeName.text = model.CharacterCodeName;
        // Debug.Log($"[GrowthView] 이름 : {model.CharacterName}");
        // Debug.Log($"[GrowthView] 코드네임 : {model.CharacterCodeName}");
    }

    //고유속성 출력 : ElementUI elementUI
    //0불 /1물 / 2번개 / 3무속성
    public void CharacterElement(CharacterListModel model)
    {
        if (elementImage == null)
        {
            Debug.Log("[GrowthView] 속성(테스트버전-텍스트)오브젝트가 없습니다.");
            return;
        }

        //elementImage = $"{model.Element}";
        Debug.Log($"[GrowthView] 속성 : {model.Element}");
    }

    //별 이미지 갱신
    public void GradeSet(CharacterListModel model)
    {
        int saveData = UserDataManager.Instance.GetCharacterGrade(model.CharacterID);
        Debug.Log($"[GrowthSkillView] 레벨 : {saveData}");

        // 하나씩 밝히는 느낌

        switch(saveData)
        {
            case 1:
                starImage[1].color = new Color(255, 255, 255, 255);
                break;
            case 2:
                starImage[2].color = new Color(255, 255, 255, 255);
                break;
        }
    }

    public void InitStar()
    {
        string ImageName = "SPUM/Retro UI Set/1_UI_Images/Theme2/06_UI/Spum_Icon139";

        for (int i = 0; i < 2; i++)
        {
            starImage[i].sprite = Resources.Load<Sprite>(ImageName);
            starImage[i].color = new Color(26, 26, 26, 255);
        }
    }


    //세부설정
    public void CharacterInfo(CharacterListModel model)
    {
        if (lineText == null || infoText == null)
        {
            Debug.Log("[GrowthView] 한마디, 설명 오브젝트가 없습니다.");
            return;
        }
        lineText.text = model.CharacterDialogue;
        infoText.text = "";
        if (model.IsUnlocked == true)
        {
            infoText.text = model.CharacterInfo;
        }
        // Debug.Log($"[GrowthView] 한마디 : {model.CharacterDialogue}");
        // Debug.Log($"[GrowthView] 설명 : {model.CharacterInfo}");
    }

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

    //비용 업데이트
    public void UpgradeCost(CharacterListModel model)
    {
        if (model.Level < 2)
        {
            upgradeCostText.text = $"{model.NeedShilling}";
            Debug.Log($"[GrowthView] : 성장 비용({model.NeedShilling}) 변경");
        }
        else
        {
            upgradeCostText.text = "최고 학년 입니다.";
        }
    }

    //버튼 활성/비활성
    public void ButtonActive(bool canClick)
    {
        if (upgradeButton == null)
        {
            Debug.Log("[GrowthView] 성장버튼 오브젝트가 없습니다.");
            return;
        }

        // upgradeButton.interactable = canClick;
        upgradeButton.gameObject.SetActive(canClick);
        Debug.Log($"[GrowthView] 성장버튼 활성화 여부 {canClick}");
    }

    //실링 부족 경고창
    public void OnNotEnoughShilling(bool isTrue)
    {
        if (notEnoughShilingPanel == null)
        {
            Debug.Log("[GrowthView] 실링부족 오보젝트가 없습니다.");
            return;
        }
        notEnoughShilingPanel.SetActive(isTrue);
    }
}
