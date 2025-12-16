using System;

[Serializable]
public class ItemData : ITableData
{
    public string itemID;
    public string itemName;
    public int stateType;
    public int value;
    public string desc;
    public string itemResource;
    public string itemSound;

    public string PrimaryID => itemID.ToString();
}
