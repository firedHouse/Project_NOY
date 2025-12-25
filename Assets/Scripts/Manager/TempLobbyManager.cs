using UnityEngine;

//로비매니저 만들어지기 전 임시
//겸 테스트배틀스타터랑 연결
public class TempLobbyManager
{
    public static TempLobbyManager Instance = new TempLobbyManager();

    private string[] testCharacterIDs = new string[3];

    public TempLobbyManager()
    {
        testCharacterIDs = new string[] { "10001", "10004", "10007" };
    }

    //테스트배틀스타터에 테스트용 아이디 넣을 때 사용
    public void SetTestCharacterIDs(string[] ids)
    {
        this.testCharacterIDs = ids;
    }

    //배틀매니저용
    public string[] GetSelectedCharacterIDs()
    {
        //테스트용 데이터 반환
        return testCharacterIDs;
    }
}