using System;
using System.Collections.Generic;
using UnityEngine;

//딕셔너리 -> List로 변경해서 JsonUtility로 저장할 수 있도록 만드는
//저장용 껍데기들

[System.Serializable]
public class UserSaveData
{
    //일단은 만들어두고 성장부터, 담당자 X
    public List<string> unlockedCharacters = new List<string>();
    public List<CharacterGradeSave> characterGrades = new List<CharacterGradeSave>();
}

//캐릭터마다의  성장여부를 저장할 클래스
[System.Serializable]
public class CharacterGradeSave
{
    public string charID;
    public int gradeLevel; // 0=1학년, 1=2학년, 2=3학년
}



//아웃게임에서 성장시킨 정보를 런타임 메모리에 관리하고 로컬에 저장/로드하는 클래스
//게임 시작 시 PlayerPrefs에 저장된 JSON 문자열 읽어오기
//데이터가 존재하면 복구하고, 없으면 초기값으로 설정
public class UserDataManager : Singleton<UserDataManager>
{
    //List 사용(hashSet이라는 거 써봐도 좋을 듯)
    private List<string> unlockedCharList = new List<string>();

    //캐릭터별 현재 학년 레벨 저장
    //key: 캐릭터ID, Value: 0/1/2 = 1/2/3학년

    private Dictionary<string, int> charGradeDict = new Dictionary<string, int>();

    private const string SAVE_KEY = "NOY_USER_DATA";

    protected override void Awake()
    {
        base.Awake();
        LoadGame();
    }



    //해금 여부 조회
    public bool IsCharacterUnlocked(string charID)
    {
        //리스트 내부에 ID가 있는지 체크
        return unlockedCharList.Contains(charID);
    }

    //캐릭터 해금
    public void UnlockCharacter(string charID)
    {
        //중복 방지, 없을 때만 넣도록 검사
        if (!unlockedCharList.Contains(charID))
        {
            unlockedCharList.Add(charID);
            SaveGame();
        }
    }
    //캐릭터 성장치 조회 메서드
    public int GetCharacterGrade(string charID)
    {
        if (charGradeDict.ContainsKey(charID))
        {
            return charGradeDict[charID];
        }
        return 0; //기본 1학년
    }

    //캐릭터 성장 시도


    //실패하면 실패
    public void TryUpgradeCharacter(string charID)
    {
        //현재 레벨 가져와서 최고레벨인지 체크
        int currentLevel = GetCharacterGrade(charID);
        if (currentLevel >= 2)
        {
            Debug.Log("최고학년이므로 리턴");
            return;
        }

        //다음 단계 데이터 가져오기
        int nextLevel = currentLevel + 1;

        //테이블매니저에 추가한 GetGradeData 사용
        GradeData nextGradeData = TableManager.Instance.GetGradeData(charID, nextLevel);

        if (nextGradeData == null)
        {
            Debug.LogError($"성장 데이터 못 찾음 ID:{charID}{nextLevel}");
            return;
        }

        //받아온 Grade 데이터 중 실링 요구치 받아오고
        int cost = nextGradeData.needShilling1;

        //실링매니저한테 결제 요청
        if (ShillingManager.Instance.TrySpendShilling(cost))
        {
            //성공하면 성장
            charGradeDict[charID] = nextLevel;
            //데이터 저장
            SaveGame();
            Debug.Log($"성장 완료 및 데이터 저장 {charID}: {nextLevel + 1}학년 달성");
        }
        else
        {
            Debug.Log("실패 시 팝업 등 Ui적인 내용");
        }

    }

    //저장, 로드(ToJson)
    public void SaveGame()
    {
        //저장용 껍데기
        UserSaveData saveData = new UserSaveData();

        //내용물만 꺼내서 리스트에 담기
        saveData.unlockedCharacters = this.unlockedCharList;

        //성장 정보 포장
        //딕셔너리는 저장불가, 캐릭터 ID / 학년을 꺼내서 CharacterGradeSave로 만들어 넣기
        foreach (var kvp in charGradeDict)
        {
            saveData.characterGrades.Add(new CharacterGradeSave { 
                charID = kvp.Key, 
                gradeLevel = kvp.Value 
            });
        }
        //껍데기에 담아둔 내용을 전부 JSON으로 변경하고 저장소에 NOY_USER_DATA 라는 이름으로 기억하기
        PlayerPrefs.SetString(SAVE_KEY, JsonUtility.ToJson(saveData));
        //저장 확정
        PlayerPrefs.Save();
    }


    //저장된 텍스트를 불러와서 다시 딕셔너리와 해쉬셋으로 정리
    public void LoadGame()
    {
        //세이브 파일 있는지 체크
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            //저장된 텍스트 불러오기
            string json = PlayerPrefs.GetString(SAVE_KEY);

            //역직렬화, 다시 객체로 변환
            var saveData = JsonUtility.FromJson<UserSaveData>(json);

            //담기
            this.unlockedCharList = saveData.unlockedCharacters;

            //List에 있는 데이터를 딕셔너리로 옮겨담기
            charGradeDict.Clear();
            foreach (var gd in saveData.characterGrades)
            {
                //ID, 레벨을 다시 등록
                charGradeDict[gd.charID] = gd.gradeLevel;
            }
        }
        else
        {
            //초기 데이터 세팅, 기본 캐릭터 해금 정도
            UnlockCharacter("10001");
            UnlockCharacter("10002");
            UnlockCharacter("10003");
        }
    }

}