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

    public void DeleteSlotIllustration()
    {
        selectedCharacterIllust.sprite = null;
    }
    // 
    public void SetSlotIllustration(CharacterListModel model)
    {
        int skinID = int.Parse(model.CharacterSkin);

        if (model.CharacterSkin == null)
        {
            Debug.Log($"[CharacterSlot] {model.CharacterID}의 일러스트를 불러올 수 없습니다");
            return;
        }

        if (model.Level == 2)
        {
            skinID += 1;
        }

        string skinData = TableManager.Instance.SkinTable.Get(skinID.ToString()).imageFace;
        Sprite sprite = ResourceManager.Instance.LoadSprite(skinData);

        if (sprite == null)
        {
            Debug.LogWarning($"[CharacterSlot] {model.CharacterID} 스킨 Sprite 로드 실패 : {sprite}");
        }

        selectedCharacterIllust.sprite = sprite;
    }
    
}
