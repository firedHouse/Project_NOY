using System;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterBattleInfoPresenter : MonoBehaviour
{
    [SerializeField] private Character characterModel;
    [SerializeField] private CharacterBattleInfoView characterView;
    [SerializeField] private CharacterPositionView characterMoveView;
    //private CharacterData characterData;
    [SerializeField] private CharacterPosition position;

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
        characterView.UpdatePosition(characterModel.CharacterClass);
        characterView.UpdateElement(characterModel.CurrentMark.ToString());
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

    //private void HandlePositionChanged(BattleUnit unit)
    //{
    //    characterView.UpdatePosition(position);
    //}

}

