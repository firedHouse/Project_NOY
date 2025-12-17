using System;

[Serializable]
public class CharacterData : ITableData
{
    public string characterID;
    public string characterName;
    public string characterCodeName;
    public bool unlock;
    public float attackLevel1;
    public float HPLevel1;
    public int speed;
    public string ownedSkill01;
    public string ownedSkill02;
    public string ownedSkill03;
    public int level;
    public int maxLevel;
    public int position;
    public string desc;
    public string characterResource;
    public string characterSound;

    public string PrimaryID => characterID.ToString();
}
