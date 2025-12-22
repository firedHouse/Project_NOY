using UnityEngine;
using UnityEngine.Rendering;

public class ElementalManager : MonoBehaviour
{
    // 현재 상태

    public ElementType currentElement => ElementLayerUtil.LayerToElement(gameObject.layer);

    public bool isReactedThisTurn { get; private set; }


    // ===== 원소 =====
    public bool CanReact()
    {
        return !isReactedThisTurn;
    }

    public void MarkReacted()
    {
        isReactedThisTurn = true;
    }

    // 턴 종료 시 호출 (Overload 소모 시점과 동일)
    public void ResetTurn()
    {
        isReactedThisTurn = false;
    }

    //원소 적용 및 초기화
    public void SetElement(ElementType element)
    {
        gameObject.layer = ElementLayerUtil.ElementToLayer(element);

        Debug.Log($"{element} 부여");
    }

    public void ClearElement()
    {
        gameObject.layer = LayerMask.NameToLayer("None");
        
        Debug.Log("원소 초기화");
    }

    //과부하
    public int overloadReamainTurn {  get; private set; }
    public bool IsOverloadActive => overloadReamainTurn > 0;

    public void ActiveOverload(int duration)
    {
        if(IsOverloadActive)
        {
            return;
        }

        overloadReamainTurn = duration;
        Debug.Log($"과부하 상태 부여 {duration}턴");
    }
    public void ConsumeOverload()
    {
        if(!IsOverloadActive)
        {  return; }

        overloadReamainTurn--;

        if (overloadReamainTurn <= 0)
        {
            Debug.Log("과부하 종료");
        }
        
    }
}
