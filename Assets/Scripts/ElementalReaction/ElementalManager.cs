using UnityEngine;

public class ElementalManager
{
    public BattleUnit Unit { get; }
    public bool isReactedThisTurn { get; set; }

    public ElementalManager(BattleUnit unit)
    {
        Unit = unit;
        isReactedThisTurn = false;
    }

    public void MarkReact()
    {
        isReactedThisTurn = true;
    }
    
    public void ResetReact()
    {
        isReactedThisTurn = false;
    }
}
