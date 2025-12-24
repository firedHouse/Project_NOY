using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//버프/디버프 지속시간 감소와 과부하 처리할 상태
public class StateTurnEnd : IBattleState
{
    //정산끝
    private bool isFinished = false;
    public void Enter(BattleManager bm)
    {
        isFinished = false;
        bm.StartCoroutine(ProcessTurnEnd(bm));
    }
    public void Execute(BattleManager bm)
    {
        if (isFinished)
        {
            // 승패 체크 후 다음 턴으로 넘기기
            if (CheckWinLoss(bm))
            {
                return;
            }
            //정산 종료 이후 다시 플레이어 턴 시작
            Debug.Log("정산 완료 -> 다음 턴 시작");
            bm.NotifyPlayerTurnStart(); 
            bm.ChangeState(new StatePlayerTurn());
        }
    }
    public void Exit(BattleManager bm)
    {

    }

    //턴종료 프로세스(유닛이 각자 처리하도록 메서드 추가)
    private IEnumerator ProcessTurnEnd(BattleManager bm)
    {
        Debug.Log("턴 정산 시작");

        //12.23 반복도중 유닛 사망시 리스트 바뀌며 생기는 문제 해결용
        var playerList = new List<Character>(bm.PlayerTeam);
        var enemyList = new List<Monster>(bm.EnemyTeam);
        //아군
        foreach (var unit in playerList)
        {
            if (!unit.IsDead)
            {
                unit.OnTurnEnd(bm.PlayerTeam);
            }
        }
        //적군
        foreach (var unit in enemyList)
        {
            if (!unit.IsDead)
            {
                unit.OnTurnEnd(bm.EnemyTeam);
            }
        }

        yield return new WaitForSeconds(1.5f);
        isFinished = true;
    }

    private bool CheckWinLoss(BattleManager bm)
    {
        if (bm.EnemyTeam.Count == 0)
        {
            bm.ChangeState(new StateEnd(true));
            return true;
        }
        if (bm.PlayerTeam.Count == 0)
        {
            bm.ChangeState(new StateEnd(false));
            return true;
        }
        return false;
    }
}
