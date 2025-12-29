using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image image;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    public int SlotIndex; //이 아이콘이 몇 번 슬롯에 있는지

    private void Awake()
    {
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        // 드래그 중에는 최상위로 올려서 가려지지 않게 함
        transform.SetParent(originalParent.parent.parent.parent); 
        canvasGroup.blocksRaycasts = false; //슬롯이 감지되도록 Raycast 끄기
        canvasGroup.alpha = 0.6f; //반투명
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1.0f;

        //드래그가 실패했으면 원래 자리로 복귀
        if (transform.parent != originalParent)
        {
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
        }
    }
}