using System.Linq;
using UnityEngine;

public class ReviveFlowController : MonoBehaviour
{
    public static ReviveFlowController instance;

    [Header("Formation UI")]
    [SerializeField] private GameObject formationPanel;
    [SerializeField] PlayerTeamListModel playerTeamListModel;

    private void Awake()
    {
        if (instance == null)
        { instance = this; }
        else
        { Destroy(gameObject); }
    }

    //부활 아이템 사용 완료 후 호출
    public void StartRevivalFlow(Character revived)
    {
        Debug.Log("부활 후 배치 변경 단계 진입");

        OpenFormationUI();
    }
    // 재배치 UI 열기
    private void OpenFormationUI()
    {
        if (formationPanel == null)
        {
            Debug.LogError("formationPanel이 연결되지 않았습니다.");
            return;
        }

        formationPanel.SetActive(true);
    }
    // Formation UI에서 결정 버튼에서 호출
    public void ApplyFormationResult()
    {
        var orderedIDs = playerTeamListModel.PlayerTeamID;
        BattleManager.Instance.ApplyPlayerFormation(orderedIDs);

        formationPanel.SetActive(false);

        FindObjectOfType<RewardFlowController>()?.ResetFlow();

    }
}
