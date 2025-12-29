using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text resultTitle;
    [SerializeField] private Text resultShillingText;
    [SerializeField] private Button backToLobby;

    private int earnedShilling;

    private void OnEnable()
    {
        InitResult();
    }

    private void InitResult()
    {
        if (resultTitle != null)
        {
            resultTitle.text = "결과";
        }

        earnedShilling = EconomyManager.Instance.ResultShilling();

        if (resultShillingText != null)
        {
            resultShillingText.text = $"획득 실링 : {earnedShilling}";
        }

        backToLobby.onClick.RemoveAllListeners();
        backToLobby.onClick.AddListener(OnClickGoLobby);
    }

    private void OnClickGoLobby()
    {
        if (earnedShilling > 0)
        {
            ShillingManager.Instance.AddOutGameShilling(earnedShilling);
        }

        EconomyManager.Instance.ResetEconomy();

        SceneManager.LoadScene("LobbyScene");
    }

}
