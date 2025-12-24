using System;
using UnityEngine;

public class GoldModel : MonoBehaviour
{
    //몬스터 객체 사망 시 획득
    //몬스터 데이터 테이블의  골드 만큼 획득
    //유지되는 동안 UI에 실시간 갱신(몬스터 처치, 아이템 구매 등 골드 값 변동 시 갱신)
    //골드 유지 종료: 게임 사이클 종료(게임오버/엔딩)
    //주요 기능1. 몬스터 골드 드랍
    //주요 기능2. 골드 차감 시 갱신

    [Header("골드 초기 값")]
    [SerializeField] private float startGold = 1000;
    [SerializeField] private float gold;

    //프로퍼티
    public float CurrentGold { get { return startGold; } set { startGold = value; } }
    public float Gold { get { return gold; } set { gold = value; } }

    public event Action GoldChanged;

    public void SpendGold(int amount)
    {
        if(Gold >= amount)
        {

        }
    }
}
