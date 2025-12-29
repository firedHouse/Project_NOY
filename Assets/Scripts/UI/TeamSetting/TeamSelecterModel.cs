using UnityEngine;
using System.Linq;


public class TeamSelecterModel : MonoBehaviour
{
    // 버튼들 
    // [SerializeField] private CharacterSlot[] slots;
    // public CharacterSlot[] Slots => slots;

    private void Awake()
    {
        // slots = gameObject.GetComponentsInChildren<CharacterSlot>().ToArray();
    }

    // 해당 슬롯에서 캐릭터 모델을 반환
    public CharacterListModel GetSlotModel(CharacterSlot slot)
    {
        return slot.SlotModel;
    }

    // 캐릭터 모델을 슬롯에 넣어줌
    // 파라미터는 버튼 아니면 게임 오브젝트
    public void SetSlotModel(GameObject slot)
    {

    }

    // 이벤트로 버튼 내 슬롯에 모델이 변경될 때마다 모델 리스트 갱신 

}
