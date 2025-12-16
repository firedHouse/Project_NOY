using UnityEngine;

public struct ReactionKey
{
    public ElementalReactionData elementA;
    public ElementalReactionData elementB;

    public ReactionKey(ElementalReactionData markElement, ElementalReactionData attackElement)
    {
        elementA = markElement;
        elementB = attackElement;
    }
}
