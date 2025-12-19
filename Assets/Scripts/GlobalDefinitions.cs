//Global Definitions (테이블 Ver 0106 반영)

public enum ElementType
{
    Fire = 0,       // 불
    Water = 1,      // 물
    Electric = 2,   // 전기
    None = 3        // 무속성
}

public enum UnitPosition
{
    Front = 0,      // 전열
    Mid = 1,        // 중열
    Back = 2        // 후열
}

public enum UnitType
{
    Character,
    Monster
}

public enum MonsterClass
{
    Dealer = 0,
    Healer = 1,
    Tanker = 2
}

public enum SkillArea //SkillTable 참조 참조
{
    Front = 0,
    FrontMid = 1,
    Mid = 2,
    MidBack = 3,
    Back = 4,
    FrontBack = 5,
    All = 6             
}
public enum SkillType
{
    Attack = 0,
    Heal = 1,
    Buff = 2,
    Debuff = 3
}