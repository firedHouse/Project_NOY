using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// 캐릭터 목록에서 캐릭터 정보 출력에 전체적으로 사용할 프레젠터
// 혜주님이 작성하신 CharacterListPresenter는 GrowthPresenter 클래스에 존재
public partial class CharacterListPresenter : CharacterPresenterBase
{
    [SerializeField] private ShillingPresenter shillingPresenter; 
    private Button unlockButton;

    protected override void Start()
    {
        growthView = gameObject.GetComponent<GrowthView>();
        SetSlotUI();
        unlockButton = growthView.UnlockButton.GetComponent<Button>();
        Debug.Log("슬롯 설정 완료");
        // if (EconomyManager.Instance != null)
        // {
        //     EconomyManager.Instance.AddShilling(1000);
        //     Shilling = EconomyManager.Instance.ResultShilling();
        // }
        // else
        // {
        //     Debug.Log($"이코노미 매니저 없음");
        // }

        LoadCharacterList();
        Debug.Log("캐릭터 로딩");
        
        // 초기 설정으로 왼쪽 패널만 보여주기
        growthView.SetDetailView(false);
    }

    // 테스트용) 인게임 > 아웃게임 실링 받아오기를 여기서 처리
    // private void SetShilling()
    // {
    //     
    // }
    
    // 캐릭터 리스트 가져오기
    // 캐릭터 수에 따라 캐릭터 슬롯 생성 후 각 슬롯에 캐릭터 정보 띄우기
    protected override void LoadCharacterList()
    {
        for (int i = 0; i < characters.Count; i++)
        {

            // Debug.Log($"[CharacterListPresenter] {characters[i].CharacterName} 불러오기 성공");
            characterSlots[i].UpdateCharacterSlot(characters[i]);
            // 슬롯 클릭 이벤트 설정
            characterSlots[i].SetButtonEvent(characters[i], OnSlotClicked);
            // 슬롯 클릭시 잠금 UI 변경 메서드
            growthView.ChangeUnlockUIActivation(characters[i].IsUnlocked);
        }
        // 잠금 해제 시 UI 변경 메서드 
        SetButtonEvent(model, OnUnlockButtonClicked);

    }

    protected override void ShowDetailView(CharacterListModel character)
    {
        if (character != null)
        {
            model = character;
            UpdateCharacterInfo(character);
            UpdateMainInfo(model);
            // 잠금 ui 변경
            Debug.Log($"[CharacterListPresenter] {model.CharacterName} 해금 여부 : {model.IsUnlocked}");
            growthView.ChangeUnlockUIActivation(model.IsUnlocked);
            // 성장 버튼 활성화
            growthView.ButtonActive(model.IsUnlocked);
            Debug.Log($"[CharacterListPresenter] 해금시 성장 버튼 변경 설정");
            GrowthButton(model.IsUnlocked);
            // CanClick(model);
            // model.OnUnlock += CanClick;
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
    public override void OnSlotClicked(CharacterListModel character)
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
        if(character != null)
        {
            Debug.Log($"[CharacterListPresenter] {model.CharacterName} 해금 여부 : {model.IsUnlocked}");
            model = character;
            // 보유 실링으로 해금 가능한지 확인 
            // 현재 보유중인 실링이 부족하면 팝업 띄우고 리턴
            if(character.UnlockShilling > ShillingManager.Instance.OutGameShilling)
            {
                
                Debug.Log($"[CharacterListPresenter] 실링 부족해서 해금 안됨!! {model.IsUnlocked}");
                growthView.Popup.ShowPopup();
                return;
            }
 
            model.Unlock();
            shillingPresenter.UpdateUI();
            growthView.ChangeUnlockUIActivation(model.IsUnlocked);
            GrowthButton(model.IsUnlocked);
        }
        else
        {
            Debug.Log($"[CharacterListPresenter] 캐릭터가 존재하지 않아 해금 상태 변경 실패");
        }
    }

    /// <summary>
    /// 이름, 해금 여부?, 코드네임, 속성, 대사, 인포, 업데이트
    /// </summary>
    public override void UpdateMainInfo(CharacterListModel character)
    {
        Debug.Log($"[CharacterListPresenter] 기본 정보 업데이트");
        growthView.CharacterName(character);
        growthView.CharacterInfo(character);
        Init(character);
        //growthView.CharacterElement(character);
    }

    // 클릭하면 해금되도록 버튼 이벤트 설정
    public override void SetButtonEvent(CharacterListModel character, UnityAction<CharacterListModel> onClickCallBack)
    {
        model = character;
        Debug.Log("해금 버튼 설정");
        unlockButton.onClick.AddListener(() => onClickCallBack(model));
    }
}
