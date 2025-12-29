using System.Collections.Generic;
using UnityEngine;

public class FormationPresenter : MonoBehaviour
{
    //구성원 제외 모든 캐릭터 중 3명을 랜덤으로 화면에 출력
    //출력캐릭터 리스트 결정하기
    [SerializeField] FormationView view;
    [SerializeField] PlayerTeamListModel model;
    [SerializeField] FarmationButton button;

    private string firstCharacter;
    private string secondCharacter;
    private int firstSlotNum;



    public void OnEnable()
    {
        firstCharacter = null;
        secondCharacter = null;
        if(button == null)
        {
            button = GameObject.Find("ChangePositionPanel").GetComponent<FarmationButton>();
        }

        //플레이터팀 캐릭터 데이터 출력
        PrintPlayerTeam();
    }

    public void ConfirmButton()
    {
        Debug.Log("[RosterButton] 최종 팀 리스트");

        Debug.Log($"[RosterButton] 전열 {model.PlayerTeamID[0]}");
        Debug.Log($"[RosterButton] 중열 {model.PlayerTeamID[1]}");
        Debug.Log($"[RosterButton] 후열 {model.PlayerTeamID[2]}");
    }

    public void PrintPlayerTeam()
    {
        //플레이어캐릭터 출력(후열에 전열캐릭터)
        model.UpdateCharacterInfo(0, model.PlayerTeamID[0]);
        model.UpdateCharacterInfo(1, model.PlayerTeamID[1]);
        model.UpdateCharacterInfo(2, model.PlayerTeamID[2]);
        view.CharacterIllust(model);

        Debug.Log($"[RosterButton] 플레이어팀 목록");
    }



    //클릭한 캐릭터 이미지 넣기
    public void ClickCharacter(int i)
    {
        Debug.Log("[RosterButton] 골드 연결 X : 영입에 필요한 골드 체크 부분 ");

        //변경할 캐릭터 선택
        if (firstCharacter == null)
        {
            //자리를 바꿀 캐릭터 저장
            firstCharacter = model.PlayerTeamID[i];
            //상단 이미지 활성화
            view.TopImageActive(true);
            //Bottom칸에 이미지 적용
            view.topImage(model, i);
            firstSlotNum = i;
            Debug.Log($"[RosterButton] 새 캐릭터{model.PlayerTeamID[i]} 선택됨");
        }

        //방출 캐릭터 선택
        else 
        {
            secondCharacter = model.PlayerTeamID[i];
            //팀ID리스트 변경
            model.PlayerTeamID[i] = firstCharacter;
            model.PlayerTeamID[firstSlotNum] = secondCharacter;
            Debug.Log($"[RosterButton] {firstCharacter}와 {firstCharacter}가 교체됨");

            view.TopImageActive(false);
            firstCharacter = null;
            secondCharacter = null;
        }

        //UI 갱신
        PrintPlayerTeam();
        view.CharacterIllust(model);
        Debug.Log($"[RosterButton] UI 갱신");
    }

    public void TeamListPrint()
    {
        Debug.Log($"[RosterButton] 팀 구성");
        Debug.Log($"[RosterButton] {model.ChracterName[0]}");
        Debug.Log($"[RosterButton] {model.ChracterName[1]}");
        Debug.Log($"[RosterButton] {model.ChracterName[2]}");
    }
}
