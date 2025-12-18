using UnityEngine;
using System.Collections.Generic;

//아군 유닛 뼈대 스크립트
public class Character : BattleUnit
{
    //초기화 메서드 필요
    public void InitializeCharacter(string charID, UnitPosition pos)
    {
        //캐릭터 데이터 조회
        CharacterData data = TableManager.Instance.CharacterTable.Get(charID);

        if (data != null)
        {
            Debug.LogError($"캐릭터 데이터를 찾지 못했읍니다 {charID}");
            return;
        }
        //MVP 이후엔 레벨 계산 필요
        InitializeBase(
            charID,
            data.characterName,
            data.HPLevel1,
            data.speed,
            data.attackLevel1,
            pos
        );

        //리소스 로드

        //스킬 로두
        List<string> skillIDs = new List<string>()
        {
            data.ownedSkill01, 
            data.ownedSkill02,
            data.ownedSkill03
        };
        LoadSkills(skillIDs);
    }
    //스킬 사용 함수 (인덱스: 0, 1, 2)
    public void UseSkill(int skillIndex, BattleUnit target)
    {
        if (skillIndex < 0 || skillIndex >= skills.Count)
        {
            return;
        }

        Skill skill = skills[skillIndex];

        //PP 체크 및 소모
        if (skill.TryUse())
        {
            //데미지 계산 및 적용 로직은 BattleManager 에서 처리 예정
            Debug.Log($"{unitName} 가 스킬: {skill.Data.skillName} 사용. (남은 PP: {skill.CurrentPP})");
        }
        else
        {
            Debug.Log("PP 부족!");
        }
    }

}
