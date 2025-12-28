using UnityEngine;
using UnityEngine.UI;

public class RosterButton : MonoBehaviour
{
    [Header("프레젠터")]
    [SerializeField] RosterPresenter presenter;

    [Header("팀 영입 판넬 / 스킵경고 판넬")]
    [SerializeField] GameObject rosterPenel;
    [SerializeField] GameObject SkipCautionPanel;

    [Header("캐릭터 체인지 판넬")]
    [SerializeField] public GameObject ChangePenel;

    [Header("다음스테이지 텍스트")]
    [SerializeField] Text teamButtonText;

    private int click;


    #region 아이템창 버튼
    //팀 구성변경 버튼
    //조건 1. 보스 스테이지 클리어 시에 표시 텍스트 변경
    //---조건 2. 아이템 선택 O---
    //조건 3. 팀구성 버튼 클릭

    //스킵경고패널
    //조건 1. 보스 스테이지 클리어 시에만 활성화
    //---조건 2. 아이템 선택 X---
    //조건 3. 팀구성 버튼 클릭

    //텍스트 변경, 보스스테이지에서만
    private void ButtonTextSwitch()
    {
        if (StageManager.Instance.CurrentRound == 5)
        {
            teamButtonText.text = "팀 구성 변경";
        }
        else if (StageManager.Instance.CurrentRound != 5)
        {
            teamButtonText.text = "다음 스테이지";
        }
    }

    //팀 구성 변경 버튼
    public void OnTeamCompositionButton(bool itemChoice)
    {
        Debug.Log("[RosterButton] 클릭됨");
        if (rosterPenel == null || SkipCautionPanel == null)
        {
            Debug.Log("[RosterButton] 패널 비었음");
            return;
        }

        //if (StageManager.Instance.CurrentRound == 5)
        //{
        //아이템 선택 시에 실행
        if (itemChoice == true)
        {
            rosterPenel.SetActive(true);
            Debug.Log("[RosterButton] 팀구성화면");
        }

        //아이템 미선태 시, 스킵 경고창
        else if (itemChoice == false)
        {
            SkipCautionPanel.SetActive(true);
            Debug.Log("[RosterButton] 경고창");
        }
        //}
    }
    #endregion

    public void OnJoinSkip()
    {
        Debug.Log("[RosterButton] 영입스킵");
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
    }

    //영입 캐릭터 선택 칸 클릭
    public void OnClickCharacter(int i)
    {
        // 0 : 1번캐 클릭, 1: 2번캐 클릭, 2 : 3번캐 클릭
        click = i;
        Debug.Log($"[RosterButton] {i}캐릭터 클릭");
        presenter.ClickCharacter(i);
    }

    public void OnJoinButton()
    {
        if (presenter.joinCharacter != null)
        {
            ChangePenel.SetActive(true);
            presenter.PrintPlayerTeam();
        }
        else
        {
            Debug.Log($"[RosterButton] 영입캐릭터를 선택하지 않음");
        }
    }

    public void OnExportButton()
    {
        presenter.SwitchCharacter();

    }

    public void OnConfirmButton()
    {
        //팀원이 변경되었으면 실링 차감
        presenter.GoldCal();
        //다음 스테이지
        OnBattleStartButton();
    }
    #endregion

}
