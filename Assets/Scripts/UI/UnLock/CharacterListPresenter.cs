using UnityEngine;
using System.Collections.Generic;

// 캐릭터 목록에서 캐릭터 정보 출력에 전체적으로 사용할 프레젠터
public partial class CharacterListPresenter : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private CharacterListView characterListView;
    
    [Header("[리스트 내부 요소 연결]" +
    "\nElement 갯수는 캐릭터수만큼, " +
    "\n씬 내부에 있는 CharacterSelectButton을 순서대로 넣어주세요")]
    [SerializeField] private List<CharacterSlot> characterSlots;
    
    private List<CharacterListModel> characters;

    private void Start()
    {
        if(LobbyManager.Instance == null)
        {
            Debug.Log("데이터 매니저 없음");
        }
        else
        {
            LobbyManager.Instance.SetCharacterDataList();
            characters = LobbyManager.Instance.CharacterListModels;
        }

        LoadCharacterList();
        //characterSlots = GetComponentsInChildren<CharacterSlot>;

        Init();
        model.OnUnlock += CanClick;
        model.OnUpgrade += UpdateCharacterInfo;
    }

    // 캐릭터 리스트 가져오기
    // 캐릭터 수에 따라 캐릭터 슬롯 생성 후 각 슬롯에 캐릭터 정보 띄우기
    private void LoadCharacterList()
    {
        for (int i = 0; i < characters.Count; i++)
        {

            //Debug.Log($"[CharacterListPresenter] {characters[i].CharacterName} 불러오기 성공");
            characterSlots[i].UpdateCharacterSlot(characters[i].CharacterID);
        }
    }

    // 캐릭터 해금
    // to-do : 현재 상세 정보 ui에서 표시하고 있는 캐릭터 데이터의 unlock을 변경
    private void UnlockCharacter()
    {
        //characterListModel.Unlock();
        // 뷰에서 해금에 따라 UI 갱신
        // 해금 버튼 비활성화, 해금 아이콘 비활성화
        // 성장 버튼 선택 가능하도록 변경
        //characterListView.
    }

    // 클릭한 캐릭터 슬롯에 해당하는 상세 정보 띄워주기
}
