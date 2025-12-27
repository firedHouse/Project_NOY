using System;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터 데이터 목록 가져오는 매니저
public class LobbyManager : Singleton<LobbyManager>
{
    #region Field 
    /// <summary>
    /// 데이터 테이블의 모든 캐릭터 데이터를 가져와 저장하는 리스트
    /// </summary>
    [SerializeField] private List<CharacterData> characterDatas;
    
    /// <summary>
    /// 배틀씬에 전달되는 구성된 팀 플레이어 id 목록
    /// </summary>
    [Header("배틀씬에 전달되는 팀 플레이어 id")]
    [SerializeField] private string[] selectedCharacterIDs = new string[3];
    
    [SerializeField] public List<CharacterListModel> CharacterListModels;

    [SerializeField] private GameObject characterPrefab;
    #endregion


    #region Property 
    public string[] SelectedCharacterIDs => selectedCharacterIDs;

    #endregion

    private void Start()
    {
        SetCharacterDataList();
        PlayerPrefs.SetInt("OutGameShilling", 5000);
        Debug.Log($"[ShillingPresenter] 해금 테스트시 실링 소비를 위해 실링 값 조정");
    }

    public void SetCharacterDataList()
    {
        // 모든 데이터 가져와서 Convert에 넣어주는 식으로 개선
        characterDatas = TableManager.Instance.CharacterTable.GetAll();

        // characterData를 CharacterListModels로 변환하여 저장
        for(int i = 0; i < characterDatas.Count; i++)
        {
            CharacterListModels[i] = cvtToDM(characterDatas[i]);

            if (CharacterListModels[i] == null)
            {
                continue;
            }
            Debug.Log($"[LobbyManager] {CharacterListModels[i].CharacterName} 로드");

        }
        Debug.Log($"[LobbyManager] 캐릭터 데이터 목록 생성완료");
    }

    // 캐릭터 데이터를 가져와 직접 캐릭터 리스트 모델 데이터로 변환
    private CharacterListModel cvtToDM(CharacterData data)
    {
        // 씬에 객체로 생성하여 팀 구성과 캐릭터 리스트 팝업에서도 사용할 수 있도록 함
        CharacterListModel model = Instantiate(characterPrefab).gameObject.GetComponent<CharacterListModel>();
        // CharacterListModel model = gameObject.AddComponent<CharacterListModel>();
            
        // CharacterListModels 각 필드에 characterData 저장
        model.Initialize(data.characterID, data);
        return model;
    }
    
    /// <summary>
    /// (inProgress) 선택된 캐릭터의 id를 리스트로 반환하여 배틀씬에 전달
    /// </summary>
    public void SetTeam(string[] inTeamMembers)
    {
        // selectedCharacterIDs에 차례로 추가
        for (int i = 0; i < inTeamMembers.Length; i++)
        {
            selectedCharacterIDs[i] = inTeamMembers[i];
            Debug.Log($"[LobbyManager] {i} {selectedCharacterIDs[i]}");
        }
    }
    
    
}
