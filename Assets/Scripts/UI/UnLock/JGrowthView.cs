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
    [SerializeField] private Button unlockButton;

    public Button UnlockButton  => unlockButton;

    private void Start()
    {
        unlockButton = (GameObject.Find("CharacterUnlockButton")).GetComponent<Button>();
        lockedIcon = GameObject.Find("CharacterLockIcon");
    }

    // 캐릭터 데이터 표시하는 메서드에 추가
    // unlock 이 false일 경우 해금 버튼을 표시
    // unlock 이 false일 경우 자물쇠 아이콘을 표시
    // unlock 값에 따라 일러스트 변경 
    //public void UpdateCharacterDetailView(bool isLocked)
    //{
    //    ChangeLockedUIActivation(isLocked);
    //}

    // 잠금 아이콘 비활성화
    public void ChangeLockedUIActivation(bool isUnlocked)
    {
        // 값이 true면 비활성화
        lockedIcon.SetActive(!isUnlocked);
        unlockButton.gameObject.SetActive(!isUnlocked);
    }

    //// 이 버튼의 의도: 클릭하여 해금
    //public void SetButtonEvent(CharacterListModel character, UnityAction<CharacterListModel> onClickCallBack)
    //{
        
    //    Debug.Log("해금 버튼 설정");
    //    unlockButton.onClick.AddListener(() => onClickCallBack(character));
    //}
}
