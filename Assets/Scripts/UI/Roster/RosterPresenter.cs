using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class RosterPresenter : MonoBehaviour
{
    //구성원 제외 모든 캐릭터 중 3명을 랜덤으로 화면에 출력
    //출력캐릭터 리스트 결정하기
    [SerializeField] RosterView view;
    [SerializeField] RosterModel model;
    [SerializeField] RosterButton button;
    [SerializeField] Roster_ChangeView changeView;

    public List<string> beforePlayerTeamID = new List<string>();
    //[SerializeField] RosterSwap swap;

    public string joinCharacter;
    public string originalCharacter;
    public int originalSlotNumber;



    public void Start()
    {
        joinCharacter = null;
        originalCharacter = null;
        button = GameObject.Find("TeamComposition").GetComponent<RosterButton>();
        if (model == null)
        {
            model = GameObject.Find("RosterPanel").GetComponent<RosterModel>();
            Debug.Log($"[RosterPresenter] 모델 다시 가져옴");
        }
        //영입할 캐릭터 데이터 출력
        PrintNewCharacter();
    }

    public void ConfirmButton()
    {
        Debug.Log("[RosterButton] 최종 팀 리스트");

        Debug.Log($"[RosterButton] 전열 {model.PlayerTeamID[0]}");
        Debug.Log($"[RosterButton] 중열 {model.PlayerTeamID[1]}");
        Debug.Log($"[RosterButton] 후열 {model.PlayerTeamID[2]}");
    }

    public void UpdateNewCharacter()
    {
        view.CharacterName(model);
        view.CharacterIllust(model);
        view.CharacterIcon(model);
        view.CharacterClass(model);
    }

    public void PrintNewCharacter()
    {
        model.UpdateCharacterInfo(0, model.NewCharacterID[0]);
        model.UpdateCharacterInfo(1, model.NewCharacterID[1]);
        model.UpdateCharacterInfo(2, model.NewCharacterID[2]);
        Debug.Log($"[RosterButton] 영입할 캐릭터 목록");
        UpdateNewCharacter();
    }


    public void PrintPlayerTeam()
    {
        //플레이어캐릭터 출력(후열에 전열캐릭터)
        model.UpdateCharacterInfo(0, model.PlayerTeamID[0]);
        model.UpdateCharacterInfo(1, model.PlayerTeamID[1]);
        model.UpdateCharacterInfo(2, model.PlayerTeamID[2]);
        changeView.CharacterIllust(model);

        Debug.Log($"[RosterButton] 플레이어팀 목록");
    }



    //클릭한 캐릭터 이미지 넣기
    public void ClickCharacter(int i)
    {
        beforePlayerTeamID = model.PlayerTeamID;

        //추가할 캐릭터 선택
        if (button.ChangePenel.activeSelf == false)
        {
            //joinCharacter 에 저장
            joinCharacter = model.NewCharacterID[i];
            //하단 이미지 활성화
            changeView.BottomImageActive(true);
            //Bottom칸에 이미지 적용
            changeView.bottomImage(model, i);
            Debug.Log($"[RosterButton] 새 캐릭터{model.NewCharacterID[i]} 선택됨");
        }

        //방출 캐릭터 선택
        else
        {
            //topImage > originalCharacter로 변경
            changeView.topImage(model, i);
            changeView.TopImageActive(true);

            //originalCharacter에 저장
            originalCharacter = model.PlayerTeamID[i];
            Debug.Log($"[RosterButton] 교체할 캐릭터{model.PlayerTeamID[i]} 선택됨");
            originalSlotNumber = i;
        }

        if(joinCharacter != null && originalCharacter != null)
        {
            //팀구성 리스트 아이디 변경
            SwitchCharacter();
            //재 출력
            changeView.CharacterIllust(model);
        }
        //UI 갱신
        changeView.CharacterIllust(model);
        Debug.Log($"[RosterButton] UI 갱신");
    }

    public void SwitchCharacter()
    {
        if (originalCharacter != null)
        {
            //플레이어팀 구성 변경
            model.PlayerTeamID[originalSlotNumber] = joinCharacter;
            //선택한 칸에 joinCharacter의 이미지, 이름, 속성, 포지션 출력 전 정보 변경
            model.UpdateCharacterInfo(originalSlotNumber, joinCharacter);
            //하단 이미지 비활성화
            changeView.BottomImageActive(false);

            Debug.Log($"[RosterButton] {originalCharacter} 방출");
            Debug.Log($"[RosterButton] {joinCharacter} 영입");

            //joinCharacter 와 originalCharacter를 교체
            joinCharacter = originalCharacter;
            originalCharacter = null;

            //숫자는 초기화 어떻게 하지
            originalSlotNumber = 3;
        }
        else
        {
            Debug.Log($"[RosterButton] 방출 캐릭터를 선택하지 않음");
        }
    }

    public bool IsChangeTeam()
    {
        if(beforePlayerTeamID == model.PlayerTeamID)
        {
            Debug.Log($"[RosterButton] 팀원 교체됨");
            return true;
        }
        return false;
    }

    public void GoldCal()
    {
        if (IsChangeTeam() == true)
        {
            EconomyManager.Instance.SpendGold(500);
            Debug.Log($"[RosterButton] 골드 차감됨 -차감 매서드 호출 해야함.");
        }
        else
        {
            Debug.Log($"[RosterButton] 골드 차감 없음.");
        }
    }
}
