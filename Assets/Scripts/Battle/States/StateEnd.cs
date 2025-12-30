using UnityEngine;

public class StateEnd : IBattleState
{
    //승리여부
    private bool isWin;

    //생성자에서 승패여부 받기
    public StateEnd(bool win)
    {
        this.isWin = win;
    }

    public void Enter(BattleManager bm)
    {
        if (isWin)
        {
            if (StageManager.Instance != null &&
                StageManager.Instance.CurrentStage == 3 &&
                StageManager.Instance.CurrentRound == 5)
            {
                if (bm.ResultPanelPrefab != null)
                {
                    bm.ResultPanelPrefab.SetActive(true);
                }
            }
            else
            {
                Debug.Log("라운드 승리, 보상 패널 오픈");
                bool hasDeadPlayer = bm.HasDeadPlayer();
                bm.OpenRewardUI(hasDeadPlayer);
            }
        }
        else
        {
            Debug.Log("패배!");
            if (bm.ResultPanelPrefab != null)
            {
                bm.ResultPanelPrefab.SetActive(true);
            }
        }
    }

    public void Execute(BattleManager bm)
    {
        //돌아가기 등등 버튼 입력 기다리기? X
        //UI에게 위임
    }

    public void Exit(BattleManager bm)
    {
        //정리작업
    }
}
