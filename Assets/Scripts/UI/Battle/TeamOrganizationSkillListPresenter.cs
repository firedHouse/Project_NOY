using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// 클릭한 모델의 스킬을 불러와 스킬칸에 띄워줌
// 스킬에 마우스 오버 하면 스킬의 설명을 띄워줌
// pp 계산이나 다른 것들은 필요 없고 단순히 출력만 하면 된다
// 필요한 것 : 모델 불러오기, 스킬 로드하기 O > 모델 베이스, 스킬 출력, 마우스오버, 마우스 오버에 따라 스킬 다르게 출력해주기

public class TeamOrganizationSkillListPresenter : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TeamOrganizationSkillList skillList;       // 스킬 슬롯들이 있는 UI
    [SerializeField] private TeamOrganizationSkillList mouseOverInfo;

    //데이터
    private List<Character> playerTeam; // 아군 3명
    // private List<Monster> enemyTeam; // (임시) 타겟용 적군 리스트

    //현재 누구의 스킬을 고르는 중인지 추적용 변수
    // private int currentActorIndex = 0;

    private void Awake()
    {
        //턴 시작 시 리스트 초기화 및 선택 시작 이벤트 연결
        BattleManager.Instance.OnPlayerTurnStart += HandlePlayerTurnStart;
    }
    

    //턴이 시작되면 호출 (BattleManager가 호출)
    private void HandlePlayerTurnStart(List<Character> characters)
    {
        playerTeam = characters;
        // enemyTeam = BattleManager.Instance.EnemyTeam; // 적군 정보도 가져와야 함

        //인덱스 초기화 (0번 타자부터 시작)
        // currentActorIndex = 0;

        //첫 번째 타자의 UI 보여주기
        ShowSkillUI();
    }

    private void ShowSkillUI()
    {
        //UI 텍스트 갱신 (누구 턴인지 표시)
        // mouseOverInfo.UpdateTurnInfo(currentActorIndex);
        skillList.SetSkillSlotAvailable(true);
        
        //skillList UI에 현재 캐릭터 정보를 넘겨서, 버튼 아이콘 등을 갱신
        //기존 로직 활용
        // skillList.Model = currentCharacter;
        // skillList.SkillDataLoad(OnSkillButtonClicked);
    }
}