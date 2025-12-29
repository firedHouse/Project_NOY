using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 캐릭터 데이터와 스킬 데이터를 함께 처리하기 위한 클래스
/// </summary>
public class CharacterModelBase : MonoBehaviour
{
    [SerializeField] protected string characterID;
    [SerializeField] protected bool isUnlocked;
    [SerializeField] protected int level;
    [SerializeField] private int position;
    [SerializeField] private int element;
    [SerializeField] private string characterName;
    [SerializeField] private string characterCodeName;
    [SerializeField] private string characterDialogue;
    [SerializeField] private string characterInfo;
    [SerializeField] private string characterSkin;
    [SerializeField] protected float attackLevel;
    [SerializeField] protected float hpLevel;
    
    private string ownedSkill02;
    private string ownedSkill01;
    private string ownedSkill03;
    
    protected string gradeID;
    protected int gradeIDNum;
    
    public Dictionary<int, (int, int)> gradeData = new Dictionary<int, (int, int)>();

    public string CharacterID => characterID;
    public bool IsUnlocked { get => UserDataManager.Instance.IsCharacterUnlocked(characterID);
    }
    public int Level => level;
    public int Position => position;
    public int Element => element;
    public string CharacterName => characterName;
    public string CharacterCodeName => characterCodeName;
    public string CharacterDialogue => characterDialogue;
    public string CharacterInfo => characterInfo;
    public string CharacterSkin => characterSkin;
    public float AttackLevel { get => attackLevel; set => attackLevel = value; }
    public float HpLevel { get => hpLevel; set => hpLevel = value; }
    
    public void Initialize(string id, CharacterData characterData)
    {
        // CharacterData characterData = TableManager.Instance.CharacterTable.Get(id);
        // Debug.Log($"[CharacterListModel] {characterData.unlock}");

        if(characterData is not null)
        {
            characterID = characterData.characterID;
            isUnlocked = characterData.unlock;
            level = characterData.level;
            position = characterData.position;
            element = characterData.elementUI;
            characterName = characterData.characterName;
            characterCodeName = characterData.characterCodeName;
            characterDialogue = characterData.characterDialogue;
            characterInfo = characterData.characterInfo;
            characterSkin = characterData.characterSkin;
            attackLevel = characterData.attackLevel1;
            hpLevel = characterData.HPLevel1;
        }
        else
        {
            Debug.Log("캐릭터 데이터 불러오기 실패");
        }

        GetGradeData();
        // 스킬 로드 (BattelUnit.LoadSkills 메서드 사용 예정)> 팀 구성에서 이 클래스 사용하게 되면 추가
    }

    // growthPresenter에서 모델 베이스 클래스로 이동
    private void GetGradeData()
    {
        if (gradeData.Count == 0)
        {
            
            gradeIDNum = 50001;
            // int characterIDNum = 10001; // 필드 사용하도록 수정
            int characterIDNum = int.Parse(characterID);
            for (int i = 0; i < 9; i++)
            {
                gradeData.Add(characterIDNum, (gradeIDNum, gradeIDNum + 1));
                // Debug.Log($"[CharacterListModel] 아이디 입력 체크 : {gradeData[characterIDNum].Item1}");
                gradeIDNum += 2;
                characterIDNum += 1;
            }
            // 딕셔너리 10001 캐릭터는 50001, 50002의 의 레벨업 정보를 가지고 있다. 정도의 내용
        }
    }

    protected void SetIsUnlocked()
    {
        isUnlocked = true;
        UserDataManager.Instance.UnlockCharacter(characterID);
    }
}