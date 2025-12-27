using UnityEngine;
using UnityEngine.UI;

public class RosterButton : MonoBehaviour
{
    [SerializeField] GameObject rosterPenel;
    [SerializeField] GameObject SkipCautionPanel;
    [SerializeField] Text teamButtonText;

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
        if(StageManager.Instance.CurrentRound == 5)
        {
            teamButtonText.text = "팀 구성 변경";
        }
        else if(StageManager.Instance.CurrentRound != 5)
        {
            teamButtonText.text = "다음 스테이지";
        }
    }

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
    }

    #endregion
}
