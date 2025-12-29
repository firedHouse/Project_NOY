using UnityEngine;
using UnityEngine.UI;

public class NextStageButton : MonoBehaviour
{
    [Header("다음스테이지 텍스트")]
    [SerializeField] Text teamButtonText;

    private bool isBossStage;
    public bool IsBossStage => isBossStage;

    private void Start()
    {
        ButtonTextSwitch();
    }

    private void ButtonTextSwitch()
    {
        if (StageManager.Instance.CurrentRound == 5)
        {
            teamButtonText.text = "팀 구성 변경";
            isBossStage = true;
        }
        else if (StageManager.Instance.CurrentRound != 5)
        {
            teamButtonText.text = "다음 스테이지";
            isBossStage = false;
        }
    }
}
