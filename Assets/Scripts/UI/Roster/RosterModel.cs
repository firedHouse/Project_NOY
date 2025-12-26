using System.Collections.Generic;
using UnityEngine;

public class RosterModel : MonoBehaviour
{
    //새로 영입가능한 캐릭터 리스트
    //캐릭터의 이름
    //캐릭터 클래스
    //캐릭터 속성

    [SerializeField] RosterPresenter presenter;

    // 모든 캐릭터 데이터 가져오기 
    [SerializeField] private string[] characterDataIDs = { "10001", "10002", "10003", "10004", "10005", "10006", "10007", "10008", "10009" };

    #region Field
    [SerializeField] private string chracterName;
    [SerializeField] private string chracterIllust;
    [SerializeField] private int chracterClass;
    [SerializeField] private int chracterElement;

    private List<CharacterData> PlayerTeam = new List<CharacterData>();

    private System.Random random = new System.Random();


    #endregion

    #region Property 
    public string ChracterName => chracterName;
    public string ChracterIllust => chracterIllust;
    public int ChracterClass => chracterClass;
    public int ChracterElement => chracterClass;

    #endregion
    private void Start()
    {
       //Debug.Log($"[RosterPresenter] 플레이어팀{BattleManager.Instance.PlayerTeam[0].UnitName}");

        //PlayerTeam = BattleManager.Instance.PlayerTeam;
        PlayerTeam = new List<CharacterData>();
        PlayerTeam.Add(TableManager.Instance.CharacterTable.Get("10001"));
        PlayerTeam.Add(TableManager.Instance.CharacterTable.Get("10002"));
        PlayerTeam.Add(TableManager.Instance.CharacterTable.Get("10003"));

        NewListSet();
    }

    public void NewListSet()
    {
        do
        {
            //화면에 표시할 캐릭터 표시
            //중복 검사
            if (TableManager.Instance.CharacterTable == null)
            {
                Debug.Log("[RosterPresenter] 캐릭터테이블 없음");
                return;
            }

            //번호 랜덤으로 뽑기
            int num = random.Next(0, 9);
            CharacterData characterData = TableManager.Instance.CharacterTable.Get(characterDataIDs[num]);

            //유닛네임으로 검색
            for (int i = 0; i < PlayerTeam.Count; i++)
            {
                if (characterData.characterName != PlayerTeam[i].characterName)
                {
                    //중복 없으면 추가
                    chracterName = characterData.characterName;
                    chracterIllust = characterData.characterSkin;
                    chracterClass = characterData.position;
                    chracterElement = characterData.elementUI;
                    Debug.Log("[RosterPresenter] 캐릭터 입력");
                    break;
                }
            }
        } while (chracterName == null);

        Debug.Log("[RosterPresenter] 루프끝");
    }
}
