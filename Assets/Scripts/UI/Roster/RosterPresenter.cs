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
    [SerializeField] GoldModel GoldModel;
    public string[] beforePlayerTeamID;
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

        Debug.Log($"[RosterButton] {model.PlayerTeamID[0]}");
        Debug.Log($"[RosterButton] {model.PlayerTeamID[1]}");
        Debug.Log($"[RosterButton] {model.PlayerTeamID[2]}");
    }

    public void UpdateList()
    {
        view.CharacterName(model);
        view.CharacterIllust(model);
        view.CharacterIcon(model);
        view.CharacterClass(model);
    }

    public void PrintNewCharacter()
    {
        model.CharacterInfo(0, model.NewCharacterID[0]);
        model.CharacterInfo(1, model.NewCharacterID[1]);
        model.CharacterInfo(2, model.NewCharacterID[2]);
        Debug.Log($"[RosterButton] 영입할 캐릭터 목록");
        UpdateList();
    }


    public void PrintPlayerTeam()
    {
        //플레이어캐릭터 출력
        model.CharacterInfo(0, model.PlayerTeamID[0]);
        model.CharacterInfo(1, model.PlayerTeamID[1]);
        model.CharacterInfo(2, model.PlayerTeamID[2]);
        Debug.Log($"[RosterButton] 플레이어팀 목록");
        UpdateList();
    }

    //클릭한 캐릭터 이미지 넣기
    public void ClickCharacter(int i)
    {
        beforePlayerTeamID = model.PlayerTeamID;
        //if(GoldModel.CurrentGold < 1000)
        //{
        //    Debug.Log("[RosterButton] 골드가 부족합니다.");
        //    return;
        //}
        Debug.Log("[RosterButton] 골드 연결 X : 영입에 필요한 골드 체크 부분 ");

        //추가할 캐릭터 선택
        if (button.ChangePenel.activeSelf == false)
        {
            //joinCharacter 에 저장
            joinCharacter = model.NewCharacterID[i];
            //하단 이미지 활성화
            view.BottomImageActive(true);
            //Bottom칸에 이미지 적용
            view.bottomImage(model, i);
            Debug.Log($"[RosterButton] 새 캐릭터{model.NewCharacterID[i]} 선택됨");
        }

        //방출 캐릭터 선택
        else
        {
            //topImage > originalCharacter로 변경
            view.topImage(model, i);
            view.TopImageActive(true);

            //originalCharacter에 저장
            originalCharacter = model.PlayerTeamID[i];
            Debug.Log($"[RosterButton] 교체할 캐릭터{model.PlayerTeamID[i]} 선택됨");
            originalSlotNumber = i;
        }

        //UI 갱신
        UpdateList();
        Debug.Log($"[RosterButton] UI 갱신");
    }

    public void SwitchCharacter()
    {
        if (originalCharacter != null)
        {
            model.PlayerTeamID[originalSlotNumber] = joinCharacter;
            //선택한 칸에 joinCharacter의 이미지, 이름, 속성, 포지션 출력정보변경
            model.CharacterInfo(originalSlotNumber, joinCharacter);
            //하단 이미지 비활성화
            view.BottomImageActive(false);

            Debug.Log($"[RosterButton] {originalCharacter} 방출");
            Debug.Log($"[RosterButton] {joinCharacter} 영입");

            //joinCharacter 와 originalCharacter를 교체
            originalCharacter = joinCharacter;
            joinCharacter = originalCharacter;
            originalCharacter = null;
            originalSlotNumber = 3;
        }
        else
        {
            Debug.Log($"[RosterButton] 방출 캐릭터를 선택하지 않음");
        }
        UpdateList();
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
            //GoldModel.Decrease(1000);
            Debug.Log($"[RosterButton] 골드 차감됨 -차감 매서드 호출 해야함.");
        }
        else
        {
            Debug.Log($"[RosterButton] 골드 차감 없음.");
        }
    }
}
