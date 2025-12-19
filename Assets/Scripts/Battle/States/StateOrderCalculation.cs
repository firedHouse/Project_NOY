using UnityEngine;
using System.Linq;
using System.Collections.Generic;

//양측이 예약한 행동(각 TempActions)을 모아두고
//속도 기반으로 누가 먼저 때릴지 정하고 ActionQueue 에 줄을 세우는 단계(상태)

public class StateOrderCalculation : IBattleState
{
    public void Enter(BattleManager bm)
    {
        //실행 큐 초기화
        bm.ActionQueue.Clear();

        //속도 합산(생존한 유닛의 Speed 합)
        int playerSpeedSum = bm.PlayerTeam.Where(u => !u.IsDead).Sum(u => u.Speed);
        int enemySpeedSum = bm.EnemyTeam.Where(u => !u.IsDead).Sum(u => u.Speed);

        //선공판정, 속도 같으면 플레이어 우선
        bool isPlayerFirst = (playerSpeedSum >= enemySpeedSum);
        Debug.Log($"속도 체크: 아군({playerSpeedSum}) vs 적군({enemySpeedSum}) -> 선공은 {(isPlayerFirst ? "플레이어" : "적군")}");


        //선후공 판정(선공 먼저 액션큐에 등록)
        if (isPlayerFirst)
        {
            EnqueueTeamActions(bm, bm.TempPlayerActions);
            EnqueueTeamActions(bm, bm.TempEnemyActions);
        }
        else
        {
            EnqueueTeamActions(bm, bm.TempEnemyActions);
            EnqueueTeamActions(bm, bm.TempPlayerActions);
        }

    }

    //리스트에 있는 행동들을 큐에 넣는 함수
    private void EnqueueTeamActions(BattleManager bm, List<BattleAction> actions)
    {
        foreach (var action in actions)
        {
            bm.ActionQueue.Enqueue(action);
        }
    }

    public void Execute(BattleManager bm)
    {
        bm.ChangeState(new StateExecution());
    }

    public void Exit(BattleManager bm)
    {
        //
    }
}
