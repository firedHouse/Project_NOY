using System;

[Serializable]
public class MonsterGroupData : ITableData
{
    public string group;
    public string monsterID;
    public float spawnRate;
    public string desc;

    public string PrimaryID => group.ToString();
}
