using UnityEngine;

public class RosterPresenter : MonoBehaviour
{
    // 캐릭터 슬롯과 상세 정보 UI에 사용할 모델
    // 필요한 정보: 아이디, 해금 여부, 이름, 코드네임, 포지션, 속성, 레벨(학년), 대사, 세부 설정, 스킨, 
    // 팀구성 팝업만 사용할 정보 : 스킬(팀구성), 
    [SerializeField] RosterView[] view = new RosterView[3];
    [SerializeField] RosterModel[] model = new RosterModel[3];

    private void Start()
    {
        for(int i = 0; i < view.Length; i++)
        {
            view[i].InitPage(model[i]);
        }
    }
}

