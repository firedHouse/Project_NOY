using System;

[Serializable]
public class ItemEquipData : ITableData
{
    public string itemEquipID;
    public string itemEquipName;
    public int stateType;
    public float value;
    public string itemEquipTextUI;
    public string itemEquipImage;
    public string itemEquipSound;
    public string desc;

    public string PrimaryID => itemEquipID.ToString();
    public RelicStateType RelicStateType => (RelicStateType)stateType;
}
