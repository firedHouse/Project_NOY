using UnityEngine;
using UnityEngine.UI;

public class RewardSkipConfirmUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button skipButton;

    private RewardUI owner;

    private void Awake()
    {
        gameObject.SetActive(false);

        backButton.onClick.AddListener(OnBack);
        skipButton.onClick.AddListener(OnSkip);
    }

    public void Open(RewardUI rewardUI)
    {
        Debug.Log("[SkipConfirmUI] Open 호출됨");
        owner = rewardUI;
        gameObject.SetActive(true);
    }

    private void OnBack()
    {
        gameObject.SetActive(false);

        if(owner != null)
        {
            owner.OnSkipConfirmClosed();
        }
        owner = null;
    }

    private void OnSkip()
    {
        // 1. 확인창 끄기
        gameObject.SetActive(false);

        if (owner != null)
        {
            owner.OnSkipConfirmClosed();

            Transform recruitBtnTr = owner.transform.Find("RecruitButton");

            bool isBossRound = (recruitBtnTr != null && recruitBtnTr.gameObject.activeSelf);

            if (isBossRound)
            {
                RecruitUI recruitUI = FindFirstObjectByType<RecruitUI>(FindObjectsInactive.Include);

                if (recruitUI != null)
                {
                    Debug.Log("[SkipConfirm] 스킵 후 영입 UI 강제 오픈");
                    recruitUI.Open();

                    owner.isRecruitOpen = true;
                }
                else
                {
                    Debug.LogError("씬에서 RecruitUI 스크립트를 가진 오브젝트를 찾을 수 없습니다!");
                }
            }
            else
            {
                owner.ProceedNextStage();
            }

            owner = null;
        }
    }
}
