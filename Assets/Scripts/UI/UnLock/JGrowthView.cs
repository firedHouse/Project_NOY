using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// 혜주님 캐릭터 상세 UI와 합칠 예정
// 해금에 따라 변경될 것들 구현
public partial class GrowthView : MonoBehaviour
{
    [Header("잠금 아이콘")]
    [SerializeField] private GameObject lockedIcon;
    
    [Header("해금 버튼")]
    [SerializeField] private GameObject unlockButton;

    private void Start()
    {
        // 스크립트로 오브젝트 할당했더니 널참조 오류나서 인스펙터에서 연결
        //unlockButton = GameObject.Find("CharacterUnlockButton").GetComponent<Button>();
        //lockedIcon = GameObject.Find("CharacterLockIcon").GetComponent<RawImage>();
    }

    // 캐릭터 데이터 표시하는 메서드에 추가
    // unlock 이 false일 경우 해금 버튼을 표시
    // unlock 이 false일 경우 자물쇠 아이콘을 표시
    public void ChangeLockedUIActivation(bool isUnlocked)
    {
        // 값이 true면 비활성화
        unlockButton.SetActive(!isUnlocked);
        lockedIcon.SetActive(!isUnlocked);
    }
}
