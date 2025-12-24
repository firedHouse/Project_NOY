using UnityEngine;
using UnityEngine.UI;

// 각 캐릭터마다 리스트에 표시해줄 View
// 캐릭터 unlock 속성이 unlock이면 검은 실루엣 이미지로 처리
public class CharacterSlot : MonoBehaviour
{
    //[SerializeField] private CharacterListPresenter characterListPresenter;
    [Header("캐릭터 슬롯 프리팹")]
    [SerializeField] private Button button;

    private string id;

    // 캐릭터 데이터 받아와서 띄우기
    // 초기화 하면서 필요한 데이터 모두 업데이트하기
    // 슬롯 기준으로는 id만 알면 된다?
    // 
    public void UpdateCharacterSlot(string charID)
    {
        // 캐릭터 두상 일러스트로 변경
        // 리소스 들어오기 전까지는 들어오는 데이터로 파악
        //button.image.
        id = charID;
        Debug.Log($"[CharacterSlot] {id} 슬롯에 로드 완료");
    }

    // 해금 여부에 따라 UI 변경
    // 변경에 따라 갱신되어야 함

    // 성장 레벨에 따라 일러스트 UI 변경
}
