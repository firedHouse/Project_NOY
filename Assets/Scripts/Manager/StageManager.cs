using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StageManager : MonoBehaviour
{
    //싱글톤
    public static StageManager Instance;

    [Header("UI 표시용 현재 지점")]
    public int CurrentStage = 1; // 1 ~ 3 
    public int CurrentRound = 1; // 1 ~ 5 

    [Header("배틀매니저")]
    public BattleManager battleManager; //기존 BattleManager 참조

    //이번 게임에서 진행할 스테이지 ID 순서를 저장할 리스트
    private List<string> mapOrder = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InitializeStageOrder();
        StartBattle();
    }

    //맵랜덤로직
    private void InitializeStageOrder()
    {
        mapOrder.Clear();

        //일반 스테이지 풀(나중에 스테이지 늘어나면 리팩토링)
        List<string> normalStages = new List<string> { "90001", "90002" };

        //무작위 섞기
        normalStages = normalStages.OrderBy(x => Random.value).ToList();

        //순서 확정
        mapOrder.AddRange(normalStages);
        //마지막은 90003
        mapOrder.Add("90003");
    }

    //전투 시작 로직
    public void StartBattle()
    {
        Debug.Log($"스테이지 {CurrentStage}-{CurrentRound} 시작");
        
        //리스트 비어있으면 재 초기화
        if (mapOrder.Count == 0)
        {
            InitializeStageOrder();
        }

        List<MonsterData> selectedMonsters = SelectMonstersForStage(CurrentStage, CurrentRound);

        if (selectedMonsters == null || selectedMonsters.Count == 0)
        {
            Debug.LogError("[STageManager]몬스터 데이터 로드 실패");
            return;
        }
        //5라운드는 보스전 플래그 true
        bool isBoss = (CurrentRound == 5);

        //12.25 여기에 몬스터 스펙증가(스테이지마다 5%) 추가
        float stageBuffMultiplier = 1.0f;
        if (CurrentStage > 1)
        {
            stageBuffMultiplier = 1.0f + ((CurrentStage - 1) * 0.05f);
        }


        //12.25 몬스터 리스트, 보스여부, + 강화수치 전달)
        //BattleManager에게 전투 세팅 요청
        battleManager.SetupBattle(selectedMonsters, isBoss, stageBuffMultiplier);
    }

    //12.24 TestBattleStarter 로직 이관, 몬스터 뽑기
    private List<MonsterData> SelectMonstersForStage(int uiStageNum, int roundNum)
    {
        //반환할 결과
        List<MonsterData> result = new List<MonsterData>();

        //12.25 추가
        //UI 변수를 인덱스로 바꾸고 조회
        int mapIndex = uiStageNum - 1;

        //인덱스 범위 체크
        if (mapIndex < 0 || mapIndex >= mapOrder.Count)
        {
            Debug.LogError($"맵 순서 범위를 벗어남");
            return result;
        }

        //리스트에서 ID꺼내기
        string searchKey = mapOrder[mapIndex]; 

        //데이터 받아오기
        var stageData = TableManager.Instance.StageTable.Get(searchKey);
        if (stageData == null)
        {
            return result;
        }

        //보스전
        if (roundNum == 5)
        {
            var boss = TableManager.Instance.MonsterTable.Get(stageData.bossID);
            if (boss != null) result.Add(boss);
            return result;
        }

        //일반전, 랜뽑
        string groupID = stageData.groupID;

        int spawnedCount = 0;
        int maxAttempts = 100; // 무한루프 방지
        int attempts = 0;


        //12.25 탱커여부, 딜러카운트
        bool hasTanker = false;
        int dealerCount = 0;

        //3마리 뽑을 때까지 반복
        while (spawnedCount < 3 && attempts < maxAttempts)
        {
            attempts++;
            string drawnID = GetRandomMonsterID(groupID);
            if (string.IsNullOrEmpty(drawnID))
            {
                continue;
            }

            MonsterData mData = TableManager.Instance.MonsterTable.Get(drawnID);
            if (mData == null)
            {
                continue;
            }
            //스폰여부체크
            bool isSpawnable = false;

            //탱커 체크
            if ((MonsterClass)mData.monsterClass == MonsterClass.Tanker)
            {
                if (!hasTanker)
                {
                    hasTanker=true;
                    isSpawnable = true;
                }
            }
            else
            {
                if (dealerCount < 2)
                {
                    dealerCount++;
                    isSpawnable = true;
                }
            }
            if (isSpawnable)
            {
                result.Add(mData);
                spawnedCount++;
            }
        }
        return result;
    }

    //12.24 TestBattleStarter 로직 이관, 그룹에서 가중치 기준 랜덤 몬스터 뽑기
    private string GetRandomMonsterID(string groupID)
    {
        MonsterGroupData groupData = TableManager.Instance.MonsterGroupTable.Get(groupID);
        if (groupData == null) return null;

        var candidates = groupData.GetSpawnList(); //확장 메서드 활용
        if (candidates.Count == 0) return null;

        float totalRate = candidates.Sum(x => x.rate);
        float randomPoint = Random.Range(0, totalRate);

        float currentRate = 0;
        foreach (var item in candidates)
        {
            currentRate += item.rate;
            if (randomPoint <= currentRate) return item.id;
        }
        return candidates.Last().id;
    }

    //BattleManager에서 전투 승리 시 호출
    public void OnBattleClear()
    {
        Debug.Log($"스테이지 {CurrentStage}-{CurrentRound} 클리어");

        //여기에 보상 페이즈 출력 연결
        //보상 처리가 끝났다고 가정, 다음 단계 진행
        OnRewardProcessCompleted();
    }

    //보상/영입 이벤트 처리가 끝난 후 호출
    public void OnRewardProcessCompleted()
    {
        //라운드 증가
        CurrentRound++;

        //스테이지 갱신 체크 (5라운드 초과 시 다음 스테이지로) 
        if (CurrentRound > 5)
        {
            CurrentRound = 1;
            CurrentStage++;
        }

        //엔딩 체크 (3-5 클리어 후) 
        if (CurrentStage > 3)
        {
            GameClear();
            return;
        }

        //다음 전투 시작
        StartBattle();
    }

    //엔딩 크레딧 또는 로비 이동
    private void GameClear()
    {
        Debug.Log("모든 스테이지 클리어!");
        //UI 패널 연결
    }
}