using System.Collections.Generic;
using UnityEngine;

//MonsterGroupData 에 함수를 생성해도 매번 파싱 툴이 스크립트를 덮어씌우기에
//파싱 툴에 영향받지 않기 위해 분리한 MonsterGroupData 메서드
public static class MonsterGroupDataExtensions
{ 
    //몬스터ID와 스폰확률을 연결시켜주는 메서드
    public static List<(string id, float rate)> GetSpawnList(this MonsterGroupData data)
    {
        var list = new List<(string, float)>();

        //유효성 체크해서 연결하고 리스트에 담기
        AddIfValid(list, data.monsterID01, data.spawnRate01);
        AddIfValid(list, data.monsterID02, data.spawnRate02);
        AddIfValid(list, data.monsterID03, data.spawnRate03);
        AddIfValid(list, data.monsterID04, data.spawnRate04);
        AddIfValid(list, data.monsterID05, data.spawnRate05);
        AddIfValid(list, data.monsterID06, data.spawnRate06);
        AddIfValid(list, data.monsterID07, data.spawnRate07);
        AddIfValid(list, data.monsterID08, data.spawnRate08);
        AddIfValid(list, data.monsterID09, data.spawnRate09);
        AddIfValid(list, data.monsterID10, data.spawnRate10);
        AddIfValid(list, data.monsterID11, data.spawnRate11);

        return list;
    }

    //아이디, 유효성 체크
    private static void AddIfValid(List<(string, float)> list, string id, float rate)
    {
        //ID가 유효하고 확률이 0보다 클 때만 추가
        if (!string.IsNullOrEmpty(id) && id != "null" && rate > 0)
        {
            list.Add((id, rate));
        }
    }
}