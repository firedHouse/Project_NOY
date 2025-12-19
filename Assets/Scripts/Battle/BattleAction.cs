using UnityEngine;

//커맨드 패턴의 Command 

//user 누가, target 누구를, skill 어떤 스킬로 공격할 지 라는 행동 요청을 
//바로 처리하지 않고 BattleAction이라는 하나의 객체로 캡슐화
public class BattleAction
{
    public BattleUnit User;     //행동 주체
    public Skill Skill;         //사용할 스킬
    public BattleUnit Target;   //대상

    //전투 행동을 담을 캡슐
    public BattleAction(BattleUnit user, Skill skill, BattleUnit target)
    {
        User = user;
        Skill = skill;
        Target = target;
    }
}
