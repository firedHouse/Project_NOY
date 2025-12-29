using System;
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
        if (isReactedThisTurn)
        {
            Debug.Log("이미 원소 반응을 일으킨 상태, 원소 부여 불가");
            return;
        }

        gameObject.layer = ElementLayerUtil.ElementToLayer(element);

        Debug.Log($"{element} {gameObject.GetComponent<BattleUnit>().UnitName}에게 레이어 {gameObject.layer} 부여");
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

    //12.23 턴 종료시에 호출하고 과부하턴 줄이기
    public void DecreaseOverloadTurn()
    {
        if (overloadReamainTurn > 0)
        {
            overloadReamainTurn--;
            if (overloadReamainTurn <= 0)
            {
                Debug.Log("과부하 끝!");
                overloadReamainTurn = 0;
            }
        }
        ResetTurn();
    }
}
