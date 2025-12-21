using System;

[Serializable]
public class SkinData : ITableData
{
    public string skinID;
    public string characterID;
    public string imageFull;
    public string imageFace;
    public string skinSprite;
    public string desc;

    public string PrimaryID => skinID.ToString();
}
