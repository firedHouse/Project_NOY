using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class RosterModel : MonoBehaviour
{
    //새로 영입가능한 캐릭터 리스트
    //캐릭터의 이름
    //캐릭터 클래스
    //캐릭터 속성

    [SerializeField] RosterPresenter presenter;

    // 모든 캐릭터 ID 가져오기 
    [SerializeField] private List<string> allID = new List<string>() { "10001", "10002", "10003", "10004", "10005", "10006", "10007", "10008", "10009" };

    //팀 아이디를 기준으로 중복 평가
    #region Field
    [SerializeField] private string[] chracterName = new string[3];
    [SerializeField] private string[] chracterIllust = new string[3];
    [SerializeField] private int[] chracterClass = new int[3];
    [SerializeField] private int[] chracterElement = new int[3];

    private string[] playerTeamID;
    //private List<string> testTeam = new List<string>();
    private List<string> newCharacterID = new List<string>();

    private System.Random random = new System.Random();


    #endregion

    #region Property 
    public string[] ChracterName => chracterName;
    public string[] ChracterIllust => chracterIllust;
    public int[] ChracterClass => chracterClass;
    public int[] ChracterElement => chracterClass;
    public string[] PlayerTeamID => playerTeamID;
    public List<string> NewCharacterID => newCharacterID;

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
        playerTeamID = LobbyManager.Instance.SelectedCharacterIDs;

        Debug.Log($"[RosterModel] {playerTeamID.Length}");
        //Debug.Log($"[RosterModel] {testTeam.Count}");
        NewListSet();
    }

    public void CharacterInfo(int i, string ID)
    {
        CharacterData characterData = TableManager.Instance.CharacterTable.Get(ID);

        chracterName[i] = characterData.characterName;
        chracterIllust[i] = characterData.characterSkin;
        chracterClass[i] = characterData.position;
        chracterElement[i] = characterData.elementUI;
    }

    #endregion

    public void NewListSet()
    {
        Debug.Log("[RosterModel] 리스트 제작");
        //현재 팀인 캐릭터 리스트에서 제거
        //for (int i = 0; i < playerTeam.Count; i++)
        //{
        //    allCharacterDataID.Remove(playerTeam[i].UnitID);
        //    Debug.Log($"[RosterModel] {playerTeam[i].UnitName} 제거");
        //}

        for (int i = 0; i < playerTeamID.Length; i++)
        {
            if(allID.Contains(playerTeamID[i]))
            {
                allID.Remove(playerTeamID[i]);
                Debug.Log($"[RosterModel] {playerTeamID[i]} 제거");
            }
        }


        //캐릭터 리스트에서 중복되지 않도록 세개 추가
        for (int i = 0; i < 3;)
        {
            int chaNum = random.Next(0, allID.Count);
            Debug.Log($"[RosterModel] 번호 : {allID[chaNum]} ");

            bool isAdd = true;

            for (int j = 0; j < newCharacterID.Count; j++)
            {
                if (allID[chaNum] == newCharacterID[j])
                {
                    isAdd = false;
                    break;
                }
            }

            if (isAdd == true)
            {
                newCharacterID.Add(allID[chaNum]);
                Debug.Log($"[RosterModel] {allID[chaNum]} 추가");
                i++;
            }
        }
    }
}
