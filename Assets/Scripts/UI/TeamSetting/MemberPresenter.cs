using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// 팀 구성 페이지에서 사용할 프레젠터
public class MemberPresenter : CharacterPresenterBase
{
    [SerializeField] protected GrowthSkillView skillView;
    [SerializeField] private TeamSelectView selectView;
    
    // 테스트용 팀 멤버 id 배열
    private string[] ids = new string[3];
    private CharacterListModel[] teamMembers = new CharacterListModel[3];
    public string[] Ids => ids;
    public CharacterListModel[] TeamMembers => teamMembers;
    
    protected override void Start()
    {
        Debug.Log($"[MemberPresenter] start");
        skillView = gameObject.GetComponent<GrowthSkillView>();
        selectView = GameObject.Find("SeletedTeamPanel").GetComponent<TeamSelectView>();
        SetSlotUI();
        LoadCharacterList();
        skillView.SetDetailView(false);
        selectView.SetAllButtonInteractable(false);
        SetSelectTeamUI();
        // if (selectView.Slots != null)
        // {
        //     foreach (var slot in selectView.Slots)
        //     {
        //         if (slot != null)
        //         {
        //         Debug.Log($"[MemberPresenter] 슬롯 설정");
        //         SetButtonEvent(slot, SetTeamPosition);   
        //         }
        //         else
        //         {
        //             Debug.Log($"[MemberPresenter] 슬롯 설정 실패");
        //         }
        //
        //         Debug.Log($"[MemberPresenter] 슬롯 설정 끝");
        //     }
        // }
        // else
        // {
        //     Debug.Log($"[MemberPresenter] 슬롯 없어서 슬롯 설정 실패");
        // }
    }

    private void SetSelectTeamUI()
    {
        Debug.Log($"[MemberPresenter] 슬롯 설정 시작");
        
        if (selectView.Slots.Length != 0)
        {
            foreach (var slot in selectView.Slots)
            {
                if (slot != null)
                {
                    Debug.Log($"[MemberPresenter] 각 슬롯 설정");
                    SetButtonEvent(slot, SetTeamPosition);
                }
                else
                {
                    Debug.Log($"[MemberPresenter] 슬롯 설정 실패");
                }

            }
            Debug.Log($"[MemberPresenter] 슬롯 설정 끝");
        }
        else
        {
            Debug.Log($"[MemberPresenter] 슬롯 없어서 슬롯 설정 실패");
        }
        Debug.Log($"[MemberPresenter] 아무튼 슬롯 설정 끝냄");
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
    /// 이름, 해금 여부, 코드네임, 속성, 대사, 인포, 업데이트
    /// </summary>
    public override void UpdateMainInfo(CharacterListModel character)
    {
        Debug.Log($"[CharacterListPresenter] 기본 정보 업데이트");
        skillView.CharacterName(character);
        //growthView.CharacterElement(character);
    }

    // 전/중/후열 중에 하나를 클릭하면 해당 캐릭터 슬롯을 받아감
    // 현재 모델을 클릭된 열에 넣어준다
    public void SetTeamPosition(MemberSlot selectedSlot)
    {
        Debug.Log($"[MemberPresenter] {selectedSlot.MemberPosition} 슬롯 클릭됨");
        
        int idx = Array.IndexOf(teamMembers, model);
        // if 다른 배열 위치에 저장되어 있었다면 해당 포지션의 slotModel을 null로 변경해준다
        if (idx != -1)
        {
            teamMembers[idx] = null;
            selectView.slots[idx].SlotModel = null;
            selectView.slots[idx].selectedCharacterName.text = "";
            Debug.Log($"[MemberPresenter] 위치 중복으로 이동됨 {teamMembers[idx]} {selectView.slots[idx].SlotModel}");
        }
        Debug.Log($"[MemberPresenter] {selectedSlot.transform.parent.name} {model.CharacterName}");
        selectedSlot.SlotModel = model;
        selectedSlot.SlotCharacter.text = model.CharacterName;
        // 해당 슬롯에 모델 저장
        teamMembers[selectedSlot.memberPosition] = model;
    }
    
    // 클릭하면 버튼의 슬롯 모델에 현재 프레젠터의 모델을 넣음 
    public void SetButtonEvent(MemberSlot slot, UnityAction<MemberSlot> onClickCallBack)
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

        // 슬롯에 있는 애들의 캐릭터 리스트 데이터를 가져와 배열에 저장
        for (int i = 0; i < selectView.Slots.Length; i++)
        {
            teamMembers[i] = selectView.Slots[i].SlotModel;
        }
        
        Debug.Log($"[MemberPresenter] {ids[0]} - {ids[1]} - {ids[2]}");
        Debug.Log($"[MemberPresenter] {teamMembers[0].CharacterName} - {teamMembers[1].CharacterName} - {teamMembers[2].CharacterName}");
        
        // 로비 매니저에 구성된 팀원 저장
        LobbyManager.Instance.SetTeam(ids);
        LobbyManager.Instance.SetTeamData(teamMembers);
        // 씬 전환
        SceneManager.LoadScene("HyeonGuTestScene");
    }
}
