using System;

[Serializable]
public class MonsterGroupData : ITableData
{
    public string groupID;
    public string monsterID01;
    public string monsterID02;
    public string monsterID03;
    public string monsterID04;
    public string monsterID05;
    public string monsterID06;
    public string monsterID07;
    public string monsterID08;
    public string monsterID09;
    public string monsterID10;
    public string monsterID11;
    public float spawnRate01;
    public float spawnRate02;
    public float spawnRate03;
    public float spawnRate04;
    public float spawnRate05;
    public float spawnRate06;
    public float spawnRate07;
    public float spawnRate08;
    public float spawnRate09;
    public float spawnRate10;
    public float spawnRate11;

    public string PrimaryID => groupID.ToString();
}
