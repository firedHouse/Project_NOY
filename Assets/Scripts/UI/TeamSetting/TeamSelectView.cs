using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelectView : MonoBehaviour
{

    // 버튼들 
    [SerializeField] private CharacterSlot[] slots;
    public CharacterSlot[] Slots => slots;
    
    private void Start()
    {
        slots = gameObject.GetComponentsInChildren<CharacterSlot>().ToArray();
        // 슬롯 클릭 전까지는 비활성화
        SetAllButtonInteractable(false);
    }

    // 버튼 클릭 가능 여부 설정
    public void SetButtonInteractable(CharacterSlot slot, bool isInteractable)
    {
        slot.SlotButton.interactable = isInteractable;
    }

    public void SetAllButtonInteractable(bool isInteractable)
    {
        // 버튼 리스트에서 
        foreach (var slot in slots)
        {
            SetButtonInteractable(slot, isInteractable);
        }
        Debug.Log($"[TeamSelectView] 모든 버튼 클릭 가능 : {isInteractable}");
    }

    // 해당 슬롯에서 캐릭터 모델을 반환
    public CharacterListModel GetSlotModel(CharacterSlot slot)
    {
        return slot.SlotModel;
    }
    
    // 캐릭터 모델을 슬롯에 넣어줌
    // 파라미터는 버튼 아니면 게임 오브젝트
    public void SetSlotModel(CharacterSlot slot, CharacterListModel model)
    {
        slot.SlotModel = model;
    }
        
    // 이벤트로 버튼 내 슬롯에 모델이 변경될 때마다 모델 리스트 갱신 
}
