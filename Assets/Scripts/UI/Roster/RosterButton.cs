using UnityEngine;
using UnityEngine.UI;

public class RosterButton : MonoBehaviour
{
    [Header("프레젠터")]
    [SerializeField] RosterPresenter presenter;

    [Header("캐릭터 리스트 모델")]
    [SerializeField] PlayerTeamListModel playerTeamListModel;

    [Header("버튼 스크립트")]
    [SerializeField] NextStageButton nextStageButton;

    [Header("리워드 플로우 컨트롤러")]
    [SerializeField] RewardFlowController rewardFlowController;

    [Header("팀 영입 판넬 / 스킵경고 판넬")]
    [SerializeField] GameObject rosterPenel;
    [SerializeField] GameObject SkipCautionPanel;

    [Header("캐릭터 체인지 판넬")]
    [SerializeField] public GameObject ChangePenel;

    [Header("골드 부족 판넬")]
    [SerializeField] public GameObject purchaseFailedPanel;

    [Header("캐릭터 영입 판넬")]
    [SerializeField] public GameObject joinPenel;

    [Header("캐릭터 재배치 판넬")]
    [SerializeField] public GameObject RePosPenel;


    private int click;
    private bool isitemChoice;
    private bool isBossStage;

    private void Start()
    {
        isitemChoice = rewardFlowController.IsLocked;
    }


    #region 아이템창 버튼
    //팀 구성변경 버튼
    //조건 1. 보스 스테이지 클리어 시에 표시 텍스트 변경
    //---조건 2. 아이템 선택 O---
    //조건 3. 팀구성 버튼 클릭

    //스킵경고패널
    //조건 1. 보스 스테이지 클리어 시에만 활성화
    //---조건 2. 아이템 선택 X---
    //조건 3. 팀구성 버튼 클릭


    //팀 구성 변경 버튼
    public void OnTeamCompositionButton()
    {
        isBossStage = nextStageButton.IsBossStage;

        //보스 스테이지 아니면 바로 스테이지 넘어가게
        if (isBossStage == false)
        {
            OnConfirmButton();
            return;
        }

        Debug.Log("[RosterButton] 클릭됨");
        if (rosterPenel == null || SkipCautionPanel == null)
        {
            Debug.Log("[RosterButton] 패널 비었음");
            return;
        }

        //if (StageManager.Instance.CurrentRound == 5)
        //{
        //아이템 선택 시에 실행
        if (isitemChoice == true)
        {
            rosterPenel.SetActive(true);
            Debug.Log("[RosterButton] 팀구성화면");
        }

        //아이템 미선태 시, 스킵 경고창
        else if (isitemChoice == false)
        {
            SkipCautionPanel.SetActive(true);
            Debug.Log("[RosterButton] 경고창");
        }
        //}
    }

    public void OnItemSkipButton()
    {
        SkipCautionPanel.SetActive(false);
        isitemChoice = true;       
        OnTeamCompositionButton();
    }
    #endregion

    public void OnJoinSkip()
    {
        Debug.Log("[RosterButton] 영입스킵");
        playerTeamListModel.IsJoinSkip = true;
        OnRePosButton();
    }

    #region 스킵경고패널 버튼
    //뒤로가기 버튼 : 패널 비활성화
    public void OnBackButton()
    {
        SkipCautionPanel.SetActive(false);
    }
    //건너뛰기 버튼 : 팀구성패널 버튼과 동일한 역할
    #endregion


    #region 팀구성패널 버튼
    //건너뛰기 버튼 : 전투 진입
    public void OnBattleStartButton()
    {
        Debug.Log("[RosterButton] 배틀진입버튼 눌림");
        presenter.ConfirmButton();
        playerTeamListModel.IsJoinSkip = false;
    }

    //영입 캐릭터 선택 칸 클릭
    public void OnClickCharacter(int i)
    {

        Debug.Log($"[RosterButton] 현재 보유 골드 : {EconomyManager.Instance.RunGold} 골드");
        if (EconomyManager.Instance.RunGold < 500)
        {
            Debug.Log("[RosterButton] 골드가 부족합니다.");
            purchaseFailedPanel.SetActive(true);
            return;
        }

        // 0 : 전열 클릭, 1: 중열 클릭, 2 : 후열 클릭
        click = i;
        Debug.Log($"[RosterButton] {i}캐릭터 클릭");
        presenter.ClickCharacter(i);
        //현재 팀 구성 화면
        ChangePenel.SetActive(true);
        joinPenel.SetActive(false);
        //플레이어팀을 띄워줘야 함
        presenter.PrintPlayerTeam();
    }

    public void OnRePosButton()
    {
        playerTeamListModel.IsJoinSkip = true;
        RePosPenel.SetActive(true);
        joinPenel.SetActive(false);
    }

    public void OnConfirmButton()
    {
        //팀원이 변경되었으면 실링 차감
        presenter.GoldCal();
        Debug.Log($"[RosterButton] 남은 골드 : {EconomyManager.Instance.RunGold} 골드");

        //다음 스테이지
        OnBattleStartButton();
    }
    #endregion

}
