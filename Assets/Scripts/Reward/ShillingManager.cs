using UnityEngine;

public class ShillingManager : Singleton<ShillingManager>
{   //아웃 게임 실링 관리 매니저
    private int outGameShilling = 0;

    public int OutGameShilling => outGameShilling; // 현재 보유 실링 양

    protected override void Awake()
    {
        base.Awake();
        LoadShilling();
    }
    //실링 저장 및 불러오기 메서드
    private void LoadShilling()
    {
        outGameShilling = PlayerPrefs.GetInt("OutGameShilling", 0);
        Debug.Log($"실링 불러오기: 현재 실링 {outGameShilling}");
    }
    private void SaveShilling()
    {
        PlayerPrefs.SetInt("OutGameShilling", outGameShilling);
        PlayerPrefs.Save();
        Debug.Log($"실링 저장하기: 현재 실링 {outGameShilling}");
    }

    //아웃 게임으로 실링 옳기는 매서드
    public void AddOutGameShilling(int plusShilling)
    {
        if (plusShilling <= 0)
        { return; }
        outGameShilling += plusShilling;
        SaveShilling();
        Debug.Log($"실링 획득: +{plusShilling}. 현재 실링: {outGameShilling}");
    }
    //실링 소모 메서드
    public void SpendShilling(int cost)
    {
        if (outGameShilling < cost)
        {
            Debug.Log("보유한 실링이 부족합니다!");
            return;
        }
        outGameShilling -= cost;
        SaveShilling();
        Debug.Log($"실링 사용: -{cost}. 현재 실링: {outGameShilling}");
    }

    //성장 시스템용 성공 여부 반환하는 결제 함수
    public bool TrySpendShilling(int cost)
    {
        if (outGameShilling < cost)
        {
            Debug.Log($"실링 부족으로 실패, 보유: {outGameShilling}, 필요: {cost})");
            return false;
        }

        //실링 되면 차감
        outGameShilling -= cost;
        SaveShilling();
        Debug.Log($"실링 사용 성공: -{cost}. 남은 실링: {outGameShilling}");
        return true;
    }

    //12.31 여현구. 뉴게임용
    public void ResetShilling()
    {
        outGameShilling = 0;
        SaveShilling(); 
        Debug.Log("실링 초기화 완료: 0");
    }

}
