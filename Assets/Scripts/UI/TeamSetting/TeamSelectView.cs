using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelectView : MonoBehaviour
{

    // 버튼들 
    [SerializeField] private List<GameObject> selectSlots;
    public List<GameObject> SelectSlots => selectSlots;

    private void Awake()
    {
        selectSlots = gameObject.GetComponentsInChildren<GameObject>().ToList();
        // 슬롯 클릭 전까지는 비활성화
        SetAllButtonInteractable(false);
    }

    // 버튼 클릭 가능 여부 설정
    public void SetButtonInteractable(Button bt, bool isInteractable)
    {
        bt.interactable = isInteractable;
    }

    public void SetAllButtonInteractable(bool isInteractable)
    {
        Debug.Log($"[TeamSelectView] 모든 버튼 클릭 가능 : {isInteractable}");
        foreach (var slot in selectSlots)
        {
            SetButtonInteractable(slot.GetComponent<Button>(), isInteractable);
        }
    }

    public CharacterListModel GetSlotModel(GameObject slot)
    {
        return slot.GetComponent<CharacterSlot>().SlotModel;
    }
}
