using UnityEngine;
using System.Collections.Generic;

//개발용 테스터
//ID 입력하여 소환
public class TestBattleStarter : MonoBehaviour
{
    [Header("테스트 모드 활성화 여부")]
    public bool isTestMode = false;

    [Tooltip("테스트할 몬스터 ID)")]
    //여기서는 특정 몬스터 ID를 직접 넣어서 테스트
    public string[] testMonsterIDs = { "20001", "20002", "20003" };

    [Tooltip("테스트할 아군 캐릭터 ID 3개")]
    public string[] testPlayerIDs = { "10001", "10004", "10007" };

    private void Start()
    {
        //테스트모드일때만 작동
        if (!isTestMode)
        {
            return;
        }
        //매니저 초기화 대기 (혹시 모를 순서 문제 방지)
        //안전하게 Start에서 호출
        SetupAndRunTest();
    }

    private void SetupAndRunTest()
    {
        Debug.Log("[TestBattleStarter] 테스트 모드 전투 시작");

        //아군 데이터 세팅 (임시 로비 매니저에 주입)
        if (TempLobbyManager.Instance != null)
        {
            TempLobbyManager.Instance.SetTestCharacterIDs(testPlayerIDs);
        }

        //적군 데이터 세팅,ID값 -> MonsterData 변환
        List<MonsterData> testMonsters = new List<MonsterData>();

        foreach (string id in testMonsterIDs)
        {
            if (string.IsNullOrEmpty(id))
            {
                continue;
            }

            MonsterData mData = TableManager.Instance.MonsterTable.Get(id);
            if (mData != null)
            {
                testMonsters.Add(mData);
            }
            else
            {
                Debug.LogWarning($"[Test] 몬스터 ID {id}를 테이블에서 찾을 수 없습니다.");
            }
        }

        //BattleManager 호출
        if (BattleManager.Instance != null)
        {
            //보스전 테스트 여부는 필요하면 bool 변수 변경
            BattleManager.Instance.SetupBattle(testMonsters, false);
        }
        else
        {
            Debug.LogError("BattleManager가 씬에 없음");
        }
    }
}