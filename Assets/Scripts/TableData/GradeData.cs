using System;

[Serializable]
public class GradeData : ITableData
{
    public string characterID;
    public int attackUP1;
    public int attackUP2;
    public int hpUP1;
    public int hpUP2;
    public string changeImg;
    public int needShilling1;
    public int needShilling2;
    public string desc;

    public string PrimaryID => characterID.ToString();
}
