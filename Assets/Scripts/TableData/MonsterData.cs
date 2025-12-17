using System;

[Serializable]
public class MonsterData : ITableData
{
    public string monsterID;
    public int elementType;
    public string monsterSkill01;
    public string monsterSkill02;
    public string monsterSkill03;
    public string monsterName;
    public float monsterHP;
    public float monsterAttack;
    public int monsterSpeed;
    public int monsterDropGold;
    public int monsterDropShilling;
    public string monsterSound;
    public string monsterResource;
    public int monsterClass;

    public string PrimaryID => monsterID.ToString();
}
