using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class FormationUI : MonoBehaviour
{
    public static FormationUI Instance;

    [Header("UI Components")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Image[] slotImages;

    private int firstSelectedIndex = -1;
    // ID 대신 실제 Character 객체 리스트를 들고 있습니다.
    private List<Character> tempPlayerTeam = new List<Character>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        mainPanel.SetActive(false);
        confirmButton.onClick.AddListener(OnConfirmClick);
    }

    public void Open(Character revivedCharacter)
    {
        // BattleManager의 실제 아군 리스트를 복사해옵니다.
        tempPlayerTeam = new List<Character>(BattleManager.Instance.PlayerTeam);

        firstSelectedIndex = -1;
        mainPanel.SetActive(true);
        RefreshUI();
    }

    private void RefreshUI()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i < tempPlayerTeam.Count)
            {
                Character charObj = tempPlayerTeam[i];

                // Character 클래스에 있는 currentSkinSprite를 직접 참조합니다.
                if (charObj != null && charObj.currentSkinSprite != null)
                {
                    slotImages[i].sprite = charObj.currentSkinSprite;
                    slotImages[i].color = (i == firstSelectedIndex) ? Color.yellow : Color.white;
                    slotImages[i].gameObject.SetActive(true);
                }
                else
                {
                    // 혹시라도 Sprite가 없다면 SpriteRenderer에서라도 가져옵니다.
                    slotImages[i].sprite = charObj?.GetComponent<SpriteRenderer>().sprite;
                    slotImages[i].gameObject.SetActive(slotImages[i].sprite != null);
                }
            }
            else
            {
                slotImages[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnSlotClick(int i)
    {
        if (firstSelectedIndex == -1)
        {
            firstSelectedIndex = i;
        }
        else
        {
            // 리스트 내의 Character 객체 위치를 스왑합니다.
            Character temp = tempPlayerTeam[firstSelectedIndex];
            tempPlayerTeam[firstSelectedIndex] = tempPlayerTeam[i];
            tempPlayerTeam[i] = temp;

            firstSelectedIndex = -1;
        }
        RefreshUI();
    }

    private void OnConfirmClick()
    {
        // 정렬된 객체 리스트에서 ID만 뽑아서 전달하거나, 리스트 자체를 적용합니다.
        List<string> orderedIDs = tempPlayerTeam.Select(c => c.UnitID).ToList();

        // 1. 배틀 매니저 좌표 갱신
        BattleManager.Instance.ApplyPlayerFormation(orderedIDs);

        // 2. 데이터 모델 갱신 (다음 스테이지에서도 유지되도록)
        // 만약 playerTeamListModel이 있다면 여기서 업데이트해줍니다.

        mainPanel.SetActive(false);
        FindFirstObjectByType<RewardFlowController>()?.ResetFlow();
    }
}

