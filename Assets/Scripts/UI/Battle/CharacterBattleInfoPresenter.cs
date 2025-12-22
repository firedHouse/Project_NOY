using System;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterBattleInfoPresenter : MonoBehaviour
{
    [Header("모델, 런타임 중 자동 추가됨.")]
    [SerializeField] private Character characterModel;
    [Header("캐릭터 CharacterBox Panel, 전중후열에 맞게 각각 추가")]
    [SerializeField] private CharacterBattleInfoView characterView;
    [Header("CharacterBoxGroupPanel 추가")]
    [SerializeField] private CharacterPositionView characterMoveView;
    //private CharacterData characterData;
    [Header("전중후열 값")]
    [SerializeField] private UnitPosition position;

    private void Awake()
    {
        //Debug.Log("[CharacterBattleInfoPresenter] Awake");
        BattleManager.Instance.OnBattleSetted += Initialize;
        BattleManager.Instance.OnBattleSetted += DeathCharacter;
    }

    //뷰 초기 설정
    public void Initialize()
    {
        Debug.Log("[CharacterBattleInfoPresenter] 초기화");
        characterModel = BattleManager.Instance.PlayerTeam[(int)position];
        characterModel.OnDeath += HandleDeath;
        //characterModel.OnDeath += HandlePositionChanged;
        characterModel.OnHpChanged += HandleHpChanged;
        //characterModel.OnMarkChanged += HandleMarkChanged;


        characterView.SetMaxHP(characterModel.MaxHP);
        characterView.SetSkillList(characterModel.Skills);
        characterView.UpdateCharacterName(characterModel.UnitName);
        characterView.UpdateSpeed(characterModel.Speed);
        //characterView.UpdatePosition(characterModel.CharacterClass);

        //고유속성
        //elementUI로 변경해야 함
        characterView.UpdateClass(characterModel.CurrentMark.ToString());

        //표식
        //characterView.UpdateElementMark(characterModel.CurrentMark.ToString());
    }

    private void HandleHpChanged(BattleUnit character, float hpChangedAmount)
    {
        characterView.UpdateHPBar(hpChangedAmount);
    }

    // 속성 표시 변경 데이터인데 속성이 데이터 테이블에 없어서 지금은 사용 안함
    //private void HandleMarkChanged(BattleUnit character, ElementType elementType)
    //{
    //}

    private void HandleDeath(BattleUnit unit)
    {
        BattleManager.Instance.OnUnitDead(unit);
    }

    private void HandlePositionChanged(BattleUnit unit)
    {
        //characterView.UpdatePosition(position);
    }

}

