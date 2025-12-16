using System.Collections.Generic;
using UnityEngine;

public class ElementalReactionDatabase : MonoBehaviour
{
    private Dictionary<ReactionKey, ElementalReactionData> table;

    //ElementalReactionData에서 mark=ElementA, trigger=ElementB를 받아 Dictionary에서 검색 후 반환
    public ElementalReactionData GetReaction(ElementalReactionData mark, ElementalReactionData trigger)
    {
        table.TryGetValue(new ReactionKey(mark, trigger), out ElementalReactionData reactionData);

        return reactionData;
    }
}
