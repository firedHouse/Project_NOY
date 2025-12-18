using System;

[Serializable]
public class StageData : ITableData
{
    public string stageID;
    public string monsterID;
    public int monsterGroup;
    public float value;

    public string PrimaryID => stageID.ToString();
}
