using System;
using UnityEngine;
using UnityEngine.UI;

// 혜주님 캐릭터 상세 UI와 합칠 예정
// 해금에 따라 변경될 것들 구현
public partial class GrowthView : MonoBehaviour
{
    [Header("잠금 아이콘")]
    [SerializeField] private GameObject lockedIcon;
    
    [Header("해금 버튼")]
    [SerializeField] private GameObject unlockButton;

    [Header("해금에 필요한 실링 임시")]
    [SerializeField] private int shillingForUnlock;
    [SerializeField] private Text unlockShillingText;

    [SerializeField] private GameObject MiddlePanel;
    [SerializeField] private GameObject RightPanel;
    [SerializeField] private AlertPopUpView popup;

    public AlertPopUpView Popup  => popup; 

    public GameObject UnlockButton => unlockButton;

    private void Awake()
    {
        // 널참조 오류나면 주석처리하고 인스펙터에서 연결하기
        unlockButton = GameObject.Find("CharacterUnlockButton");
        lockedIcon = GameObject.Find("CharacterLockIcon");
        MiddlePanel = GameObject.Find("MiddlePanel");
        RightPanel = GameObject.Find("RightPanel");
        //popup = GameObject.Find("NotifyPanel").GetComponent<AlertPopUpView>() ;
    }

    // 캐릭터 데이터 표시하는 메서드에 추가
    // unlock 이 false일 경우 해금 버튼을 표시
    // unlock 이 false일 경우 자물쇠 아이콘을 표시
    public void ChangeUnlockUIActivation(bool isUnlocked)
    {
        // 값이 true면 비활성화
        unlockButton.SetActive(!isUnlocked);
        lockedIcon.SetActive(!isUnlocked);
        // 캐릭터별 해금에 필요한 실링값을 받아와 변환
        unlockShillingText.text = shillingForUnlock.ToString();
        // 우측 패널 활성화 변경
        SetDetailView(true);
    }

    private void Start()
    {
        if (GameObject.Find("CharacterListPanel") is not null)
        {
            presenter = GameObject.Find("CharacterListPanel").GetComponent<CharacterListPresenter>();
        }
    }

    public void SetDetailView(bool isActive)
    {
        // Debug.Log($"[GrowthView] 상세 패널 활성화 여부 : {isActive}");
        // 우측 패널 활성화 변경
        MiddlePanel.SetActive(isActive);
        RightPanel.SetActive(isActive);
    }

    // 캐릭터별 해금에 필요한 실링값 받아오기 > 모델에서 구현(나중에 이동)
    //public void GetUnlockShilling() { }
}
