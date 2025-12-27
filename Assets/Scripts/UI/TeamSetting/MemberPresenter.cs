using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// 팀 구성 페이지에서 사용할 프레젠터
public class MemberPresenter : CharacterPresenterBase
{
    [SerializeField] protected GrowthSkillView skillView;
    [SerializeField] private TeamSelectView selectView;
    
    // private CharacterListModel[] ids = new CharacterListModel[3];
    // 테스트용 팀 멤버 id 배열
    // private string[] ids = new string[] {"10002", "10003", "10008"};
    private string[] ids = new string[3];
    
    protected override void Start()
    {
        skillView = gameObject.GetComponent<GrowthSkillView>();
        selectView = GameObject.Find("SeletedTeamPanel").GetComponent<TeamSelectView>();
        SetSlotUI();
        LoadCharacterList();
        skillView.SetDetailView(false);
        selectView.SetAllButtonInteractable(false);
        foreach (var slot in selectView.Slots)
        {
            Debug.Log($"슬롯 설정");
            SetButtonEvent(slot, SetTeamPosition);   
        }
    }

    // 캐릭터 리스트 가져오기
    // 캐릭터 수에 따라 캐릭터 슬롯 생성 후 각 슬롯에 캐릭터 정보 띄우기
    protected override void LoadCharacterList()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            //Debug.Log($"[CharacterListPresenter] {characters[i].CharacterName} 불러오기 성공");
            characterSlots[i].UpdateCharacterSlot(characters[i]);
            // 해당 슬롯이 해금되지 않았다면 선택 불가능하게
            if (!characterSlots[i].SlotModel.IsUnlocked)
            {
                characterSlots[i].gameObject.GetComponent<Button>().interactable = false;
            }
            // 슬롯 클릭 이벤트 설정
            characterSlots[i].SetButtonEvent(characters[i], OnSlotClicked);
        }
    }

    // 클릭한 캐릭터 슬롯에 해당하는 상세 정보 띄워주기
    // 클릭한 슬롯을 model에 넣어줌
    // 각 캐릭터 슬롯에서 호출
    // 잠금된 캐릭터는 캐릭터 상세 정보가 출력되지 않고 슬롯 이미지만 보임
    // 슬롯 클릭하면 중앙에 팀 선택 버튼들이 클릭 가능하도록 변경
    public override void OnSlotClicked(CharacterListModel character)
    {
        Debug.Log("[CharacterListPresenter] 슬롯 클릭");
        ShowDetailView(character);
        skillView.SetDetailView(true);
        selectView.SetAllButtonInteractable(true);

    }

    protected override void ShowDetailView(CharacterListModel character)
    {
        if (character != null)
        {
            model = character;
            UpdateCharacterInfo(character);
            UpdateMainInfo(model);
        }
        else
        {
            Debug.Log("[CharacterListPresenter] 슬롯에서 캐릭터를 받아올 수 없습니다");
        }
    }

    /// <summary>
    /// 이름, 해금 여부?, 코드네임, 속성, 대사, 인포, 업데이트
    /// </summary>
    public override void UpdateMainInfo(CharacterListModel character)
    {
        Debug.Log($"[CharacterListPresenter] 기본 정보 업데이트");
        skillView.CharacterName(character);
        //growthView.CharacterElement(character);
    }

    // 전/중/후열 중에 하나를 클릭하면 해당 캐릭터 슬롯을 받아감
    // 현재 모델을 클릭된 열에 넣어준다
    public void SetTeamPosition(CharacterSlot selectedSlot)
    {
        Debug.Log($"[MemberPresenter] {selectedSlot.transform.parent.name} {model.CharacterName}");
        selectedSlot.SlotModel = model;
        selectedSlot.SlotCharacter.text = model.CharacterName;

    }
    
    // 클릭하면 버튼의 슬롯 모델에 현재 프레젠터의 모델을 넣음 
    public void SetButtonEvent(CharacterSlot slot, UnityAction<CharacterSlot> onClickCallBack)
    {
        Debug.Log($"[MemberPresenter] 모델 선택 버튼");
        slot.SlotButton.onClick.AddListener(() => onClickCallBack(slot));
    }

    public override void UpdateCharacterInfo(CharacterListModel character)
    {
        skillView.CharacterIllust(model);
    }

    // NotImplementedException 나중에 삭제
    public override void Init(CharacterListModel character)
    {
        throw new NotImplementedException();
    }
    
    // 출전 버튼을 누르면 > 다른 메서드에서 구현
    // 로비 매니저에게 아이디 리스트 전달
    // !!!!! 팀 멤버가 다 안차면 출전을 누를 수 없도록 해야 함
    public void SaveTeamList()
    {
        // 슬롯에 있는 애들의 id를 가져와 배열에 저장
        for (int i = 0; i < selectView.Slots.Length; i++)
        {
            ids[i] = selectView.Slots[i].SlotModel.CharacterID;
        }
        Debug.Log($"[MemberPresenter] {ids[0]} - {ids[1]} - {ids[2]}");
        LobbyManager.Instance.SetTeam(ids);
    }
}
