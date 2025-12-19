using UnityEngine;

public class StateSetup : IBattleState
{
    public void Enter(BattleManager bm)
    {
        Debug.Log("전투 설정 초기화");

        //큐, 임시 저장소 비우기
        bm.ActionQueue.Clear();
        bm.TempPlayerActions.Clear();
        bm.TempEnemyActions.Clear();
    }

    public void Execute(BattleManager bm)
    {
        //준비가 끝났으므로 플레이어턴으로 전환
        //전환 간 시간 지연이 필요한 상황이다? 여기에 시간 지연
        bm.ChangeState(new StatePlayerTurn());
    }

    public void Exit(BattleManager bm)
    {
        //아직 뭐 없음
        //계속 없을 듯?
    }

}