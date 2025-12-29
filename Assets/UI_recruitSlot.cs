using UnityEngine;
using UnityEngine.EventSystems;

public class UI_RecruitSlot : MonoBehaviour, IDropHandler
{
    //슬롯 타입 정의
    //0~2: 팀 슬롯
    //99: 영입 대기석
    //-1: 방출대기석
    public int SlotType;

    public void OnDrop(PointerEventData eventData)
    {
        //드래그된 물체를 가져오기
        UI_DraggableItem droppedItem = eventData.pointerDrag.GetComponent<UI_DraggableItem>();

        if (droppedItem != null)
        {
            //RecruitUI에게 누가 왔는지 전달
            RecruitUI manager = FindFirstObjectByType<RecruitUI>();
            manager.OnDropItem(droppedItem.SlotIndex, this.SlotType);
        }
    }
}