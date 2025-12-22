using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEditor.PlayerSettings;

public class CharacterPosPresenter : MonoBehaviour
{
    //프레젠터 : 현재스크립트-CharacterPosPresenter
    //모델 : BattleManager
    //뷰 : CharacterPositionView

    [SerializeField] private CharacterPositionView _view;

    private List<Character> PlayerTeam = new List<Character>();

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
                //사망 캐릭터 체크 후 비활성화 번호 전달
                Debug.Log($"[CharacterPosPresenter] : 사망자 발생{i}");
                //배틀매니저에서 유닛 비활성화
                BattleManager.Instance.OnUnitDead(PlayerTeam[i]);

                _view.Inactive(i);
            }
        }
    }

}


