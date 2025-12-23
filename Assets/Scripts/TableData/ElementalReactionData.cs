using System;

[Serializable]
public class ElementalReactionData : ITableData
{
    public string elementalReactionID;
    public int elementA;
    public int elementB;
    public int attackType;
    public float damage;
    public int duration;
    public string elementalReactionName;
    public string elementEffect;
    public string effectSound;
    public string desc;

    public string PrimaryID => elementalReactionID.ToString();
}
