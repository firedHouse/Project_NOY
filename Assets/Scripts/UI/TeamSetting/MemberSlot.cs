using System;
using UnityEngine;
using UnityEngine.UI;

public class MemberSlot : CharacterSlotBase
{
    [SerializeField] public int memberPosition;
    [SerializeField] public Text selectedCharacterName;
    [SerializeField] public Image selectedCharacterIllust;

    public int MemberPosition => memberPosition;
    private void Start()
    {
        switch (transform.parent.gameObject.name)
        {
            case "FrontCharacterSelectPanel":
                memberPosition = (int)UnitPosition.Front;
                break;
            case "MiddleCharacterSelectPanel":
                memberPosition = (int)UnitPosition.Mid;
                break;
            case "RoarCharacterSelectPanel":
                memberPosition = (int)UnitPosition.Back;
                break;
        }
    }

    public void UpdateButtonAvailable(bool isAvailable)
    {
        slotButton.interactable = isAvailable;
    }

    // 
    
}
