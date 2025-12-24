using UnityEngine;

// 캐릭터 목록에서 캐릭터 정보 출력에 전체적으로 사용할 프레젠터
public class CharacterListPresenter : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private CharacterListView characterListView;
    [SerializeField] private CharacterListModel characterListModel;

    // 캐릭터 리스트 가져오기

    // 캐릭터 해금
    private void UnlockCharacter()
    {
        characterListModel.Unlock();
        // 뷰에서 해금에 따라 UI 갱신
        // 해금 버튼 비활성화, 해금 아이콘 비활성화
        // 성장 버튼 선택 가능하도록 변경
        //characterListView.
    }

    // 클릭한 캐릭터 슬롯에 해당하는 상세 정보 띄워주기
}
