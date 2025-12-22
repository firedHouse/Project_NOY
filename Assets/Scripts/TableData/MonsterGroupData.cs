using System;

[Serializable]
public class MonsterGroupData : ITableData
{
    public string groupID;
    public string monsterID;
    public float spawnRate;
    public string desc;

    //12.22 수정사항
    //유니크하게 아이디 생성하도록 변경(추후 monsterID01~ 10 이런 방식으로 변경되면 기존 로직을 따름)
    public string PrimaryID => $"{groupID}_{monsterID}";
}
