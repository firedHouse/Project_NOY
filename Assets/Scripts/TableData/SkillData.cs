using System;

[Serializable]
public class SkillData : ITableData
{
    public string skillID;
    public string skillName;
    public int skillBaseValue;
    public string targetFaction;
    public float skillFactor;
    public int skillType;
    public int skillElement;
    public string skillTooltip;
    public int skillPP;
    public int skillArea;
    public string skillSound;
    public string skillResource;

    public string PrimaryID => skillID.ToString();
}
