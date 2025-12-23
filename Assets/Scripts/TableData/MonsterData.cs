using System;

[Serializable]
public class MonsterData : ITableData
{
    public string monsterID;
    public int elementType;
    public int elementUI;
    public string monsterSkill01;
    public string monsterSkill02;
    public string monsterSkill03;
    public float monsterHP;
    public float monsterAttack;
    public int monsterSpeed;
    public int monsterDropGold;
    public int monsterDropShilling;
    public int monsterClass;
    public string monsterName;
    public string monsterSound;
    public string monsterSprite;
    public string desc;

    public string PrimaryID => monsterID.ToString();
}
