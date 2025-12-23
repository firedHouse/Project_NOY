using UnityEngine;
using UnityEngine.Rendering;

public class EconomyManager : Singleton<EconomyManager>
{   //인게임 재화인 골드 관리 매니저
    private int currentGold = 0;

    public int CurrentGold => currentGold; // 읽기 전용 현재 보유 골드양

    protected override void Awake()
    {
        base.Awake();
        // 추가 초기화 코드가 필요하면 여기에 작성
    }
    //골드 획득 매서드
    public void AddGold(int plusGold)
    {
        if (plusGold <= 0)
        { return; }

        currentGold += plusGold;
        Debug.Log($"골드 획득: +{plusGold}. 현재 골드: {currentGold}");
    }
    //골드 사용 가능 여부 확인
    public bool CanSpendGold(int cost)
    {
        return currentGold >= cost || cost >= 0;
    }
    public bool SpendGold(int cost)
    {
        if(!CanSpendGold(cost))
        {
            Debug.Log("보유한 골드가 부족합니다!");
            return false;
        }
        
        currentGold -= cost;
        Debug.Log($"골드 사용: -{cost}. 현재 골드: {currentGold}");
        return true;
    }
   
}
