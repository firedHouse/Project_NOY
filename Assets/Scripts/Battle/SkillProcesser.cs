using UnityEngine;

public class SkillProcesser : MonoBehaviour
{   //스킬에 원소 적용을 위한 프로세서, 데미지 관련한 메서드는 Unit 쪽에서 처리

    //스킬 적용 메서드
    public void ApplySkill(ElementalManager target, SkillData skill, BattleUnit[] enemyTeam)
    {
        BattleUnit unit = target.Unit;
        ElementType attackElement = ElementConverter.FromCSV(skill.skillElement);
        //속성 공격을 받았을 때, 원소 반응이 일어났었다면, 전체 무시
        if (target.isReactedThisTurn)
        {
            unit.SetElementalMark(attackElement);
            return;
        }
        //원소 반응 메서드를 가져와 현재 공격과 대상의 속성으로 원소 반응 판정
        ElementReaction reaction = ElementReactionResolver.Resolve(unit.CurrentMark, attackElement);
  
        // 공격을 받았을 때, 원소 반응이 된다면 None으로 초기화, 아니라면 공격 받은 속성으로 CurrentElement 세팅
        if (reaction != ElementReaction.None)
        {
            ReactionDamageProcesser.Apply(reaction, unit, enemyTeam); //원소 반응 데미지 처리

            target.MarkReact();
            unit.ClearMark(); // 원소 발생 후 항상 None으로 초기화
        }
        else
        {
            unit.SetElementalMark(attackElement); //원소 발생이 
        }
    }
}
