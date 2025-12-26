using UnityEngine;
using System.Collections.Generic;

public class RewardManager : MonoBehaviour
{
    //보상 산출 (무료/유료, 중복 방지 및 사망 규칙 반영)
    //유료 아이템 산출(부활 아이템 포함)
    public List<ItemData> CreatePaidItems(bool hasDeadTeam)
    {
        List<ItemData> pool = new List<ItemData>();

        foreach (var item in TableManager.Instance.ItemTable.GetAll())
            pool.Add(item);

        if (hasDeadTeam)
        {
            pool.RemoveAll(item => item.useItemType == UsableItemType.PPPotion);
            pool.Add(TableManager.Instance.ItemTable.Get("70005")); //부활의 조각 주가
        }
        Debug.Log("유료 아이템 3개 산출 완료");
        return PickRandom(pool, 3);
    }

    //무료 아이템 산출(부활 아이템 제외, 유물 포함)
    public List<object> CreateFreeItems()
    {
        List<object> pool = new List<object>();

        Debug.Log(TableManager.Instance);
        Debug.Log(TableManager.Instance.ItemTable);
        Debug.Log(TableManager.Instance.ItemTable.GetAll());

        foreach (var item in TableManager.Instance.ItemTable.GetAll())
        {
            if (item.useItemType != UsableItemType.Revive)
                pool.Add(item);
        }
        foreach (var relic in TableManager.Instance.ItemEquipTable.GetAll())
        {
            pool.Add(relic);
        }
        Debug.Log("무료 아이템 3개 산출 완료");
        return PickRandom(pool, 3);
    }

    //리스트에서 랜덤으로 count개 선택(중복 없음)
    private List<T> PickRandom<T>(List<T> source, int count)
    {
        List<T> result = new();
        List<T> temp = new(source);
        
        while(result.Count < count && temp.Count > 0)
        {
            int index = Random.Range(0, temp.Count);
            result.Add(temp[index]);
            temp.RemoveAt(index);
        }
        Debug.Log("");
        return result;
    }

}
