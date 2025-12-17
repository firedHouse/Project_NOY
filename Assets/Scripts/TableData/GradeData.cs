using System;

[Serializable]
public class GradeData : ITableData
{
    public string gradeID;
    public string characterID;
    public int attackUP;
    public int hpUP;
    public string changeImg;
    public int needShilling1;
    public string desc;

    public string PrimaryID => gradeID.ToString();
}
