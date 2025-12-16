using System;

[Serializable]
public class SkillData : ITableData
{
    public string skillID;
    public string skillName;
    public float skillAttack;
    public int effectType;
    public int skillElement;
    public string skillTooltip;
    public int skillPP;
    public int skillArea;
    public string skillSound;
    public string skillResource;

    public string PrimaryID => skillID.ToString();
}
