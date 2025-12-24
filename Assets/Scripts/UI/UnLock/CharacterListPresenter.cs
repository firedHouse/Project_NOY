using UnityEngine;
using System.Collections.Generic;

// 캐릭터 목록에서 캐릭터 정보 출력에 전체적으로 사용할 프레젠터
public class CharacterListPresenter : MonoBehaviour
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
        if(CharacterDataManager.Instance == null)
        {
            Debug.Log("데이터 매니저 없음");
        }
        else
        {
            Debug.Log("데이터 매니저 있음");
            CharacterDataManager.Instance.SetCharacterDataList();
            characters = CharacterDataManager.Instance.CharacterListModels;
        }

        //characterSlots = GetComponentsInChildren<CharacterSlot>;
        GetCharacterList();
    }

    // 캐릭터 리스트 가져오기
    // 캐릭터 수에 따라 캐릭터 슬롯 생성 후 각 슬롯에 캐릭터 정보 띄우기
    private void GetCharacterList()
    {
        //foreach(CharacterListModel character in characters)
        for (int i = 0; i < characters.Count; i++)
        {
            //CharacterSlot.Instantiate(characterSlot, GameObject.Find("LobbyCanvas").transform);
            //Debug.Log($"[CharacterListPresenter] {characters[i].CharacterName} 불러오기 성공");
            characterSlots[i].UpdateCharacterSlot(characters[i].CharacterID);
        }
    }

    // 캐릭터 해금
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
