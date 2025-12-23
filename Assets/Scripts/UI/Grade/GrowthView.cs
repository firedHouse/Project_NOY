using UnityEngine;
using UnityEngine.UI;

public class GrowthView : MonoBehaviour
{
    [Header("GrowthPresenter")]
    [SerializeField] private GrowthPresenter presenter;

    [Header("일러스트출력")]
    [SerializeField] private Image illust;
    //[SerializeField] private Sprite[] illustImage = new Sprite[3];

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
    private void NotEnoughShiling ()
    {
        notEnoughShilingText.text = "실링이 부족합니다.";
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


    //세부설정
    public void CharacterInfo(string info, string line)
    {
        lineText.text = line;
        infoText.text = info;
    }

    //학년별 일러스트
    public void CharacterIllust(int grade)
    {
        Debug.Log("[GrowthView] : 일러스트 변경");
        //illust.sprite = illustImage[grade];
    }

    //비용 업데이트
    public void UpgradeCost(int grade)
    {
        if(upgradeButton.interactable == true)
        {
            upgradeCostText.text = $"{grade * 1000}";
        }
        else
        {
            upgradeCostText.text = "최고 학년 입니다.";
        }
    }

    //버튼 활성/비활성
    public void ButtonActive(bool canClick)
    {
        upgradeButton.interactable = canClick;
    }

    //실링 부족 경고창
    public void OnNotEnoughShilling(bool isTrue)
    {
        notEnoughShilingPanel.SetActive(isTrue);
    }

}
