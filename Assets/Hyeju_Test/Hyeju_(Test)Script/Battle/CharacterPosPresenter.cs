using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPosPresenter : MonoBehaviour
{
    //프레젠터 나중에 하나로 합쳐야 함.
    //프레젠터 : 현재스크립트-CharacterPosPresenter

    //모델 : BattleUnit
    //[SerializeField] private BattleUnit _battleUnit;
    //[SerializeField] private TestBattleStarter _model;
    //[SerializeField] private BattleManager _attleUnit;

    //뷰 : CharacterPositionView
    [SerializeField] private CharacterPositionView _view;

    private List<Character> PlayerTeam = new List<Character>();

    private UnitPosition position;

    //BattleUnit.MovePosition(UnitPosition newPosition)

    //모델 정보 받아서 뷰에 전달해야 함.
    private void Start()
    {
        //팀 리스트
        PlayerTeam = BattleManager.Instance.PlayerTeam;
    }

    public void SurvivalList()
    {
        // 데이터가 없으면 리턴
        if (PlayerTeam == null)
        {
            return;
        }

        for(int i = 0; i < 3; i++)
        {
            if (PlayerTeam[i].IsDead == true)
            {
                BattleManager.Instance.OnUnitDead(PlayerTeam[i]);
            }

        }
    }

}

