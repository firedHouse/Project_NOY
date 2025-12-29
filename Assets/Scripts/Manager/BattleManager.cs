using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//상태를 관리하고 필요한 데이터(유닛 리스트) 를 제공하는 매니저
//TestBattleStarter에서 받아옴 
//12.24 상속 제거 -> MonoBehaviour로 변경 (씬 전환 시 파괴되도록)

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    private void Awake()
    {
        //싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //이미 있으면 중복 생성된 것이므로 파괴
            if (Instance != this)
            {
                Destroy(gameObject);
            }
            return;
        }
        if (EnemyTeam == null)
        {
            EnemyTeam = new List<Monster>();
        }
        if (PlayerTeam == null)
        {
            PlayerTeam = new List<Character>();
        }
    }
    //현재 실행중인 상태
    private IBattleState currentState;

    //데이터 저장소 (State 접근용 Public) 
    public List<Character> PlayerTeam { get; private set; } = new List<Character>();
    public List<Monster> EnemyTeam { get; private set; } = new List<Monster>();

    //순서가 정렬된? 최종 실행 큐 
    public Queue<BattleAction> ActionQueue { get; private set; } = new Queue<BattleAction>();

    //각 턴의 행동을 모아두는 리스트 
    public List<BattleAction> TempPlayerActions { get; private set; } = new List<BattleAction>();
    public List<BattleAction> TempEnemyActions { get; private set; } = new List<BattleAction>();

    //12.22
    //위치 정보 저장용 변수 추가(TestBattleStarter 에서 가져옴)
    public List<Transform> PlayerSpawnPoints;
    public List<Transform> EnemySpawnPoints;

    //12.24
    //TestBattleStarter에서 이관된 캐릭터 뼈대
    [Header("Prefabs")]
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject characterPrefab;

    //12.26 보상, 결과 패널도 연결 필요
    [Header("보상, 결과 패널 출력용")]
    public RewardUI RewardPanelPrefab;
    public GameObject ResultPanelPrefab;


    //프레젠터 이벤트
    public event Action<List<Character>> OnPlayerTurnStart;
    // Jihoo
    // 프레젠터 초기화를 위한 이벤트(아군/적군 공용)
    public event Action OnBattleSetted;

    //상태 변경 메서드
    public void ChangeState(IBattleState newState)
    {
        if (currentState != null)
        {
            currentState.Exit(this);
        }

        currentState = newState;

        //상태 전환 추적
        Debug.Log($"[BattleMnager] {currentState.GetType().Name} 진입");
        currentState.Enter(this);
    }

    private void Update()
    {
        //매 프레임 bool 체크만 진행(리팩토링 필요한가?)
        if (currentState != null)
        {
            currentState.Execute(this);
        }
    }

    //전투 시작 진입점(호출용)
    public void StartBattle(List<Character> players, List<Monster> enemies)
    {
        PlayerTeam = players;
        EnemyTeam = enemies;

        //12.22 시프트 기능 구현을 위한 사망 이벤트 연결
        foreach (var player in PlayerTeam)
        {
            //중복 연결 방지를 위해 뺐다가 다시 연결
            player.OnDeath -= OnUnitDead;
            player.OnDeath += OnUnitDead;
        }

        foreach (var enemy in EnemyTeam)
        {
            enemy.OnDeath -= OnUnitDead;
            enemy.OnDeath += OnUnitDead;
        }

        //Setup 상태 진입
        ChangeState(new StateSetup());

        // Jihoo
        // 각 프레젠터 초기화하도록 이벤트로 알림 (아군/적군 공용)
        Debug.Log("[BattleManager] 프레젠터 초기화 이벤트"); // 지우기
        BattleSetted();
    }

    //SkillArea에 따른 타겟 리스트 반환
    public List<BattleUnit> GetTargetsBySkill(List<BattleUnit> targetTeam, SkillData skillData)
    {
        //매핑할 타겟 리스트 생성
        List<BattleUnit> validTargets = new List<BattleUnit>();

        //SkillData의 int형 skillArea를 Enum으로 변환
        SkillArea area = (SkillArea)skillData.skillArea;

        //현재 살아있는(리스트에 존재하는) 유닛의 수
        int count = targetTeam.Count;
        if (count == 0) return validTargets;

        //enum 값에 따른 인덱스 매핑
        switch (area)
        {
            case SkillArea.Front: //전열
                if (count > 0) validTargets.Add(targetTeam[0]);
                break;

            case SkillArea.FrontMid: //전중열
                if (count > 0) validTargets.Add(targetTeam[0]);
                if (count > 1) validTargets.Add(targetTeam[1]);
                break;

            case SkillArea.Mid: //중열
                if (count > 1) validTargets.Add(targetTeam[1]);
                break;

            case SkillArea.MidBack: //중후열
                if (count > 1) validTargets.Add(targetTeam[1]);
                if (count > 2) validTargets.Add(targetTeam[2]);
                break;

            case SkillArea.Back: //후열
                if (count > 2) validTargets.Add(targetTeam[2]);
                break;

            case SkillArea.FrontBack: //전후열
                if (count > 0) validTargets.Add(targetTeam[0]);
                if (count > 2) validTargets.Add(targetTeam[2]);
                break;

            case SkillArea.All: //전체
                validTargets.AddRange(targetTeam);
                break;
        }
        //매핑된 리스트 반환
        return validTargets;
    }

    //유닛 사망 시 당겨짐 처리
    //리스트에서 제거하면 끝
    public void OnUnitDead(BattleUnit deadUnit)
    {
        bool isPlayer = deadUnit is Character;

        if (isPlayer)
        {
            if (PlayerTeam.Contains((Character)deadUnit))
            {
                PlayerTeam.Remove((Character)deadUnit);
                Debug.Log($"아군 {deadUnit.UnitName} 사망");
                //당기기
                UpdateTeamPositions(PlayerTeam, PlayerSpawnPoints);
            }
        }
        else
        {
            if (EnemyTeam.Contains((Monster)deadUnit))
            {
                EnemyTeam.Remove((Monster)deadUnit);
                Debug.Log($"몬스터 {deadUnit.UnitName} 사망");
                //당기기
                UpdateTeamPositions(EnemyTeam, EnemySpawnPoints);
            }
        }
    }

    //리스트 순서대로 전열 중열 후열 재부여
    private void UpdateTeamPositions<T>(List<T> team, List<Transform> spawnPoints) where T : BattleUnit
    {
        for (int i = 0; i < team.Count; i++)
        {
            UnitPosition newPos = UnitPosition.Front;
            if (i == 1)
            {
                newPos = UnitPosition.Mid;
            }
            else if (i == 2)
            {
                newPos = UnitPosition.Back;
            }
            //실제 유닛 내부 변수 변경
            team[i].MovePosition(newPos, spawnPoints[i].position);
        }
    }


    //상대팀 지정(layer처리안하려고 이렇게)
    public List<BattleUnit> GetOpponentTeam(BattleUnit user)
    {
        //Monster라면 상대 = PlayerTeam
        if (user is Monster)
        {
            return PlayerTeam.Cast<BattleUnit>().ToList();
        }
        //Character라면 상대는 EnemyTeam
        return EnemyTeam.Cast<BattleUnit>().ToList();
    }

    //UI연동용, Presenter가 호출할 메서드
    public void ReceivePlayerAction(BattleUnit user, Skill skill, BattleUnit target)
    {
        //커맨드 객체 생성 및 저장
        BattleAction newAction = new BattleAction(user, skill, target);
        TempPlayerActions.Add(newAction);

        Debug.Log($"[UI 연동 체크] {user.UnitName}의 커맨드 입력 완료");
    }

    public void OnPlayerInputFinished()
    {
        Debug.Log("[BattleManager] 모든 아군 입력 완료 신호 수신. 턴을 진행합니다.");

        // 현재 상태가 '플레이어 턴'인지 확인하고 턴을 넘김
        if (currentState is StatePlayerTurn playerTurnState)
        {
            Debug.Log("호출");
            playerTurnState.SetInputComplete();
        }
        else
        {
            Debug.LogWarning("[Error] 현재 상태가 StatePlayerTurn이 아닙니다.");
        }
    }

    public void NotifyPlayerTurnStart()
    {
        OnPlayerTurnStart?.Invoke(PlayerTeam);
    }


    // Jihoo
    // 각 프레젠터 초기화하도록 이벤트로 알림 (아군/적군 공용)
    public void BattleSetted()
    {
        Debug.Log("[BattleManger] 이벤트 실행");

        OnBattleSetted?.Invoke();
    }

    public void SetupBattle(List<MonsterData> monsters, bool isBossRound, float multiplier = 1.0f)
    {
        //아군 소환(PlayerSpawnPoints 사용) 
        SpawnPlayerTeam();

        //적군 소환(여기로 로직 이동) 
        SpawnEnemyTeam(monsters, isBossRound, multiplier);

        StartBattle(PlayerTeam, EnemyTeam);
    }
    //적 배치 및 소환
    private void SpawnEnemyTeam(List<MonsterData> monsters, bool isBoss, float multiplier)
    {
        EnemyTeam.Clear();
        Monster[] slots = new Monster[3]; // 0:전열, 1:중열, 2:후열

        foreach (var data in monsters)
        {
            int targetIndex = -1;

            //역할군에 따른 자리 배치 로직 (TestBattleStarter에서 가져옴)
            if ((MonsterClass)data.monsterClass == MonsterClass.Tanker)
            {
                if (slots[0] == null)
                {
                    targetIndex = 0; // 전열
                }
            }
            else //딜러, 힐러
            {
                if (slots[1] == null)
                {
                    targetIndex = 1; // 중열
                }
                else if (slots[2] == null)
                {
                    targetIndex = 2; // 후열
                }
            }

            //실제 소환
            if (targetIndex != -1)
            {
                GameObject go = Instantiate(monsterPrefab, EnemySpawnPoints[targetIndex].position, Quaternion.identity);
                Monster monster = go.GetComponent<Monster>();

                //몬스터 초기화
                monster.InitializeMonster(data.monsterID, (UnitPosition)targetIndex, isBoss);

                //12.25 유닛 능력치 강화(소환 시에 강화, 데이터 원본 유지)
                if (multiplier > 1.0f)
                {
                    monster.ApplyBuffMultiplier(multiplier);

                }
                slots[targetIndex] = monster;
                EnemyTeam.Add(monster);
            }
        }
        // UI나 내부 데이터 갱신
        UpdateTeamPositions(EnemyTeam, EnemySpawnPoints);
    }
    public void SpawnPlayerTeam()
    {
        Debug.Log("[BattleManager] 아군 소환 시작");

        //플레이어 팀 리스트 초기화
        PlayerTeam.Clear();
        //string[] teamData = TempLobbyManager.Instance.GetSelectedCharacterIDs();
        string[] teamData = LobbyManager.Instance.SelectedCharacterIDs;

        //뭐시깽이 매니저.Instance.메서드 혹은 변수명, 데이터 형식 필요함

        if (teamData == null)
        {
            Debug.LogError("팀 정보를 불러오지 못했습니다");
            return;
        }

        //슬롯(0,1,2) 순회하며 소환
        for (int i = 0; i < 3; i++)
        {
            string charID = teamData[i];

            //ID가 없으면 빈 자리이므로 패스
            if (string.IsNullOrEmpty(charID))
            {
                continue;
            }

            //테이블에서 캐릭터 데이터 로드
            CharacterData cData = TableManager.Instance.CharacterTable.Get(charID);
            if (cData == null)
            {
                Debug.LogError($"캐릭터 데이터를 찾을 수 없음: {charID}");
                continue;
            }


            if (i < PlayerSpawnPoints.Count)
            {
                GameObject go = Instantiate(characterPrefab, PlayerSpawnPoints[i].position, Quaternion.identity);
                Character character = go.GetComponent<Character>();

                //매니저 연결 및 데이터 주입
                character.InitializeCharacter(charID, (UnitPosition)i);

                //관리 리스트에 추가
                PlayerTeam.Add(character);

                Debug.Log($"[Spawn] {cData.characterName} 소환 완료");
            }
        }
        UpdateTeamPositions(PlayerTeam, PlayerSpawnPoints);
    }
    public bool HasDeadPlayer()
    {
        return PlayerTeam.Count < 3;
    }
}