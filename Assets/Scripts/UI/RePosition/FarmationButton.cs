using UnityEngine;
using UnityEngine.UI;

public class FarmationButton : MonoBehaviour
{
    [Header("프레젠터")]
    [SerializeField] FormationPresenter presenter;

    [Header("팀 자리 변경 패널")]
    [SerializeField] GameObject changePositionPanel;

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
        changePositionPanel.SetActive(false);
        presenter.TeamListPrint();
    }
    #endregion

}
