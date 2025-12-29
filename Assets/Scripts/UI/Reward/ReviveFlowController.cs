using System.Linq;
using UnityEngine;

public class ReviveFlowController : MonoBehaviour
{
    public static ReviveFlowController instance;

    [Header("Formation UI")]
    [SerializeField] private GameObject formationPanel;
    [SerializeField] PlayerTeamListModel playerTeamListModel;

    private Character revivedCharacter;

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
        revivedCharacter = revived;

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

        if (orderedIDs == null || orderedIDs.Count == 0)
        {
            Debug.LogWarning("팀 ID 리스트가 비어있음");
            return;
        }

        var team = BattleManager.Instance.PlayerTeam;

        for (int i = 0; i < orderedIDs.Count; i++)
        {
            var targetID = orderedIDs[i];
            int currentIndex = team.FindIndex(c => c.UnitID == targetID);

            if (currentIndex != -1 && currentIndex != i)
            {
                var temp = team[i];
                team[i] = team[currentIndex];
                team[currentIndex] = temp;
            }
        }

        BattleManager.Instance.SendMessage("UpdateTeamPositions", team, SendMessageOptions.DontRequireReceiver);

        Debug.Log("전투 팀 재배치 완료");

        formationPanel.SetActive(false);

        revivedCharacter = null;

    }
}
