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
    public string elementalReactionText;
    public string elementalReactionName;
    public string effectSprite;
    public string effectSound;
    public string desc;

    public string PrimaryID => elementalReactionID.ToString();
}
