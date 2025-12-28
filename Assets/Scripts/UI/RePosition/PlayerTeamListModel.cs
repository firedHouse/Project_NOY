using System.Collections.Generic;
using UnityEngine;

public class PlayerTeamListModel : MonoBehaviour
{
    [SerializeField] FormationPresenter formationPresenter;

    //팀 아이디를 기준으로 중복 평가
    #region Field
    [SerializeField] private string[] chracterID = new string[3];
    [SerializeField] private string[] chracterName = new string[3];
    [SerializeField] private string[] chracterIllust = new string[3];
    [SerializeField] private int[] chracterClass = new int[3];
    [SerializeField] private int[] chracterElement = new int[3];

    private List<string> playerTeamID = new List<string>();

    #endregion

    #region Property 
    public string[] ChracterID => chracterID;

    public string[] ChracterName => chracterName;
    public string[] ChracterIllust => chracterIllust;
    public List<string> PlayerTeamID => playerTeamID;
    #endregion

    private void Awake()
    {
        //배열 생성
        chracterName = new string[3];
        chracterIllust = new string[3];
        chracterClass = new int[3];
        chracterElement = new int[3];
    }
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            playerTeamID.Add(LobbyManager.Instance.SelectedCharacterIDs[i]);
        }

        Debug.Log($"[RosterModel] {playerTeamID.Count}");
    }

    //전중후열순으로 기록
    public void UpdateCharacterInfo(int i, string ID)
    {
        CharacterData characterData = TableManager.Instance.CharacterTable.Get(ID);

        chracterID[i] = characterData.characterID;
        chracterName[i] = characterData.characterName;
        chracterIllust[i] = characterData.characterSkin;
    }
}