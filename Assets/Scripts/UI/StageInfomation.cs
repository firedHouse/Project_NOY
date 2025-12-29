using UnityEngine;
using UnityEngine.UI;

public class StageInfomation : MonoBehaviour
{
    [SerializeField] private Text stageInfoText;

    public void Update()
    {
        UpdateStageInfo();
    }

    private void UpdateStageInfo()
    {
        if (StageManager.Instance == null)
        {
            stageInfoText.text = "";
            return;
        }

        if (StageManager.Instance.CurrentRound == 5)
        {
            stageInfoText.text += "(Boss)";
            return;
        }

        int stage = StageManager.Instance.CurrentStage;
        int round = StageManager.Instance.CurrentRound;

        stageInfoText.text = $"스테이지 {stage}-{round}";
    }
}
