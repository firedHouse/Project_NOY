using UnityEngine;
using System.Collections.Generic;

public class RewardManager : MonoBehaviour
{
    public HashSet<string> usedRelics = new();

    private const string HPSmallID = "70001";
    private const string HPMiddleID = "70002";
    private const string PPSmallID = "70003";
    private const string PPMiddleID = "70004";
    private const string reviveID = "70005";

    //보상 산출 (무료/유료, 중복 방지 및 사망 규칙 반영)
    //유료 아이템 산출(부활 아이템 포함)
    public List<RunTimeItem> CreatePaidItems(bool hasDeadTeam)
    {
        List<RunTimeItem> result = new();

        // 슬롯 1 : 소형 HP
        result.Add(CreateItem(HPSmallID));

        // 슬롯 2 : 중형 HP
        result.Add(CreateItem(HPMiddleID));

        // 슬롯 3 : PP 또는 부활
        if (hasDeadTeam)
            result.Add(CreateItem(reviveID, UsableItemType.Revive));
        else
            result.Add(CreateItem(PPMiddleID));

        return result;
    }

    //무료 아이템 산출(부활 아이템 제외, 유물 포함)
    public List<object> CreateFreeItems()
    {
        List<object> pool = new();

        // 소비 아이템 4종
        pool.Add(CreateItem(HPSmallID));
        pool.Add(CreateItem(HPMiddleID));
        pool.Add(CreateItem(PPSmallID));
        pool.Add(CreateItem(PPMiddleID)); // 중형 PP 회복약

        // 유물
        foreach (var relic in TableManager.Instance.ItemEquipTable.GetAll())
        {
            if (usedRelics.Contains(relic.itemEquipID))
                continue;

            var state = ItemTypeResolver.ResolveRelic(relic.itemEquipID);
            pool.Add(new RunTimeRelic(relic, state));
        }

        return PickRandom(pool, 3);
    }

    private RunTimeItem CreateItem(string itemID, UsableItemType? overrideType = null)
    {
        var itemData = TableManager.Instance.ItemTable.Get(itemID);

        if (itemData == null)
        {
            Debug.LogError($"ItemData not found: {itemID}");
            return null;
        }

        var type = overrideType ?? ItemTypeResolver.ResolveItem(itemID);
        return new RunTimeItem(itemData, type);
    }

    //리스트에서 랜덤으로 count개 선택(중복 없음)
    private List<T> PickRandom<T>(List<T> source, int count)
    {
        List<T> result = new();
        List<T> temp = new(source);

        while (result.Count < count && temp.Count > 0)
        {
            int index = Random.Range(0, temp.Count);
            result.Add(temp[index]);
            temp.RemoveAt(index);
        }
        Debug.Log("중복 없이 랜덤으로 아이템 산출");
        return result;
    }

    public void MarkRelicUsed(RunTimeRelic relic)
    {
        usedRelics.Add(relic.itemData.itemEquipID);
    }

}
