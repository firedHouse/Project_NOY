using UnityEngine;

public static class ItemTypeResolver
{
    public static UsableItemType ResolveItem(string itemID)
    {
        return itemID switch
        {
            "70001" => UsableItemType.HPPotion,
            "70002" => UsableItemType.HPPotion,
            "70003" => UsableItemType.PPPotion,
            "70004" => UsableItemType.PPPotion,
            "70005" => UsableItemType.Revive,
            _ => UsableItemType.HPPotion
        };
    }

    public static RelicStateType ResolveRelic(string itemID)
    {
        return itemID switch
        {
            "80001" => RelicStateType.HPBuff,
            "80002" => RelicStateType.HPBuff,
            "80003" => RelicStateType.SpeedBuff,
            "80004" => RelicStateType.AllStatBuff,
            _ => RelicStateType.HPBuff,

        };
    }
}
