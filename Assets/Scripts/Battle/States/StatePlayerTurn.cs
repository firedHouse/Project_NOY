using UnityEngine;

//StatePlayerTurn 의 Enter => 플레이어 턴 시작이다! 스킬 버튼 띄우라 요청
//Vlew(UI)는  유저가 버튼 클릭한 걸  Presenter에 알리고
//Presenter가 BattleManager에게 유저가 이걸 골랐다 전달
//BattleManager는 데이터를 받고 턴 넘기기

public class StatePlayerTurn : IBattleState
{
    private bool isInputDone = false;


    public void Enter(BattleManager bm)

    {
        Debug.Log("[StatePlayerTurn] 유저 입력 대기중");

        isInputDone = false;
        //셋업 단계 이후(턴 종료 후 루프하기 때문에 여기단에서도 Clear)
        bm.TempPlayerActions.Clear();

        //이벤트 발행 요청 메서드
        bm.NotifyPlayerTurnStart();
    }

    public void Execute(BattleManager bm)
    {
        //Presenter 쪽에서 bm.ReceivePlayerAction()의 호출 대기
        //호출되면 isInputDone = true; 로 전환
        if (isInputDone)
        {
            //bm.ChangeState(new StateEnemyTurn());
        }
    }
    public void Exit(BattleManager bm)
    {
        //프레젠터에게 턴 종료 알리기(필요시에)
    }

    //입력 완료(battleManager가 호출)
    public void SetInputComplete()
    {
        isInputDone = true;
    }
}
