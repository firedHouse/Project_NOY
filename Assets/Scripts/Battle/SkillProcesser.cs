using UnityEngine;

public class SkillProcesser : MonoBehaviour
{   //스킬에 원소 적용을 위한 프로세서, 데미지 관련한 메서드는 Unit 쪽에서 처리

    //과부하는 3턴 고정
    private const int overloadDuration = 3;

    //스킬 적용 메서드
    public void ApplyElement(BattleUnit caster, BattleUnit target, Skill skill, BattleUnit[] enemyTeam)
    {
        /// <summary>
        /// caster : 스킬을 사용하는 유닛
        /// targetState : 스킬을 받는 유닛의 ElementalManager
        /// skill : 사용되는 스킬
        /// enemyTeam : 타격 유닛의 팀
        /// </summary>
        Debug.Log("[SkillProcesser] ApplySkill 호출됨");
        
        var elemental = target.GetComponent<ElementalManager>();
        if(elemental == null)
        {
            Debug.Log("ElementManager가 없음");
            return;
        }

        // 스킬의 원소 타입을 CSV에서 가져옴
        ElementType attackElement = ElementConverter.FromCSV(skill.Data.skillElement);

        //원소 반응 판정
        ElementReaction reaction = ElementReactionResolver.Resolve(elemental.currentElement, attackElement);
        
        //표식 부여
        if(reaction == ElementReaction.None)
        {
            elemental.SetElement(attackElement);
            return;
        }

        //이번 턴에 원소 반응을 했었다면 Return;
        if (!elemental.CanReact())
        {
            return;
        }

        switch (reaction)
        {
            case ElementReaction.Vaporize:
                Debug.Log("증발");
                ReactionDamageProcesser.ApplyVaporize(target);
                break;
            case ElementReaction.ElectroShock:
                Debug.Log("감전");
                ReactionDamageProcesser.ApplyElectroShock(enemyTeam);
                break;
            case ElementReaction.Overload:
                Debug.Log("과부하");
                elemental.ActiveOverload(overloadDuration);
            break;
        }
            
         elemental.MarkReacted();
         elemental.ClearElement();
  
    }
 
}
