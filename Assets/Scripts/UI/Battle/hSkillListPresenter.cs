using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillListPresenter : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private hSkillList skillList;       // 스킬 슬롯들이 있는 UI
    [SerializeField] private MouseOverInfo mouseOverInfo;

    //데이터
    private List<Character> playerTeam; // 아군 3명
    private List<Monster> enemyTeam; // (임시) 타겟용 적군 리스트

    //현재 누구의 스킬을 고르는 중인지 추적용 변수
    private int currentActorIndex = 0;

    private void Awake()
    {
        //턴 시작 시 리스트 초기화 및 선택 시작 이벤트 연결
        BattleManager.Instance.OnPlayerTurnStart += HandlePlayerTurnStart;
    }

    //턴이 시작되면 호출 (BattleManager가 호출)
    private void HandlePlayerTurnStart(List<Character> characters)
    {
        playerTeam = characters;
        enemyTeam = BattleManager.Instance.EnemyTeam; // 적군 정보도 가져와야 함

        //인덱스 초기화 (0번 타자부터 시작)
        currentActorIndex = 0;

        //첫 번째 타자의 UI 보여주기
        ShowSkillUIForCurrentActor();
    }

    //현재 순서인 캐릭터의 스킬을 UI에 전달
    private void ShowSkillUIForCurrentActor()
    {
        // 3명이 다 골랐으면 종료
        if (currentActorIndex >= playerTeam.Count)
        {
            EndSkillSelection();
            return;
        }

        Character currentCharacter = playerTeam[currentActorIndex];

        //UI 텍스트 갱신 (누구 턴인지 표시)
        mouseOverInfo.UpdateTurnInfo(currentActorIndex);
        skillList.SetSkillSlotAvailable(true);
        mouseOverInfo.PPInfo(currentCharacter);

        //skillList UI에 현재 캐릭터 정보를 넘겨서, 버튼 아이콘 등을 갱신
        //기존 로직 활용
        skillList.BattleUnit = currentCharacter;
        skillList.SkillDataLoad(OnSkillButtonClicked);

        Debug.Log($"{currentCharacter.name}의 스킬을 선택해주세요.");
    }

    //UI 버튼이 클릭되었을 때 실행되는 함수 (이걸 버튼에 연결)
    //SkillSlot 스크립트에서 버튼 클릭 시, 이 함수를 호출하며 자신의 Skill 정보를 넘겨줘야 함
    public void OnSkillButtonClicked(Skill selectedSkill)
    {
        if (selectedSkill != null && selectedSkill.CurrentPP <= 0)
        {
            Debug.Log("PP 부족, 클릭 무시");
            return;
        }

        //한번 클릭하면 버튼 비활성화
        skillList.SetSkillSlotAvailable(false);

        //현재 행동하는 캐릭터
        Character actingCharacter = playerTeam[currentActorIndex];

        //타겟 결정 로직
        //어차피 행동 직전에 다시 타겟 지정함
        BattleUnit target = BattleManager.Instance.EnemyTeam[0];

        if(selectedSkill == null)
        {
            Debug.Log("null임");
        }
        else
        {
            Debug.Log($"{actingCharacter.name} -> {selectedSkill} 사용 예약");
        }

        //배틀매니저에게 행동 전달
        BattleManager.Instance.ReceivePlayerAction(actingCharacter, selectedSkill, target);
        int skillNum = actingCharacter.Skills.IndexOf(selectedSkill);
        actingCharacter.UseSkill(skillNum, target);

        //다음 타자로 넘어가기
        currentActorIndex++;

        //다음 타자 UI 갱신?
        ShowSkillUIForCurrentActor();
    }

    //모든 선택이 끝났을 때 턴 넘기라고 지시
    private void EndSkillSelection()
    {
        Debug.Log("아군 3명의 스킬 선택이 모두 완료되었습니다.");
        mouseOverInfo.UpdateTurnChanged();
        BattleManager.Instance.OnPlayerInputFinished();
    }
}