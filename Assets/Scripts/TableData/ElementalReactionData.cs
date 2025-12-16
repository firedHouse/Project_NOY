using System;

[Serializable]
public class ElementalReactionData : ITableData
{
    public string elementalReactionID;
    public int elementA;
    public int elementB;
    public string elementalReactionName;
    public int attackType;
    public float damage;
    public int duration;
    public string desc;
    public string effectSound;
    public string effectResource;

    public string PrimaryID => elementalReactionID.ToString();
}
