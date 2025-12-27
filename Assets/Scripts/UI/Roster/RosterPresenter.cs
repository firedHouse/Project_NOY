using UnityEditor.U2D.Animation;
using UnityEngine;

public class RosterPresenter : MonoBehaviour
{
    //구성원 제외 모든 캐릭터 중 3명을 랜덤으로 화면에 출력
    //출력캐릭터 리스트 결정하기

    [SerializeField] RosterView view;
    [SerializeField] RosterModel model;

    //모델에서 불러온 리스트를 뷰에 띄우기
    //

    public void OnEnable()
    {
        view.CharacterName(model);
        view.CharacterIllust(model);
        view.CharacterIcon(model);
        view.CharacterClass(model);

    }
}
        


