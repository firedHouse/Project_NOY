using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//상태를 관리하고 필요한 데이터(유닛 리스트) 를 제공하는 매니저
//TestBattleStarter에서 받아옴 

public class BattleManager : Singleton<BattleManager>
{
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

}