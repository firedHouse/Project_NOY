using UnityEngine;
using System.Collections.Generic;

//적 유닛 뼈대 스크립트
public class Monster : BattleUnit
{
    [Header("Drop Info")]
    [SerializeField] private int dropGold; //드롭골드 체크용(임시)

    //몬스터 역할군 딜 힐 탱, 나중에 AI 로직에서 사용
    public MonsterClass Role { get; private set; }

    //보스 체크용
    public bool IsBoss { get; private set; }

    public void InitializeMonster(string monID, UnitPosition pos, bool isBossUnit)
    {
        //데이터 테이블 불러오기
        MonsterData data = TableManager.Instance.MonsterTable.Get(monID);

        if (data == null)
        {
            Debug.LogError($"몬스터 데이터를 찾지 못했습니다: {monID}");
            return;
        }

        //보스, 드롭골드
        IsBoss = isBossUnit;
        dropGold = data.monsterDropGold;

        InitializeBase(
            monID,
            data.monsterName,
            data.monsterHP,
            data.monsterSpeed,
            data.monsterAttack,
            pos
            );

        //리소스 로드 data.monsterResource 등

        //스킬 로드
        List<string> skillIDs = new List<string>
        {
            data.monsterSkill01,
            data.monsterSkill02,
            data.monsterSkill03
        };
        LoadSkills(skillIDs);
    }

    //AI 행동 로직
    //BattleManager가 메서드 사용
    public Skill ExecuteTurn()
    {
        if (skills.Count == 0)
        {
            return null;
        }

        //MVP 단계 AI: 보유 스킬 중 하나를 랜덤하게 선택
        int randomIndex = Random.Range(0, skills.Count);
        Skill selectedSkill = skills[randomIndex];

        //12.16 몬스터의 PP제한은 기획 상엔 일단 없지만 PP는 존재하니 체크
        if (selectedSkill.TryUse())
        {
            return selectedSkill;
        }
        return null;
    }

    //오버라이드 Die 골드 처리
    protected override void Die()
    {
        //처치 시 골드, 실링 획득
        //ResourceManager.Instnce.AddGold(dropGold)? 정도
        base.Die();
    }
}
