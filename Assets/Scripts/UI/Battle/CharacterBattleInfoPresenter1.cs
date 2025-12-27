using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEditor.PlayerSettings;

partial class CharacterBattleInfoPresenter
{
    [SerializeField] private BattleUnit[] _character = new BattleUnit[3];
    [SerializeField] private GameObject[] _basePos = new GameObject[3];
    private Dictionary<BattleUnit, int> characterBoxPos= new Dictionary<BattleUnit, int>();

    //public Dictionary<GameObject, Character> CharacterBoxPos => characterBoxPos;
    public GameObject[] BasePos => _basePos;
    public void DeathCharacter()
    {
        //n번 위치에 있던 캐릭터
        for (int i = 0; i < 3; i++)
        {
            _character[i] = BattleManager.Instance.PlayerTeam[i];

            characterBoxPos.Add(_character[i], i);
            Debug.Log($"[CharacterPosPresenter] : 저장개수 {characterBoxPos.Keys.Count}");
        }
    }

    public void PosReset(BattleUnit unit)
    {
        Debug.Log($"[CharacterPosPresenter] : 사망자 발생{unit}");

       characterMoveView.Inactive(characterBoxPos[unit]);
        //UI 비활성화
        //캐릭터 위치 재설정
    }
}


