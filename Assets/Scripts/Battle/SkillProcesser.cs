using UnityEngine;

public class SkillProcesser : MonoBehaviour
{   //스킬에 원소 적용을 위한 프로세서, 데미지 관련한 메서드는 Unit 쪽에서 처리
    /*

    //스킬 적용 메서드
    public void ApplySkill(Unit target, SkillData skill)
    {
        ElementType attackElement = ElementConverter.FromCSV(skill.skillElement);
        //속성 공격을 받았을 때, 원소 반응이 일어났었다면, 전체 무시
        if (target.isReactionThisTurn)
        {
            target.SetElement(attackElement);
            return;
        }
        //원소 반응 메서드를 가져와 현재 공격과 대상의 속성으로 원소 반응 판정
        ElementReaction reaction = ElementReactionResolver.Resolve(target.currentElement, attackElement);

        // 공격을 받았을 때, 원소 반응이 된다면 None으로 초기화, 아니라면 공격 받은 속성으로 CurrentElement 세팅
        if (reaction != ElementReaction.None)
        {
            TriggerReaction(target, reaction);

            target.MarkReact();
            target.ClearElemental(); // 원소 발생 후 항상 None으로 초기화
        }
        else
        {
            target.SetElement(attackElement); //원소 발생이 
        }
    }

    //원소 반응 트리거 함수
    private void TriggerReaction(Unit target, ElementReaction reaction)
    {
        switch(reaction)
        {
            case ElementReaction.Vaporize:
                Debug.Log("증발 발생");
                break;
            case ElementReaction.ElectroShock:
                Debug.Log($"감전 발생");
                break;
            case ElementReaction.Overload:
                Debug.Log($"과부하 발생");
                break;
            default:
                Debug.Log("Debug.Log : None");
                break;
        }
     }
*/
}
