using UnityEngine;
using UnityEngine.UI;

public class FarmationButton : MonoBehaviour
{
    [Header("프레젠터")]
    [SerializeField] FormationPresenter presenter;

    [Header("캐릭터 리스트 모델")]
    [SerializeField] PlayerTeamListModel playerTeamListModel;

    [Header("팀 자리 변경 패널")]
    [SerializeField] GameObject changePositionPanel;

    public bool isJoinSkip = false;

    private int click;

    #region 팀구성패널 버튼

    //영입 캐릭터 선택 칸 클릭
    public void OnClickCharacter(int i)
    {
        // 0 : 전열 클릭, 1: 중열 클릭, 2 : 후열 클릭
        click = i;
        Debug.Log($"[RosterButton] {i}캐릭터 클릭");
        presenter.ClickCharacter(i);
        //클릭하면 UI갱신
    }

    public void OnConfirmButton()
    {
        if (isJoinSkip)
        {
            //스킵한거면 다음 스테이지로 이어져야 함.
            Debug.Log("[RosterButton] 배틀진입버튼 눌림");
            presenter.ConfirmButton();
            playerTeamListModel.IsJoinSkip = false;
            Debug.Log($"[FarmationButton] 다음스테이지로");
        }
        else
        {
            //스킵한거 아니면 그냥 닫기
            changePositionPanel.SetActive(false);
            presenter.TeamListPrint();
        }
    }
    #endregion

}
