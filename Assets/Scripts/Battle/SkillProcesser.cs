using UnityEngine;

public class SkillProcesser : MonoBehaviour
{   //스킬에 원소 적용을 위한 프로세서, 데미지 관련한 메서드는 Unit 쪽에서 처리

    //스킬 적용 메서드
    public void ApplyElement(BattleUnit caster, BattleUnit target, Skill skill, BattleUnit[] enemyTeam)
    {
        Debug.Log("[SkillProcesser] ApplySkill 호출됨");
        /// <summary>
        /// caster : 스킬을 사용하는 유닛
        /// targetState : 스킬을 받는 유닛의 ElementalManager
        /// skill : 사용되는 스킬
        /// enemyTeam : 타격 유닛의 팀
        /// </summary>
        
        //만약 스킬이 발동이 안되는 상황이라면 혹은 발동을 안했다면 사용하지 않음
        if(!skill.IsValid() || !skill.TryUse())
        {
            return;
        }

        //스킬 데이터를 가져옴
        SkillData data = skill.Data;

        var elemental = target.GetComponent<ElementalManager>();
        if(elemental == null)
        {
            Debug.Log("ElementManager가 없음");
            return;
        }

        // 스킬의 원소 타입을 가져와서 비트플래그로 변환
        ElementType attackElement = ElementConverter.FromCSV(data.skillElement);

        if(elemental.IsReactedThisTurn)
        {
            elemental.SetElement(attackElement);
            return;
        }

        //원소 반응 판정
        ElementReaction reaction = ElementReactionResolver.Resolve(elemental.CurrentElement, attackElement);
        
        //원소 반응 발생
        if(reaction == ElementReaction.None)
        {
            elemental.SetElement(attackElement);
            Debug.Log("레이어 바꾸기");
            return;
        }
        
         switch (reaction)
         {
                case ElementReaction.Vaporize:
                    ReactionDamageProcesser.ApplyVaporize(target);
                    Debug.Log("증발");
                    break;
                case ElementReaction.ElectroShock:
                    ReactionDamageProcesser.ApplyElectroShock(enemyTeam);
                    Debug.Log("감전");
                    break;
                case ElementReaction.Overload:
                    ReactionDamageProcesser.Overload(enemyTeam);
                    elemental.ActivateOverload(3);
                    Debug.Log("과부하");
                break;
         }
            
         elemental.MarkReacted();
         elemental.ClearElement();
  
    }
 
}
