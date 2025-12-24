using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;

// 캐릭터 목록에서 캐릭터 정보 출력에 전체적으로 사용할 프레젠터
// 혜주님이 작성하신 CharacterListPresenter는 GrowthPresenter 클래스에 존재
public partial class CharacterListPresenter : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private CharacterListView characterListView;
    
    [Header("[리스트 내부 요소 연결]" +
    "\nElement 갯수는 캐릭터수만큼, " +
    "\n씬 내부에 있는 CharacterSelectButton을 순서대로 넣어주세요")]
    [SerializeField] private List<CharacterSlot> characterSlots;
    
    private List<CharacterListModel> characters;
    private Button unlockButton;

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
            unlockButton = (GameObject.Find("CharacterUnlockButton")).GetComponent<Button>();
        }

        LoadCharacterList();
        //characterSlots = GetComponentsInChildren<CharacterSlot>;

        // 초기 설정으로 첫 슬롯 캐릭터 지정해주기
        ShowDetailView(characterSlots[0].gameObject.GetComponent<CharacterListModel>());
        // Hyeju
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
            // 슬롯 클릭 이벤트 설정
            characterSlots[i].SetButtonEvent(characters[i], OnSlotClicked);
            // 슬롯 클릭시 잠금 UI 변경 메서드
            growthView.ChangeLockedUIActivation(characters[i].IsUnlocked);
        }
        // 잠금 해제 시 UI 변경 메서드 
        SetButtonEvent(model, OnUnlockButtonClicked);

    }

    private void ShowDetailView(CharacterListModel character)
    {
        if (character != null)
        {
            model = character;
            //UpdateCharacterInfo(model);
            UpdateMainInfo(model);
            // 잠금 ui 변경
            Debug.Log($"[CharacterListPresenter] {model.CharacterName} 해금 여부 : {model.IsUnlocked}");
            growthView.ChangeLockedUIActivation(model.IsUnlocked);
        }
        else
        {
            Debug.Log("[CharacterListPresenter] 슬롯에서 캐릭터를 받아올 수 없습니다");
        }
    }

    // 클릭한 캐릭터 슬롯에 해당하는 상세 정보 띄워주기
    // 클릭한 슬롯을 model에 넣어줌
    // 각 캐릭터 슬롯에서 호출
    public void OnSlotClicked(CharacterListModel character)
    {
        Debug.Log("[CharacterListPresenter] 슬롯 클릭");
        ShowDetailView(character);
    }

    // 해금 버튼 클릭시 캐릭터 상태 변경
    // 파라미터 변경 필요?
    public void OnUnlockButtonClicked(CharacterListModel character)
    {
        Debug.Log("[CharacterListPresenter] 해금 클릭");
        Debug.Log($"[CharacterListPresenter] {character}");
        if(character != null)
        {
            model = character;
            model.Unlock();
            Debug.Log($"[CharacterListPresenter] {model.CharacterName} 해금 여부 : {model.IsUnlocked}");
            growthView.ChangeLockedUIActivation(model.IsUnlocked);
        }
        else
        {
            Debug.Log($"[CharacterListPresenter] 해금 상태 변경 실패");
        }
    }

    /// <summary>
    /// 이름, 해금 여부?, 코드네임, 속성, 대사, 인포, 업데이트
    /// </summary>
    public void UpdateMainInfo(CharacterListModel character)
    {
        Debug.Log($"[CharacterListPresenter] 기본 정보 업데이트");
        growthView.CharacterName(character);
        growthView.CharacterInfo(character);
        //growthView.CharacterElement(character);
    }

    // 클릭하면 해금되도록 버튼 이벤트 설정
    public void SetButtonEvent(CharacterListModel character, UnityAction<CharacterListModel> onClickCallBack)
    {
        model = character;
        Debug.Log("해금 버튼 설정");
        unlockButton.onClick.AddListener(() => onClickCallBack(model));
    }
}
