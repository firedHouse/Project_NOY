using System.Collections.Generic;
using UnityEngine;

public class RelicComponent : MonoBehaviour
{
    private BattleUnit owner;
    private readonly List<RunTimeRelic> equippedRelic = new();

    private void Awake()
    {
        owner = GetComponent<BattleUnit>();

        if(owner == null)
        {
            Debug.LogError("유물은 플레이어 필요");
        }
    }

    //유물 장착 메서드
    public void Equip(RunTimeRelic relic)
    {
        if(relic == null || owner == null)
        { return; }

        equippedRelic.Add(relic);
        RelicApplier.Apply(owner, relic);

        Debug.Log($"{owner.UnitName}에게 {relic.itemData.itemEquipName} 장착");
    }


}
