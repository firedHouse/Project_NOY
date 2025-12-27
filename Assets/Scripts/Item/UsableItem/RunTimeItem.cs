using UnityEngine;

public class RunTimeItem
{
    public ItemData itemData { get; }
    public UsableItemType useType { get; }

    public RunTimeItem(ItemData Data, UsableItemType Type)
    {
        itemData = Data;
        useType = Type;
    }
}
