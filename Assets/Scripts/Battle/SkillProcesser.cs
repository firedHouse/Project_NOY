using UnityEngine;

public class SkillProcesser : MonoBehaviour
{   //스킬에 원소 적용을 위한 프로세서, 데미지 관련한 메서드는 Unit 쪽에서 처리

    [SerializeField] private StateOverload overloadState;

    //스킬 적용 메서드
    public void ApplySkill(BattleUnit caster, BattleUnit target, Skill skill, BattleUnit[] enemyTeam)
    {
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

        //스킬 타입이 공격일 경우 데미지 입히기
        if ((SkillType)data.skillType == SkillType.Attack)
        {
            float damage = data.skillBaseValue + caster.AttackPower * data.skillFactor;
            target.TakeDamage(damage);
        }
        
        // 스킬의 원소 타입을 가져와서 비트플래그로 변환
        ElementType attackElement = (ElementType)(1 << data.skillElement);

        //원소 반응 판정
        ElementReaction reaction = ElementReactionResolver.Resolve(target.CurrentMark, attackElement);
        
        //원소 반응 발생
        if(reaction != ElementReaction.None)
        {
            switch (reaction)
            {
                case ElementReaction.Vaporize:
                    ReactionDamageProcesser.ApplyVaporize(target);
                    break;
                case ElementReaction.ElectroShock:
                    ReactionDamageProcesser.ApplyElectroShock(enemyTeam);
                    break;
                case ElementReaction.Overload:
                    ReactionDamageProcesser.ApplyOverloadNow(enemyTeam);
                    overloadState.Activate();
                    break;
            }

            target.ClearMark();
  
        }
        else
        {
            //반응이 없으면 표식만 생성
            target.SetElementalMark(attackElement);
        }
    

    }
 
}
