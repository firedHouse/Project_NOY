using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//�׼� ť �ൿ�� ������� �ϳ��� ������ ������ ������, ������ �ڸ��� ����(Shift) ������ ��
public class StateExecution : IBattleState
{
    //����Ϸ��ߴ���?
    private bool isExecutionFinished = false;
    private SkillProcesser skillProcesser;

    public void Enter(BattleManager bm)
    {
        isExecutionFinished = false;

        skillProcesser = Object.FindFirstObjectByType<SkillProcesser>();
        //�ൿó�� �ڷ�ƾ ����
        bm.StartCoroutine(ProcessActionQueue(bm));
    }

    public void Execute(BattleManager bm)
    {
        //��� �ൿ�� ������
        if (isExecutionFinished)
        {
            //���� üũ(�� �� ����)
            if (CheckWinLoss(bm))
            {
                return;
            }
            //���� �� ������ ���� �÷��̾� ���� �� ����
            bm.ChangeState(new StatePlayerTurn());
        }
    }

    public void Exit(BattleManager bm)
    {

    }

    private IEnumerator ProcessActionQueue(BattleManager bm)
    {
        //�׼�ť �� ���� ������ �ݺ�
        while (bm.ActionQueue.Count > 0)
        {
            //ť���� �ൿ ������
            BattleAction action = bm.ActionQueue.Dequeue();

            //������ ����Ȯ��
            if (action.User == null || action.User.IsDead)
            {
                continue;
            }

            //��ų ��ȿ�� üũ 1
            if (action.Skill == null || action.Skill.Data == null)
            {
                Debug.LogWarning($"�ͽ�ť�� {action.User.UnitName}�� ��ų ������ ��� �н�.");
                continue;
            }

            //��� �ĺ� GetOpponentTeam
            List<BattleUnit> targetTeam = bm.GetOpponentTeam(action.User);

            //12.23 ����/�� ��ų�̸� �Ʊ� Ÿ�ٵǵ��� �����ϴ� ���� �߰�
            SkillType sType = (SkillType)action.Skill.Data.skillType;
            if (sType == SkillType.Heal || sType == SkillType.SpeedBuff ||
               sType == SkillType.AttackBuff || sType == SkillType.SpeedBuff)
            {
                if (action.User is Monster)
                {
                    targetTeam = bm.EnemyTeam.Cast<BattleUnit>().ToList();
                }
                else
                {
                    targetTeam = bm.PlayerTeam.Cast<BattleUnit>().ToList();
                }
            }


            //���� ���� �뿭�� ���� ������ ���� Ÿ�� ��������(��� ��)
            //-> ���� �������� ������ �׾� ��������� �ٲ� ��ġ�� ������ Ÿ���� ��.
            List<BattleUnit> realTargets = bm.GetTargetsBySkill(targetTeam, action.Skill.Data);

            //�� ������ �̽�ó��
            if (realTargets.Count == 0)
            {
                Debug.Log($"{action.User.UnitName}�� ���� ������! (��� ����)");
                //���� Miss UI �� ����
                continue;
            }

            //���� ����, ������ �α׸� ���
            //�ΰ��� �α� ��¿� ����Ʈ ����Ʈ -> ���ڿ��� ��ȯ
            string targetNames = string.Join(", ", realTargets.ConvertAll(t => t.UnitName));
            Debug.Log($"[Ŀ�ǵ� ����] {action.User.UnitName} >> {action.Skill.Data.skillName} (���: {targetNames})");

            //12.23 damage => calculatedValue ��ġ ������� Ȯ��
            float calculatedValue = action.Skill.CalculateValue(action.User.AttackPower);

            //�� Ÿ�Ժ� ��� ����
            foreach (BattleUnit target in realTargets)
            {
                switch (sType)
                {
                    case SkillType.Attack:
                        target.TakeDamage(calculatedValue);
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
                        target.ApplyBuff(sType, calculatedValue);
                        break;
                }
            }

            //�ణ ������(�������ݴ��)
            yield return new WaitForSeconds(1.0f);
        }

        bm.ChangeState(new StateOverload());

        //ť ��� �������� true
        isExecutionFinished = true;
    }

    //��������üũ
    private bool CheckWinLoss(BattleManager bm)
    {
        //�� ���� -> �¸�
        if (bm.EnemyTeam.Count == 0)
        {
            bm.ChangeState(new StateEnd(true));
            return true;
        }
        //�Ʊ� ���� -> �й�
        if (bm.PlayerTeam.Count == 0)
        {
            bm.ChangeState(new StateEnd(false));
            return true;
        }
        return false;
    }
}

