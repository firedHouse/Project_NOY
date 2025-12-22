//Global Definitions (테이블 Ver 0106 반영)

public enum ElementType
{
    Fire = 1 << 0,       // 불
    Water = 1 << 1,      // 물
    Electric = 1 << 2,   // 전기
    None = 0        // 무속성
}

public enum ElementReaction
{
    None = 0,
    Vaporize = 1,
    ElectroShock = 2,
    Overload = 3
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
    Barrier = 2,
    AttackBuff = 3,
    SpeedBuff = 4,
    AttackDebuff = 5,
    SpeedDebuff = 6
}