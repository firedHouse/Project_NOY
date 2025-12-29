using System.Collections.Generic;
using UnityEngine;

public class RelicComponent : MonoBehaviour
{
    private BattleUnit owner;
    private RunTimeRelic currentRelic;

    public RunTimeRelic CurrentRelic => currentRelic;

    private void Awake()
    {
        owner = GetComponent<BattleUnit>();

        if(owner == null)
        {
            Debug.LogError("유물은 플레이어 필요");
        }
    }

    //유물 장착 메서드
    public void Equip(RunTimeRelic newrelic)
    {
        if (newrelic == null || owner == null)
        { return; }
        
        if(currentRelic != null)
        {
            RelicApplier.Remove(owner, currentRelic);
        }

        currentRelic = newrelic;
        RelicApplier.Apply(owner, newrelic);

        Debug.Log($"{owner.UnitName}에게 {newrelic.itemData.itemEquipName} 장착");
    }


}
