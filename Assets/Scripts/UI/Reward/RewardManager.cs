using UnityEngine;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;

public class RewardManager : MonoBehaviour
{
    public HashSet<string> usedRelics = new();

    //보상 산출 (무료/유료, 중복 방지 및 사망 규칙 반영)
    //유료 아이템 산출(부활 아이템 포함)
    public List<RunTimeItem> CreatePaidItems(bool hasDeadTeam)
    {
        List<RunTimeItem> pool = new List<RunTimeItem>();

        foreach (var item in TableManager.Instance.ItemTable.GetAll())
        {
            var type = ItemTypeResolver.ResolveItem(item.itemID);

            if (hasDeadTeam && type == UsableItemType.PPPotion) continue;

            pool.Add(new RunTimeItem(item, type));
        }

        if (hasDeadTeam)
        {
            var revive = TableManager.Instance.ItemTable.Get("70005"); //팀이 죽은 상태라면 부활의 조각 드롭
            pool.Add(new RunTimeItem(revive, UsableItemType.Revive));
        }

        Debug.Log("유료 아이템 리스트 산출 완료");
        return PickRandom(pool, 3);
    }

    //무료 아이템 산출(부활 아이템 제외, 유물 포함)
    public List<object> CreateFreeItems()
    {
        List<object> pool = new List<object>();

        Debug.Log(TableManager.Instance);
        Debug.Log(TableManager.Instance.ItemTable);
        Debug.Log(TableManager.Instance.ItemTable.GetAll());
        // 무료 아이템(소비) 산출
        foreach (var item in TableManager.Instance.ItemTable.GetAll())
        {
            var type = ItemTypeResolver.ResolveItem(item.itemID);
            if (type != UsableItemType.Revive)
            {
                pool.Add(new RunTimeItem(item, type));
            }
        }
        //유물 산출
        foreach (var relic in TableManager.Instance.ItemEquipTable.GetAll())
        {
            if (usedRelics.Contains(relic.itemEquipID)) continue;

            var state = ItemTypeResolver.ResolveRelic(relic.itemEquipID);
            pool.Add(new RunTimeRelic(relic, state));
        }

        Debug.Log("무료 아이템 리스트 산출 완료");
        return PickRandom(pool, 3);
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
