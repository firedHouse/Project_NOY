using System;

[Serializable]
public class SkillData : ITableData
{
    public string skillID;
    public int skillBaseValue;
    public string targetFaction;
    public float skillFactor;
    public int skillType;
    public int buffTurn;
    public int skillElement;
    public int skillPP;
    public int skillArea;
    public string skillName;
    public string skillTooltip;
    public string skillSound;
    public string skillIcon;
    public string skillSprite;
    public string desc;

    public string PrimaryID => skillID.ToString();
}
