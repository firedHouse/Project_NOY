using System;

[Serializable]
public class ItemData : ITableData
{
    public string itemID;
    public int itemCost;
    public int stateType;
    public int value;
    public string itemName;
    public string itemTextUI;
    public string itemImage;
    public string itemSound;
    public string desc;

    public string PrimaryID => itemID.ToString();
}
