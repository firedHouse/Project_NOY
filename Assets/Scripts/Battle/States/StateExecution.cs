using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//액션 큐 행동을 순서대로 하나씩 꺼내서 실제로 때리고, 죽으면 자리를 당기는(Shift) 로직이 수
public class StateExecution : IBattleState
{
    //실행완료했는지?
    private bool isExecutionFinished = false;
    private SkillProcesser skillProcesser;

    public void Enter(BattleManager bm)
    {
        isExecutionFinished = false;

        skillProcesser = Object.FindFirstObjectByType<SkillProcesser>();
        //행동처리 코루틴 시작
        bm.StartCoroutine(ProcessActionQueue(bm));
    }

    public void Execute(BattleManager bm)
    {
        //모든 행동이 끝나면
        if (isExecutionFinished)
        {
            //승패 체크(한 쪽 전멸)
            if (CheckWinLoss(bm))
            {
                return;
            }
            //승패 안 났으면 다음 플레이어 선택 턴 시작
            bm.ChangeState(new StatePlayerTurn());
        }
    }

    public void Exit(BattleManager bm)
    {

    }

    private IEnumerator ProcessActionQueue(BattleManager bm)
    {
        //액션큐 다 빠질 때까지 반복
        while (bm.ActionQueue.Count > 0)
        {
            //큐에서 행동 꺼내고
            BattleAction action = bm.ActionQueue.Dequeue();
            
            //공격자 생존확인
            if (action.User == null || action.User.IsDead)
            {
                continue;
            }

            //스킬 유효성 체크 1
            if (action.Skill == null || action.Skill.Data == null)
            {
                Debug.LogWarning($"익스큐션 {action.User.UnitName}의 스킬 정보가 없어서 패스.");
                continue;
            }

            //상대 식별 GetOpponentTeam
            List<BattleUnit> targetTeam = bm.GetOpponentTeam(action.User);

            //현재 남은 대열에 맞춰 실제로 때릴 타겟 가져오기(사망 등)
            //-> 이전 공격으로 유닛이 죽어 당겨졌으면 바뀐 위치의 유닛이 타겟이 됨.
            List<BattleUnit> realTargets = bm.GetTargetsBySkill(targetTeam, action.Skill.Data);

            //빈 범위면 미스처리
            if (realTargets.Count == 0)
            {
                Debug.Log($"{action.User.UnitName}의 공격 빗나감! (대상 없음)");
                //추후 Miss UI 등 연결
                continue;
            }

            //공격 연출, 지금은 로그만 출력
            //인게임 로그 출력용 리스트 컨버트 -> 문자열로 변환
            string targetNames = string.Join(", ", realTargets.ConvertAll(t => t.UnitName));
            Debug.Log($"[커맨드 실행] {action.User.UnitName} >> {action.Skill.Data.skillName} (대상: {targetNames})");

            float damage = action.Skill.CalculateValue(action.User.AttackPower);
            List<BattleUnit> opponentTeam = bm.GetOpponentTeam(action.User);
            foreach (BattleUnit target in realTargets)
            {
                target.TakeDamage(damage);

                skillProcesser.ApplyElement(action.User, target, action.Skill, opponentTeam.ToArray());
            }

            //약간 딜레이(다음공격대기)
            yield return new WaitForSeconds(0.5f);
        }

        bm.ChangeState(new StateOverload());

        //큐 비면 실행종료 true
        isExecutionFinished = true;
    }

    //승패조건체크
    private bool CheckWinLoss(BattleManager bm)
    {
        //적 전멸 -> 승리
        if (bm.EnemyTeam.Count == 0)
        {
            bm.ChangeState(new StateEnd(true));
            return true;
        }
        //아군 전멸 -> 패배
        if (bm.PlayerTeam.Count == 0)
        {
            bm.ChangeState(new StateEnd(false));
            return true;
        }
        return false;
    }
}

