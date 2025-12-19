using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEditor.PlayerSettings;

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

    [SerializeField] public GameObject[] positionInit = new GameObject[3];


    //BattleUnit.MovePosition(UnitPosition newPosition)

    //모델 정보 받아서 뷰에 전달해야 함.
    private void Start()
    {

    }

    public void DeathCharacter()
    {
        //팀 리스트
        PlayerTeam = BattleManager.Instance.PlayerTeam;
        Debug.Log($"{PlayerTeam[0].name}");

        // 데이터가 없으면 리턴
        if (PlayerTeam == null)
        {
            Debug.Log("[CharacterPosPresenter] : 팀없음");
            return;
        }

        for (int i = 0; i < PlayerTeam.Count; i++)
        {
            if (PlayerTeam[i].IsDead == true)
            {
                Debug.Log("[CharacterPosPresenter] : 사망자 발생");
                BattleManager.Instance.OnUnitDead(PlayerTeam[i]);
                _view.IsEnpty(PlayerTeam[i]);

                //뷰에서는 캐릭터 정보도 비활성화 해야함
                _view.Inactive();
            }
        }

        AliveCharecter();
    }

    public void AliveCharecter()
    {
        for (int i = 0; i < PlayerTeam.Count; i++)
        {
            if (PlayerTeam[i].IsDead == false)
            {
                //위치 갱신
                _view.ReSetPosition(PlayerTeam[i], positionInit);
                Debug.Log("[CharacterPosPresenter] : 생존캐릭터 위치 세팅");
            }
        }
    }
}


