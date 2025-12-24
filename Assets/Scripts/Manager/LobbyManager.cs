using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.TextCore.Text;

// 캐릭터 데이터 목록 가져오는 매니저
public class LobbyManager : Singleton<LobbyManager>
{
    #region Field 
    // 모든 캐릭터 데이터 가져오기 
    [SerializeField] private string[] characterDataIDs = { "10001", "10002", "10003", "10004", "10005", "10006", "10007", "10008", "10009" };
    
    [Header("배틀씬에 전달되는 팀 플레이어 id")]
    [SerializeField] private string[] selectedCharacterIDs = new string[3];

    [Header("[리스트 내부 요소 연결]" +
        "\nElement 갯수는 캐릭터수만큼, " +
        "\n씬 내부에 있는 CharacterSelectButton을 순서대로 넣어주세요")]
    [SerializeField] public List<CharacterListModel> CharacterListModels;
    #endregion


    #region Property 
    public string[] SelectedCharacterIDs => selectedCharacterIDs;

    #endregion

    public void SetCharacterDataList()
    {
        for(int i = 0; i < characterDataIDs.Length; i++)
        //foreach(string id in characterDataIDs)
        {
            if (string.IsNullOrEmpty(characterDataIDs[i]))
            {
                continue;
            }
            ConvertToDataModel(characterDataIDs[i], CharacterListModels[i]);

            if (CharacterListModels[i] == null)
            {
                continue;
            }
            //Debug.Log($"[CharacterDataManager] {CharacterListModels[i].CharacterName} 로드");
            //CharacterDatas.Add(data);
        }
        Debug.Log($"[CharacterDataManager] 캐릭터 데이터 목록 생성완료");
    }

    // 캐릭터 데이터를 데이터 리스트 모델로 변경
    private void ConvertToDataModel(string id, CharacterListModel listModel)
    {
        listModel.Initialize(id);
    }

    /// <summary>
    /// (inProgress) 선택된 캐릭터의 id를 리스트로 반환하여 배틀씬에 전달
    /// </summary>
    public void SetTeam()
    {
        // something
        // selectedCharacterIDs에 차례로 추가
    }
}
