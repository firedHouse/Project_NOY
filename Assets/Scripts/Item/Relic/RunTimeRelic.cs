using UnityEngine;

public class RunTimeRelic
{
    public ItemEquipData itemData { get; }
    public RelicStateType stateType { get; }

    public RunTimeRelic(ItemEquipData data, RelicStateType state)
    {
        itemData = data;
        stateType = state;
    }

}
