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

        int stage = StageManager.Instance.CurrentStage;
        int round = StageManager.Instance.CurrentRound;

        stageInfoText.text = $"스테이지 {stage}-{round}";
    }
}
