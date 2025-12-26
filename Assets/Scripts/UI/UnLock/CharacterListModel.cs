using System;
using UnityEngine;

// 캐릭터 슬롯과 상세 정보 UI에 사용할 모델
// 필요한 정보: 아이디, 해금 여부, 이름, 코드네임, 포지션, 속성, 레벨(학년), 대사, 세부 설정, 스킨, 
// 팀구성 팝업만 사용할 정보 : 스킬(팀구성), 
//[Serializable]
public partial class CharacterListModel : MonoBehaviour
{

    #region Field
    [SerializeField] private string characterID;
    [SerializeField] private bool isUnlocked;
    [SerializeField] private int level;
    [SerializeField] private int position;
    [SerializeField] private int element;
    [SerializeField] private string characterName;
    [SerializeField] private string characterCodeName;
    [SerializeField] private string characterDialogue;
    [SerializeField] private string characterInfo;
    [SerializeField] private string characterSkin;

    #region GrowthModel에서 사용할 부분 
    [SerializeField] private float attackLevel;
    [SerializeField] private float hpLevel;

    [SerializeField] private int unlockShilling = 2000;
    #endregion

    private string ownedSkill02;
    private string ownedSkill01;
    private string ownedSkill03;
    #endregion

    #region Property 
    public string CharacterID => characterID;
    public bool IsUnlocked { get => isUnlocked; private set => isUnlocked = value; }
    public int Level => level; 
    public int Position => position;
    public int Element => element;
    public string CharacterName => characterName; 
    public string CharacterCodeName => characterCodeName; 
    public string CharacterDialogue => characterDialogue; 
    public string CharacterInfo => characterInfo; 
    public string CharacterSkin => characterSkin;

    #region GrowthModel에서 사용할 부분 
    public float AttackLevel { get => attackLevel; set => attackLevel = value; }
    public float HpLevel { get => hpLevel; set => hpLevel = value; }
    public int UnlockShilling { get => unlockShilling; private set => unlockShilling = value; }
    #endregion

    #endregion

    // 초기화
    public void Initialize(string id, CharacterData characterData)
    {
        // CharacterData characterData = TableManager.Instance.CharacterTable.Get(id);
        Debug.Log($"[CharacterListModel] {characterData.unlock}");

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

        presenter = GameObject.Find("CharacterListPanel").GetComponent<CharacterListPresenter>();
        if(presenter == null)
        {
            Debug.Log("[CharacterListModel] 프레젠터없음");

            return;
        }

        presenter.GetGradeData();
        // 스킬 로드 (BattelUnit.LoadSkills 메서드 사용 예정)> 팀 구성에서 이 클래스 사용하게 되면 추가
    }

    // 스킬 로드 메서드


    // 해금 처리 
    public void Unlock()
    {
        if (!isUnlocked)
        {
            Debug.Log($"[CharacterListModel] {isUnlocked} > 해금");
            Debug.Log($"[CharacterListModel] 필요한 실링 : ");
            // to-do : 실링 빼가는 처리 추가
            isUnlocked = true;
            OnUnlock.Invoke(this);
        }
        else
        {
            Debug.Log($"[CharacterListModel] {isUnlocked} > 이미 해금됨!");
        }
    }


}
