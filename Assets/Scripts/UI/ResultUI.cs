using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text resultShillingText;
    [SerializeField] private Button GameExitButton;

    private int earnedShilling;

    private void OnEnable()
    {
        InitResult();
    }

    private void InitResult()
    {
        earnedShilling = EconomyManager.Instance.ResultShilling();

        if (resultShillingText != null)
        {
            resultShillingText.text = $"획득 실링 : {earnedShilling}";
        }

        GameExitButton.onClick.RemoveAllListeners();
        GameExitButton.onClick.AddListener(OnClickGameExit);
    }

    private void OnClickGameExit()
    {
        if (earnedShilling > 0)
        {
            ShillingManager.Instance.AddOutGameShilling(earnedShilling);
        }

        EconomyManager.Instance.ResetEconomy();
        //게임 종료
        Debug.Log("게임 종료!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); 
#endif
    }
}
