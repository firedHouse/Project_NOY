using System;

[Serializable]
public class StageData : ITableData
{
    public string stageID;
    public string monsterID01;
    public string monsterID02;
    public string monsterID03;
    public float value01;
    public float value02;
    public float value03;
    public string bossID;

    public string PrimaryID => stageID.ToString();
}
