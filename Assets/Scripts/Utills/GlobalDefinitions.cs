//Global Definitions (테이블 Ver 0202 반영)

public enum ElementType
{
    Fire = 1 << 0,       // 불
    Water = 1 << 1,      // 물
    Electric = 1 << 2,   // 전기
    None = 0        // 무속성
}

public enum ElementUI
{
    Fire = 0,       // 불
    Water = 1,      // 물
    Electric = 2,   // 전기
    None = 3        // 무속성
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

public enum CharacterPosition
{
    Tanker = 0,
    Dealer = 1,
    Healer = 2
}

public enum UnitType
{
    Character,
    Monster
}

public enum MonsterClass
{
    Tanker = 0,
    Dealer = 1,
    Healer = 2
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

public enum UsableItemType
{
    HPPotion = 0,
    PPPotion = 1,
    Revive = 2
}

public enum RelicStateType
{
    HPBuff = 0,
    SpeedBuff = 1,
    AttackBuff = 2,
    AllStatBuff = 3
}