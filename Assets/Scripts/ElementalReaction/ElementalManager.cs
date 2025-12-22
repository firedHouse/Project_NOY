using UnityEngine;

public class ElementalManager : MonoBehaviour
{
    // 현재 상태

    public ElementType currnentMark { get; private set; } = ElementType.None;

    public bool IsReactedThisTurn { get; private set; } = false;

    public int overloadRemainTurn { get; private set; } = 0;

    public bool IsOverloadActive => overloadRemainTurn > 0;

    //원소 표식

    public void SetMark(ElementType element)
    {
        currnentMark = element;
    }

    public void ClearMark()
    {
        currnentMark = ElementType.None;
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
