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
    
    protected override void Start()
    {
        skillView = gameObject.GetComponent<GrowthSkillView>();
        SetSlotUI();
        LoadCharacterList();
    }

    // 캐릭터 리스트 가져오기
    // 캐릭터 수에 따라 캐릭터 슬롯 생성 후 각 슬롯에 캐릭터 정보 띄우기
    protected override void LoadCharacterList()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            //Debug.Log($"[CharacterListPresenter] {characters[i].CharacterName} 불러오기 성공");
            characterSlots[i].UpdateCharacterSlot(characters[i]);
            // 해당 슬롯이 해금되지 않았다면 선택 불가능하게 > 의문 : 그럼 기본적으로 잠금 일러스트로 표시하나?
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
    // 잠금된 캐릭터는 캐릭터 상세 정보가 출력되지 않고 화면만? > 아예 비활성화만하기?
    public override void OnSlotClicked(CharacterListModel character)
    {
        Debug.Log("[CharacterListPresenter] 슬롯 클릭");
        ShowDetailView(character);
        // growthView.SetDetailView(true);
    }

    protected override void ShowDetailView(CharacterListModel character)
    {
        if (character != null)
        {
            model = character;
            UpdateCharacterInfo();
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
        skillView.CharacterInfo(character);
        //growthView.CharacterElement(character);
    }

    public override void SetButtonEvent(CharacterListModel character, UnityAction<CharacterListModel> onClickCallBack)
    {
        throw new NotImplementedException();
    }

    public override void UpdateCharacterInfo()
    {
        skillView.CharacterIllust(model);
    }

    protected override void Init()
    {
        throw new NotImplementedException();
    }
}
