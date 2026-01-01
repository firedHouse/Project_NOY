using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

// 각 캐릭터마다 리스트에 표시해줄 View
[RequireComponent(typeof(Button))]
public class CharacterSlot : CharacterSlotBase
{
    //[SerializeField] private CharacterListPresenter characterListPresenter;

    
    // 캐릭터 데이터 받아와서 띄우기
    // 초기화 하면서 필요한 데이터 모두 업데이트하기
    // 슬롯 기준으로는 id만 알면 된다?
    public void UpdateCharacterSlot(CharacterListModel model)
    {
        // 캐릭터 두상 일러스트로 변경
        // 리소스 들어오기 전까지는 들어오는 데이터로 파악
        //button.image.
        id = model.CharacterID;
        slotModel = model;
        slotCharacter.text = model.CharacterName;
        SlotIllustration(model);

        //미해금 색조정
        if (model.IsUnlocked)
        {
            slotImage.color = Color.white;
        }
        else
        {
            slotImage.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        }
    }

    // 성장 레벨에 따라 일러스트 UI 변경
    public void SlotIllustration(CharacterListModel model)
    {
        int saveDataLevel = UserDataManager.Instance.GetCharacterGrade(model.CharacterID);
        Debug.Log($"[CharacterListPresenter] {saveDataLevel}");

        int skinID = int.Parse(model.CharacterSkin);
        
        if (model.CharacterSkin == null)
        {
            Debug.Log($"[CharacterSlot] {model.CharacterID}의 일러스트를 불러올 수 없습니다");
            return;
        }

        if (saveDataLevel == 2)
        {
            skinID += 1;
        }

        string skinData = TableManager.Instance.SkinTable.Get(skinID.ToString()).imageFace;
        Sprite sprite = ResourceManager.Instance.LoadSprite(skinData);

        if (sprite == null)
        {
            Debug.LogWarning($"[CharacterSlot] {model.CharacterID} 스킨 Sprite 로드 실패 : {sprite}");
        }
        slotImage.sprite = sprite;
    }
}
