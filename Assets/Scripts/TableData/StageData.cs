using System;

[Serializable]
public class StageData : ITableData
{
    public string stageID;
    public string groupID;
    public string bossID;
    public string image;
    public string sound;
    public string desc;

    public string PrimaryID => stageID.ToString();
}
