using System.Collections.Generic;
using UnityEngine;

public class CharacterPosPresenter : MonoBehaviour
{
    //프레젠터 나중에 하나로 합쳐야 함.

    public Queue<CharacterData> aliveList = new Queue<CharacterData>();
    private void SurvivalList(string characterID, bool isDead)
    {
        // 데이터가 없으면 리턴
        //if (infoModel.Character == null)
        //{
        //    return;
        //}

        //if (isDead == true)
        //{
        //    aliveList.Enqueue(infoModel.Character);
        //    Debug.Log($"[CharacterBattleInfoPresenter] : 생존리스트에 {infoModel.Character} 추가");
        //}

        //if (aliveList.Count == 0)
        //{
        //    Debug.Log("[CharacterBattleInfoPresenter] : 생존 캐릭터 없음");
        //}
        ////생존리스트 
        //else if (aliveList.Count > 0)
        //{
        //    characterPosition.ReSetPosition(aliveList);
        //}
    }
}
