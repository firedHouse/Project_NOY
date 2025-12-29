using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelectView : MonoBehaviour
{

    // 버튼들 
    [SerializeField] public MemberSlot[] slots;
    public MemberSlot[] Slots => slots;
    
    private void Start()
    {
        slots = gameObject.GetComponentsInChildren<MemberSlot>().ToArray();
        // 슬롯 클릭 전까지는 비활성화
        SetAllButtonInteractable(false);
    }

    // 버튼 클릭 가능 여부 설정
    public void SetButtonInteractable(MemberSlot slot, bool isInteractable)
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
    public CharacterListModel GetSlotModel(MemberSlot slot)
    {
        return slot.SlotModel;
    }
    
    // 캐릭터 모델을 슬롯에 넣어줌
    // 파라미터는 버튼 아니면 게임 오브젝트
    public void SetSlotModel(MemberSlot slot, CharacterListModel model)
    {
        slot.SlotModel = model;
    }
        
    // 이벤트로 버튼 내 슬롯에 모델이 변경될 때마다 모델 리스트 갱신 
}

// 프레젠터에서 현재 보여주는 모델을 지정해두고 전/중/후열 슬롯을 클릭하면 모델을 슬롯에 저장하는 식으로 구현했는데 같은 캐릭터를 전/중/후열에 중복으로 저장이 가등한 상황
// 이미 열에 들어간 애를 다른 열에 저장하면 원래 있던 열에서는 지워지도록 하고 싶음