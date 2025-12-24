using System.Collections.Generic;
using UnityEngine;

public class RelicComponent : MonoBehaviour
{
    private BattleUnit owner;
    private readonly List<ItemEquipData> equippedRelic = new();

    private void Awake()
    {
        owner = GetComponent<BattleUnit>();

        if(owner == null)
        {
            Debug.LogError("유물은 플레이어 필요");
        }
    }

    //유물 장착 메서드
    public void Equip(ItemEquipData relicData)
    {
        if(relicData == null || owner == null)
        { return; }

        equippedRelic.Add(relicData);
        RelicApplier.Apply(owner, relicData);

        Debug.Log($"{owner.UnitName}에게 {relicData.itemEquipName} 장착");
    }


}
