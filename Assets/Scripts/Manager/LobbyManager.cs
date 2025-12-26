using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

// 캐릭터 데이터 목록 가져오는 매니저
public class LobbyManager : Singleton<LobbyManager>
{
    #region Field 
    // 모든 캐릭터 데이터 가져오기 
    // [SerializeField] private string[] characterDataIDs = { "10001", "10002", "10003", "10004", "10005", "10006", "10007", "10008", "10009" };
    [SerializeField] private List<CharacterData> characterDatas;
    
    [Header("배틀씬에 전달되는 팀 플레이어 id")]
    [SerializeField] private string[] selectedCharacterIDs = new string[3];

    // [Header("[리스트 내부 요소 연결]" +
    //     "\nElement 갯수는 캐릭터수만큼, " +
    //     "\n씬 내부에 있는 CharacterSelectButton을 순서대로 넣어주세요")]
    [SerializeField] public List<CharacterListModel> CharacterListModels;

    [SerializeField] private GameObject characterPrefab;
    #endregion


    #region Property 
    public string[] SelectedCharacterIDs => selectedCharacterIDs;

    #endregion

    private void Start()
    {
        // characterSlots = characterList.GetComponentsInChildren<CharacterSlot>().ToList();
    }
    
    public void SetCharacterDataList()
    {
        // 모든 데이터 가져와서 Convert에 넣어주는 식으로 개선
        characterDatas = TableManager.Instance.CharacterTable.GetAll();

        for(int i = 0; i < characterDatas.Count; i++)
        {

            
            // 모델 리스트에 직접 넣을 수 없으니까 테이블 매니저에서 불러와서 
            CharacterListModels[i] = cvtToDM(characterDatas[i]);

            if (CharacterListModels[i] == null)
            {
                continue;
            }
            Debug.Log($"[LobbyManager] {CharacterListModels[i].CharacterName} 로드");

        }
        Debug.Log($"[LobbyManager] 캐릭터 데이터 목록 생성완료");
    }

    // 데이터를 가져와 직접 모델 데이터로 변환
    private CharacterListModel cvtToDM(CharacterData data)
    {
        CharacterListModel model = Instantiate(characterPrefab).gameObject.GetComponent<CharacterListModel>();
        // CharacterListModel model = gameObject.AddComponent<CharacterListModel>();
        
        model.Initialize(data.characterID, data);
        return model;
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
