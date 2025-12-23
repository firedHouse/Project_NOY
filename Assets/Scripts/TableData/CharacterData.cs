using System;

[Serializable]
public class CharacterData : ITableData
{
    public string characterID;
    public bool unlock;
    public float attackLevel1;
    public float HPLevel1;
    public int speed;
    public int elementUI;
    public string ownedSkill01;
    public string ownedSkill02;
    public string ownedSkill03;
    public int level;
    public int maxLevel;
    public int position;
    public string characterName;
    public string characterCodeName;
    public string characterDialogue;
    public string characterInfo;
    public string characterSkin;
    public string desc;

    public string PrimaryID => characterID.ToString();
}
