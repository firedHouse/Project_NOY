using UnityEngine;
using UnityEngine.UI;

public class GrowthView : MonoBehaviour
{
    //캐릭터의 학년 정보 받아와야 함.
    [Header("임시변수")]
    [SerializeField] private int grade;

    [Header("실링부족텍스트")]
    [SerializeField] private Text notEnoughShilingPanel;

    [Header("닫기버튼")]
    [SerializeField] private Text OKPanel;

    [Header("성장버튼/텍스트")]
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Text upgradeCostText;

    [Header("GrowthPresenter")]
    [SerializeField] private GrowthPresenter presenter;

    [Header("별 출력 배열/리소스")]
    [SerializeField] private Image[] starImage = new Image[3];
    [SerializeField] private Sprite yellowStar;
    [SerializeField] private Sprite grayStar;

    [Header("고유속성 출력 배열/리소스(물/불/번개/무속)")]
    [SerializeField] private Image elementImage;
    [SerializeField] private Sprite[] elementImageResources = new Sprite[4];

    [Header("캐릭터 이름/코드네임/캐릭터 설명")]
    [SerializeField] private Text charName;
    [SerializeField] private Text codeName;
    [SerializeField] private Text infoText;



    //재사용 안된다면 삭제
    #region 실링 부족 경고창
    private void NotEnoughShiling ()
    {
        notEnoughShilingPanel.text = "실링이 부족합니다.";
    }

    private void OKText ()
    {
        OKPanel.text = "인정";
    }
    #endregion
   
    //성장버튼 클릭 액션
    public void OnClickUpgradeButton()
    {
        //실링이 충분함
        if(presenter.IsCanUpgrade() == true)
        {
            //업그레이드 정보 전달
            presenter.SuccessUpgrade();
        }
        //실링이 부족함
        else if(presenter.IsCanUpgrade() == false)
        {
            //실링부족 > 패널 띄움
            OnNotEnoughShilling(true);
        }
    }

    //확인 버튼 클릭 액션
    public void OnOKButton()
    {
        //패널 닫기
        OnNotEnoughShilling(false);
    }

    //고유속성 출력 : ElementUI elementUI
    //0불 /1물 / 2번개 / 3무속성
    public void CharacterElement(int element)
    {
        elementImage.sprite = elementImageResources[element];
    }

    //이름, 코드네임
    public void CharacterInfo(string name, string _codeName)
    {
        charName.text = name;
        codeName.text = _codeName;
    }

    //세부설정
    public void CharacterInfo(string info)
    {
        infoText.text = info;
    }

    //비용 업데이트
    public void UpgradeCost(int grade)
    {
        upgradeCostText.text = $"{grade * 1000}";
    }


    

    //버튼 활성/비활성
    public void ButtonActive(bool canClick)
    {
        upgradeButton.interactable = canClick;
    }

    //실링 부족 경고창
    public void OnNotEnoughShilling(bool isBlocked)
    {
        upgradeButton.interactable = isBlocked;
    }

    //별 이미지 갱신
    public void GradeSet(int grade)
    {
        if(yellowStar == null || grayStar ==null)
        {
            Debug.Log("[GrowthView] 별 이미지 정보 없음");
            return;
        }
        if (grade > 3)
        {
            Debug.Log("[GrowthView] 등급 최대치 넘어감");
            return;
        }

        //등급만큼 노란별
        for (int i = 0; i < grade; i++)
        {
            starImage[i].sprite = yellowStar;
        }
        //
        for (int i = grade; i < 3; i++)
        {
            starImage[i].sprite = grayStar;
        }
    }
}
