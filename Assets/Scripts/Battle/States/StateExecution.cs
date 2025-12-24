using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//액션 큐 행동을 순서대로 하나씩 꺼내서 실제로 때리고, 죽으면 자리를 당기는(Shift) 로직이 수행됨
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
        //행동끝나면
        if (isExecutionFinished)
        {
            //승패체크
            if (CheckWinLoss(bm))
            {
                return;
            }
            //결과 안 나왔으면 
            //12.23 턴 종료 정산 단계로 이동
            bm.ChangeState(new StateTurnEnd());
        }
    }

    public void Exit(BattleManager bm)
    {

    }

    private IEnumerator ProcessActionQueue(BattleManager bm)
    {
        //액션큐 빠질 때까지 반복
        while (bm.ActionQueue.Count > 0)
        {
            //큐에서 행동 꺼내기
            BattleAction action = bm.ActionQueue.Dequeue();

            //공격자 체크
            if (action.User == null || action.User.IsDead)
            {
                continue;
            }

            //스킬 유효성 체크
            if (action.Skill == null || action.Skill.Data == null)
            {
                Debug.LogWarning($"익스큐션 {action.User.UnitName}의 스킬 정보가 없어서 패스.");
                continue;
            }

            //12.23 CSV 기준 0아군 1적군 
            bool isTargetEnemy = (action.Skill.Data.targetFaction == "1");

            List<BattleUnit> targetTeam;
            //상대 식별 GetOpponentTeam
            if (isTargetEnemy)
            {
                targetTeam= bm.GetOpponentTeam(action.User);
            }
            else
            {
                if (action.User is Monster)
                {
                    targetTeam = bm.EnemyTeam.Cast<BattleUnit>().ToList();//적 타겟
                }
                else
                {
                    targetTeam = bm.PlayerTeam.Cast<BattleUnit>().ToList();//아군 타겟
                }
            }

            //현재 남은 대열에 맞춰 실제로 때릴 타겟 가져오기(사망 등)
            //-> 이전 공격으로 유닛이 당겨졌으면 바뀐 위치의 유닛이 타겟이 됨.
            List<BattleUnit> realTargets = bm.GetTargetsBySkill(targetTeam, action.Skill.Data);

            //비었으면 미스
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

            //12.23 damage => calculatedValue 계산식 통합
            float calculatedValue = action.Skill.CalculateValue(action.User.AttackPower);
            SkillType sType = (SkillType)action.Skill.Data.skillType;

            //타입별 효과 연결
            foreach (BattleUnit target in realTargets)
            {
                switch (sType)
                {
                    case SkillType.Attack:
                        target.TakeDamage(calculatedValue);
                        //12.23 원소반응 추가
                        if (skillProcesser != null)
                        {
                            //타겟 팀 전체를 배열로 변환해서 전달
                            BattleUnit[] targetTeamArray = targetTeam.ToArray();
                            skillProcesser.ApplyElement(action.User, target, action.Skill, targetTeamArray);
                        }
                            break;

                    case SkillType.Heal:
                        target.Heal(calculatedValue);
                        break;

                    case SkillType.Barrier:
                        target.AddShield(calculatedValue);
                        break;

                    case SkillType.AttackBuff:
                    case SkillType.SpeedBuff:
                    case SkillType.AttackDebuff:
                    case SkillType.SpeedDebuff:
                        //버프 지속시간 적용(buffTrun)
                        target.ApplyBuff(sType, calculatedValue, action.Skill.Data.buffTurn);
                        break;
                }
            }
            //다음 공격 대기
            yield return new WaitForSeconds(1.0f);
        }
        //큐 비면 실행종료 변수 true
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

