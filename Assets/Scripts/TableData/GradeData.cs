using System;

[Serializable]
public class GradeData : ITableData
{
    public string gradeID;
    public string characterID;
    public int attackUP;
    public int hpUP;
    public int needShilling1;
    public string gradeInfo;
    public string changeSkin;
    public string desc;

    public string PrimaryID => gradeID.ToString();
}
