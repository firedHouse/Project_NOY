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
        owner = rewardUI;
        gameObject.SetActive(true);
    }

    private void OnBack()
    {
        gameObject.SetActive(false);
        owner = null;
    }

    private void OnSkip()
    {
        gameObject.SetActive(false);

        if(owner != null)
        {
            owner.ProceedNextStage();
            owner = null;
        }
    }
}
