using UnityEngine;

public class ElementalManager : MonoBehaviour
{
    // 현재 상태

    public bool IsReactedThisTurn { get; private set; }

    public ElementType elementType => ElementLayerUtil.LayerToElement(gameObject.layer);

    public int overloadRemainTurn { get; private set; }

    public bool IsOverloadActive => overloadRemainTurn > 0;

    //원소 레이어

    public ElementType CurrentElement
    {
        get => ElementLayerUtil.LayerToElement(gameObject.layer);
    }

    public void SetElement(ElementType element)
    {
        int layer = ElementLayerUtil.ElementToLayer(element);

        if(layer < 0)
        {
            Debug.Log($"{element} 레이어 없음");
            return;
        }

        gameObject.layer = layer;
        Debug.Log($"{element} 부여");
    }

    public void ClearElement()
    {
        gameObject.layer = LayerMask.NameToLayer("None");
        Debug.Log("원소 초기화");
    }

    //원소 반응

    public void MarkReacted()
    {
        IsReactedThisTurn = true;
    }
    
    public void ResetTurn()
    {
        IsReactedThisTurn = false;
    }

    //과부하

    public void ActivateOverload(int duration)
    {
        if(IsOverloadActive)
            return;

        overloadRemainTurn = duration;
        Debug.Log($"과부하 활성화 {duration}");
    }

    public void ConsumeOverloadTurn()
    {
        overloadRemainTurn--;

        if (overloadRemainTurn == 0)
        {
            Debug.Log("과부하 종료");   
        }
    }
}
