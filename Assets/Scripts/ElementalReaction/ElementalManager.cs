using UnityEngine;

public class ElementalManager
{
    public BattleUnit Unit { get; }
    public bool isReactedThisTurn { get; private set; }

    public ElementalManager(BattleUnit unit)
    {
        Unit = unit;
        isReactedThisTurn = false;
    }

    public void MarkReacted()
    {
        isReactedThisTurn = true;
    }

    public void ResetTurn()
    {
        isReactedThisTurn = false;
    }
}
