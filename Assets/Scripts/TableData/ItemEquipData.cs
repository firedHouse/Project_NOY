using System;

[Serializable]
public class ItemEquipData : ITableData
{
    public string itemEquipID;
    public string itemEquipName;
    public int stateType;
    public float value;
    public string desc;
    public string ItemEquipResource;
    public string ItemEquipSound;

    public string PrimaryID => itemEquipID.ToString();
}
