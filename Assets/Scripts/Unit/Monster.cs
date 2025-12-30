using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

//적 유닛 뼈대 스크립트
public class Monster : BattleUnit
{
    // Jihoo, 12.29
    [SerializeField] private MonsterClass monsterPosition;

    public MonsterClass MonsterPosition => monsterPosition;

    [SerializeField] private ElementUI monsterElementUI;

    public ElementUI MonsterElementUI => monsterElementUI;
    // Jihoo

    private int dropGold; //드롭골드
    private int dropShilling; //드롭실링
    //보스 체크용
    public bool IsBoss { get; private set; }
    //12.26 보스용 턴 카운트
    private int bossTurnCount = 0;
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
        dropShilling = data.monsterDropShilling;

        InitializeBase(
            monID,
            data.monsterName,
            data.monsterHP,
            data.monsterSpeed,
            data.monsterAttack,
            pos
            );

        //리소스 로드
        string spriteName = data.monsterSprite;
        Sprite monsterSprite = ResourceManager.Instance.LoadSprite(spriteName);

        if (monsterSprite != null)
        {
            //등록
            GetComponent<SpriteRenderer>().sprite = monsterSprite;
        }
        else
        {
            Debug.LogError($"스킨 데이터를 찾을 수 없읆,,, {monsterSprite}");
        }

        // Jihoo 12.29
        // 탱딜힐 포지션 저장
        monsterPosition = (MonsterClass)data.monsterClass;
        monsterElementUI = (ElementUI)data.elementUI;

        //스킬 로드
        List<string> skillIDs = new List<string>
        {
            data.monsterSkill01,
            data.monsterSkill02,
            data.monsterSkill03
        };
        LoadSkills(skillIDs);
    }

    //보스용 오버라이드
    public override void InitializeBase(string id, string name, float hp, int spd, float atk, UnitPosition pos)
    {
        base.InitializeBase(id, name, hp, spd, atk, pos);
        //초기화 시 턴 카운트 0으로 리셋
        bossTurnCount = 0;
    }

    //스테이지 전환 간 몬스터 스펙 상승 로직
    public void ApplyBuffMultiplier(float multiplier)
    {
        //체력, 공격력 증가
        maxHP *= multiplier;
        currentHP = maxHP;
        attackPower *= multiplier;
        speed = Mathf.RoundToInt(speed * multiplier);
    }

    //AI 행동 로직
    //12.22 고도화 진행(기획서 변경사항 반영)
    //BattleManager가 메서드 사용
    public Skill ExecuteTurn()
    {
        if (skills.Count == 0)
        {
            return null;
        }

        //보스일 경우 0->1->2 사용
        if (IsBoss)
        {
            if (skills.Count < 3)
            {
                Debug.LogWarning($"보스 스킬 3개 미만");
            }
            else
            {
                //보스 스킬 0, 1, 2 순차적으로 사용
                int skillIndex = bossTurnCount % 3;
                Skill bossSkill = skills[skillIndex];

                bossTurnCount++;

                if (bossSkill.CurrentPP > 0 && bossSkill.TryUse())
                {
                    return bossSkill;
                }
                else
                {
                    //보스 PP부족, 0스킬 사용
                    Debug.Log("Boss PP 부족 0번 스킬 사용");
                    return skills[0];
                }
            }
        }

        //사용가능한(PP남은)스킬 체크
        List<Skill> validSkills = new List<Skill>();

        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].CurrentPP > 0)
            {
                validSkills.Add(skills[i]);
            }
        }

        if (validSkills.Count == 0)
        {
            return null; //발버둥치기 구현 위치
        }
        Skill selectedSkill = null;

        //조건1 30% 확률로 표식감지 시 원소반응 스킬 사용
        if (CheckOpponentHasMark())
        {
            if (Random.Range(0, 100) < 30)
            {
                //공격타입 스킬만 모으기
                List<Skill> attackSkills = new List<Skill>();
                foreach (var skill in validSkills)
                {
                    if ((SkillType)skill.Data.skillType == SkillType.Attack)
                    {
                        attackSkills.Add(skill);
                    }
                }
                if (attackSkills.Count > 0)
                {
                    selectedSkill = attackSkills[Random.Range(0, attackSkills.Count)];
                    Debug.Log($"몬스터가 {unitName}의 표식을 감지하여 공격 스킬({selectedSkill.Data.skillName}) 선택");
                    return selectedSkill;
                }
            }

            //조건2 20% 확률로 HP 1 이상 하락 시 버프스킬 사용
            if (currentHP < maxHP)
            {
                if (Random.Range(0, 100) < 20)
                {
                    //버프 계열 스킬만 모으기
                    List<Skill> buffSkills = new List<Skill>();
                    foreach (var skill in validSkills)
                    {
                        SkillType type = (SkillType)skill.Data.skillType;
                        if (type == SkillType.Barrier ||
                            type == SkillType.AttackBuff ||
                            type == SkillType.SpeedBuff)
                        {
                            buffSkills.Add(skill);
                        }
                    }

                    if (buffSkills.Count > 0)
                    {
                        selectedSkill = buffSkills[Random.Range(0, buffSkills.Count)];
                        return selectedSkill;
                    }
                }
            }

            //조건3 아군 체력이 풀이면 힐 스킬 제외
            if (IsTeamFullHP())
            {
                List<Skill> nonHealSkills = new List<Skill>();
                foreach (var skill in validSkills)
                {
                    //힐이 아닌 스킬 모아두기
                    if ((SkillType)skill.Data.skillType != SkillType.Heal)
                    {
                        nonHealSkills.Add(skill);
                    }
                }
                //힐 뺴고 남은 게 있으면 리스트 교체
                if (nonHealSkills.Count > 0)
                {
                    validSkills = nonHealSkills;
                }
            }
        }
        //모든 조건을 거치고 기본 행동 진행
        selectedSkill = validSkills[Random.Range(0, validSkills.Count)];

        if (selectedSkill.TryUse())
        {
            return selectedSkill;
        }
        return null;
    }

    //12.22
    //아군에게 표식이 달려 있는지 확인하는 메서드
    private bool CheckOpponentHasMark()
    {
        if (BattleManager.Instance == null)
        {
            return false;
        }

        var targetTeam = BattleManager.Instance.PlayerTeam;
        for (int i = 0; i < targetTeam.Count; i++)
        {
            if (targetTeam[i].CurrentMark != ElementType.None)
            {
                return true;
            }
        }
        return false;
    }

    //몬스터 팀 전원이 풀피인지 확인
    private bool IsTeamFullHP()
    {
        if (BattleManager.Instance == null)
        {
            return true;
        }

        var myTeam = BattleManager.Instance.EnemyTeam;
        for (int i = 0; i < myTeam.Count; i++)
        {
            //한 명이라도 다쳤으면 false
            if (myTeam[i].CurrentHP < myTeam[i].MaxHP)
            {
                return false;
            }
        }
        return true;
    }

    //오버라이드 Die 골드 처리
    protected override void Die()
    {
        base.Die();

        //처치 시 골드, 실링 획득
        EconomyManager.Instance.AddGold(dropGold);
        EconomyManager.Instance.AddShilling(dropShilling);
    }
}
