using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEditor.PlayerSettings;

partial class CharacterBattleInfoPresenter : MonoBehaviour
{
    [SerializeField] private CharacterPositionView _view;

    //체력 닳았을때 체크
    private void Start()
    {
        characterModel.OnDeath += DeathCharacter;
    }

    public void DeathCharacter(BattleUnit unit)
    {
        //사망 캐릭터 체크 후 비활성화 번호 전달
        Debug.Log($"[CharacterPosPresenter] : 사망자 발생{unit}");
        //사망 캐릭터 리스트에서 제거
        BattleManager.Instance.OnUnitDead(unit);
        //포지션 업데이트
        characterView.UpdatePosition(characterModel.Position);

        //UI 비활성화
        _view.Inactive(unit);
        //캐릭터 위치 재설정

    }
}


