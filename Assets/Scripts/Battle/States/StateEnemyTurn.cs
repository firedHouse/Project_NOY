using System.Collections.Generic;
using System.Collections;
using UnityEngine;

//적 몬스터들이 스킬을 결정하고 행동을 예약하는 단계
public class StateEnemyTurn : IBattleState
{
    private bool isAIDone = false; // AI 연산 완료 여부

    public void Enter(BattleManager bm)
    {
        Debug.Log(">> [State] EnemyTurn: 적군이 행동을 계산 중입니다...");
        isAIDone = false;
        bm.TempEnemyActions.Clear();

        //AI 로직 시작 (연출을 위해 코루틴 사용)
        bm.StartCoroutine(ProcessAI(bm));
    }

    public void Execute(BattleManager bm)
    {
        //AI가 결정을 다 마쳤으면 순서 계산 단계(상태)로 이동
        if (isAIDone)
        {
            bm.ChangeState(new StateOrderCalculation());
        }
    }

    public void Exit(BattleManager bm) 
    { 

    }

    //AI 프로세스 코루틴 

    private IEnumerator ProcessAI(BattleManager bm)
    {
        //AI가 생각하는 척 (0.5초 딜레이), 연출을 넣을 수도 있음
        yield return new WaitForSeconds(0.5f); 


        foreach (var monster in bm.EnemyTeam)
        {
            if (monster.IsDead)
            {
                continue;
            }

            //몬스터 스크립트 내부의 AI 로직 실행 -> 사용할 스킬 반환받음
            Skill skill = monster.ExecuteTurn();

            if (skill != null)
            {
                //공격 대상 팀(아군) 가져오기
                List<BattleUnit> playerTeam = bm.GetOpponentTeam(monster);

                //GetTargetsBySkill로 타겟 리스트 반환하여 검증(맞을 애가 있는 지 체크)
                List<BattleUnit> potentialTargets = bm.GetTargetsBySkill(playerTeam, skill.Data);

                if (potentialTargets.Count > 0)
                {
                    //타겟이 존재하므로 행동 예약
                    bm.TempEnemyActions.Add(new BattleAction(monster, skill, potentialTargets[0]));
                }
                else
                {
                    Debug.Log($" {monster.UnitName} 가 {skill.Data.skillName}을 쓰려 했으나 대상이 없어 취소.");
                    //초기 설정 이후엔 바뀌거나 대기할 수도 있고
                }

            }
        }
        //연산 종료
        isAIDone = true; 
    }
}
