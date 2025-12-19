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
            Debug.Log("승리!");
        }
        else
        {
            Debug.Log("패배!");
        }
    }

    public void Execute(BattleManager bm)
    {
        //돌아가기 등등 버튼 입력 기다리기?
    }

    public void Exit(BattleManager bm)
    {
        //정리작업
    }
}
