using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEditor.PlayerSettings;

partial class CharacterBattleInfoPresenter
{
    //public Dictionary<GameObject, Character> CharacterBoxPos => characterBoxPos;
    
    public void DeathCharacter()
    {
        currentCount = Mathf.Min(3, BattleManager.Instance.PlayerTeam.Count);

        //n번 위치에 있던 캐릭터
        for (int i = 0; i < currentCount; i++)
        {
            _character[i] = BattleManager.Instance.PlayerTeam[i];

            characterBoxPos[_character[i]] = i;
            
            Debug.Log($"[CharacterPosPresenter] : 저장개수 {characterBoxPos.Keys.Count}");
        }
    }

    public void PosReset(BattleUnit unit)
    {
        Debug.Log($"[CharacterPosPresenter] : 사망자 발생{unit.Position}");

       characterMoveView.Inactive(characterBoxPos[unit]);
        //UI 비활성화
        //캐릭터 위치 재설정
    }
}


