using System;
using UnityEngine;

//캐릭터 선택 시, ResetEconomy() 호출 필요
public class EconomyManager : Singleton<EconomyManager>
{   //인게임 화폐 관리 매니저
    private int runGold = 0;
    private int runShilling = 0;

    public int RunGold => runGold; // 읽기 전용 현재 보유 골드양
    public int RunShilling => runShilling; // 현재 게임에서 얻은 실링 양

    public event Action<int> OnGoldChanged; // 골드 변경 시 이벤트

    protected override void Awake()
    {
        base.Awake();
        ResetEconomy();
    }
    //스테이지 시작 시 재화 초기화
    public void ResetEconomy()
    {
        runGold = 0;
        runShilling = 0;
        OnGoldChanged?.Invoke(runGold);
    }

    //재화 획득 매서드
    public void AddGold(int plusGold)
    {
        if (plusGold <= 0)
        { return; }

        runGold += plusGold;
        OnGoldChanged?.Invoke(runGold);
        Debug.Log($"골드 획득: +{plusGold}. 현재 골드: {runGold}");
    }
    public void AddShilling(int plusShilling)
    {
        if (plusShilling <= 0)
        { return; }
        runShilling += plusShilling;
        Debug.Log($"실링 획득: +{plusShilling}. 현재 실링: {runShilling}");
    }

    public bool CanSpendGold(int cost)
    {
        return runGold >= cost;
    }
    public void SpendGold(int cost)
    {
        runGold -= cost;
        OnGoldChanged?.Invoke(runGold);
        Debug.Log($"골드 사용 : -{cost}. 현재 골드 : {runGold}");
    }

    //스테이지 종료 시 실링을 아웃게임으로 전송하기 전 UI에 표시 및 인게임에서 얻은 Shilling을 받기
    public int ResultShilling()
    {
        int result = runShilling;
        runShilling = 0;
        Debug.Log($"스테이지 종료: 획득한 실링 {result} 반환");
        return result;
    }
   
}
