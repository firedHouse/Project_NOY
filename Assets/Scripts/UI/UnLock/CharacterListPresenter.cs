using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
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

    [Header("테스트용 임시 실링")]
    public int Shilling;

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
            // if (EconomyManager.Instance != null)
            // {
            //     EconomyManager.Instance.AddShilling(1000);
            //     Shilling = EconomyManager.Instance.ResultShilling();
            // }
            // else
            // {
            //     Debug.Log($"이코노미 매니저 없음");
            // }
            unlockButton = (GameObject.Find("CharacterUnlockButton")).GetComponent<Button>();
        }

        LoadCharacterList();
        //characterSlots = GetComponentsInChildren<CharacterSlot>;

        // 초기 설정으로 왼쪽 패널만 보여주기
        growthView.SetDetailView(false);
        // 초기 설정으로 첫 슬롯 캐릭터 지정해주기
        // ShowDetailView(characterSlots[0].gameObject.GetComponent<CharacterListModel>());

        // Hyeju
        // Init(characterSlots[0].gameObject.GetComponent<CharacterListModel>());
        // model.OnUnlock += CanClick;
        // model.OnUnlock += growthView.CharacterInfo;
        // model.OnUpgrade += UpdateCharacterInfo;
    }

    // 테스트용) 인게임 > 아웃게임 실링 받아오기를 여기서 처리
    private void SetShilling()
    {
        
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
            growthView.ChangeUnlockUIActivation(characters[i].IsUnlocked);
        }
        // 잠금 해제 시 UI 변경 메서드 
        SetButtonEvent(model, OnUnlockButtonClicked);

    }

    private void ShowDetailView(CharacterListModel character)
    {
        if (character != null)
        {
            model = character;
            UpdateCharacterInfo(model);
            UpdateMainInfo(model);
            // 잠금 ui 변경
            Debug.Log($"[CharacterListPresenter] {model.CharacterName} 해금 여부 : {model.IsUnlocked}");
            growthView.ChangeUnlockUIActivation(model.IsUnlocked);
            // 성장 버튼 활성화
            growthView.ButtonActive(model.IsUnlocked);
        }
        else
        {
            Debug.Log("[CharacterListPresenter] 슬롯에서 캐릭터를 받아올 수 없습니다");
        }
    }

    // 클릭한 캐릭터 슬롯에 해당하는 상세 정보 띄워주기
    // 클릭한 슬롯을 model에 넣어줌
    // 각 캐릭터 슬롯에서 호출
    // 잠금된 캐릭터는 캐릭터 상세 정보가 출력되지 않고 화면만? > 아예 비활성화만하기?
    public void OnSlotClicked(CharacterListModel character)
    {
        Debug.Log("[CharacterListPresenter] 슬롯 클릭");
        ShowDetailView(character);
        // growthView.SetDetailView(true);
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
            // 보유 실링으로 해금 가능한지 확인 
            // 실링이 부족하면 팝업 띄움
            // 팝업 닫으면 안눌렸던 상태처럼 돌아감
            // 실링이 부족하면 해금 활성화 안하는 게 
            if(character.NeedShilling > Shilling)
            {
                growthView.Popup.ShowPopup();
                // 닫는 거 외에 다른 조건 설정이 필요한가?
                if(growthView.Popup.gameObject.activeSelf == false)
                {
                    return;
                }
            }

            model.Unlock();
            Debug.Log($"[CharacterListPresenter] {model.CharacterName} 해금 여부 : {model.IsUnlocked}");
            growthView.ChangeUnlockUIActivation(model.IsUnlocked);
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
