using UnityEngine;
using System.Collections.Generic;
using System.Linq;


//확률 기반 소환과 역할군별 생성 위치 규칙을 준수하고 구현한 스크립트

//1 데이터 로드 및 준비
//2 적군 소환 로직. 뽑기 -> 역할군 검사 및 배치 -> 생성
//3 아군 소환 로직. Test용 testPlayerIDs배열 순회해서 배치(캐릭터 선택창 없음) ==> 단 전열 캐릭터 두명을 입력하면 불가하도록
//4 명단 확정 -> List 변환(링큐) -> BattleManager 호출
public class TestBattleStarter : MonoBehaviour
{
    [Header("Test Settings")]
    [Tooltip("테스트할 스테이지 ID")]
    [SerializeField] private string targetStageID = "90001";

    //테스트용 아군 ID 목록 (테스트하고 싶은 캐릭터 ID 입력)
    [Tooltip("테스트할 아군 캐릭터 ID 3개")]
    [SerializeField] private string[] testPlayerIDs = { "10001", "10002", "10003" };

    [Header("Spawn Points")]
    [Tooltip("아군 소환 위치 (0:전열, 1:중열, 2:후열)")]
    [SerializeField] private Transform[] playerSpawnPoints;

    [Tooltip("적군 소환 위치 (0:전열, 1:중열, 2:후열)")]
    [SerializeField] private Transform[] enemySpawnPoints;

    //캐릭터 뼈대
    [Header("Prefabs")]
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject characterPrefab;

    private void Start()
    {
        SetupAndStartBattle();
    }

    public void SetupAndStartBattle()
    {
        //데이터 로드
        StageData stageData = TableManager.Instance.StageTable.Get(targetStageID);
        if (stageData == null)
        {
            Debug.LogError($"[Starter] 스테이지 데이터를 찾을 수 없음: {targetStageID}");
            return;
        }

        //몬스터 소환
        //슬롯관리
        Monster[] enemySlots = new Monster[3];

        int spawnedCount = 0;
        int maxAttempts = 100; // 무한루프 방지용 안전장치
        int currentAttempts = 0;

        //12.22 스테이지 연결된 몬스터 그룹 아이디 가져오도록 리팩토링
        string targetGroupID = stageData.groupID;

        //총 3마리 뽑기
        while (spawnedCount<3 && currentAttempts<maxAttempts)
        {
            currentAttempts++;
            //확률 기반 몬스터 뽑기
            //12.22 그룹아이디 기반 확률 뽑기 진행
            string drawnMonsterID = GetRandomMonsterID(targetGroupID);
            MonsterData mData = TableManager.Instance.MonsterTable.Get(drawnMonsterID);

            if (mData == null)
            {
                continue;
            }

            int targetSlotIndex = -1;
            if ((MonsterClass)mData.monsterClass == MonsterClass.Tanker) //탱커 체크
            {
                if (enemySlots[0] == null)
                {
                    targetSlotIndex = 0;
                }
                else
                {
                    Debug.Log($"[Spawn] ({mData.monsterName})가 뽑혔으나 전열이 이미 차있어 무시");
                }
            }
            else //딜러 힐러 처분
            {
                if (enemySlots[1] == null)
                {
                    targetSlotIndex = 1;
                }
                else if (enemySlots[2] == null)
                {
                    targetSlotIndex=2;
                }
                else
                {
                    Debug.Log($"[Spawn] 딜러/힐러({mData.monsterName})가 뽑혔으나 중/후열이 모두 차있어 무시");
                }
            }
            if (targetSlotIndex != -1)
            {
                GameObject go = Instantiate(monsterPrefab, enemySpawnPoints[targetSlotIndex].position, Quaternion.identity);
                Monster monster = go.GetComponent<Monster>();

                //몬스터 초기화(ID, 위치, 보스여부)
                //12.22 변경(스테이지 데이터의 보스ID와 일치할 경우 true)
                bool isBoss = (drawnMonsterID == stageData.bossID);
                monster.InitializeMonster(drawnMonsterID, (UnitPosition)targetSlotIndex, isBoss);

                // 슬롯 점유
                enemySlots[targetSlotIndex] = monster;
                Debug.Log($"[Enemy Spawn] {targetSlotIndex}열({mData.monsterClass}): {mData.monsterName} 소환 완료");
                spawnedCount++; //카운트 증가
            }
        }
        //아군 소환(Position 컬럼 규칙 적용)
        Character[] playerSlots = new Character[3];

        foreach (string charID in testPlayerIDs)
        {
            if (string.IsNullOrEmpty(charID))
            {
                continue;
            }
            CharacterData cData = TableManager.Instance.CharacterTable.Get(charID);
            if (cData == null)
            {
                continue;
            }

            //데이터의 포지션 값 가져오기 (0, 1, 2)
            int posIndex = (int)cData.position;

            //해당 자리가 비어있을 때만 배치
            if (posIndex >= 0 && posIndex < 3 && playerSlots[posIndex] == null)
            {
                GameObject go = Instantiate(characterPrefab, playerSpawnPoints[posIndex].position, Quaternion.identity);
                Character character = go.GetComponent<Character>();

                character.InitializeCharacter(charID, (UnitPosition)posIndex);
                playerSlots[posIndex] = character;

                Debug.Log($"[Player Spawn] {posIndex}열: {cData.characterName} 소환 완료");
            }
            else
            {
                Debug.LogWarning($"[Player Spawn] {cData.characterName}의 자리({cData.position})가 유효하지 않음");
            }
        }
        //BattleManager에 명단 전달, null은 제외
        List<Monster> finalEnemies = enemySlots.Where(m => m != null).ToList();
        List<Character> finalPlayers = playerSlots.Where(m => m != null).ToList();

        if (finalEnemies.Count > 0 && finalPlayers.Count > 0)
        {
            BattleManager.Instance.StartBattle(finalPlayers, finalEnemies);
        }
        else
        {
            Debug.LogError("전투 시작할 유닛 부족");
        }

    }

    //확률 가중치 뽑기 로직
    //12.22 변경=>  몬스터 그룹 테이블의 가중치를 기준으로 구현
    private string GetRandomMonsterID(string groupID)
    {

        //TableBase의 dataMap.Values를 통해 전체 데이터를 순회하며 해당 groupID를 가진 항목만 필터링
        //변수명은 그룹후보들? 정도로
        var groupCandidates = TableManager.Instance.MonsterGroupTable.dataMap.Values
                    .Where(data => data.groupID == groupID)
                    .ToList();

        //3개 몬스터의 가중치 합 30 30 45  == > 105 30/105
        if (groupCandidates.Count == 0)
        {
            Debug.LogError($"스타터 => 몬스터그룹테이블에서 그룹아이디 '{groupID}'를 찾지 못함");
            return null;
        }

        //가중치 합 계산
        float totalRate = groupCandidates.Sum(data => data.spawnRate);
        float randomPoint = Random.Range(0, totalRate);

        //뽑기
        float currentRate = 0;
        foreach (var data in groupCandidates)
        {
            currentRate += data.spawnRate;
            if (randomPoint <= currentRate)
            {
                return data.monsterID;
            }
        }
        return null;

    }
}
