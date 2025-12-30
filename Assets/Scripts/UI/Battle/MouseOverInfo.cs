using UnityEngine;
using UnityEngine.UI;

//view
public partial class MouseOverInfo : MonoBehaviour
{
    [Header("텍스트")]
    [SerializeField] private Text _skillInfo;
    [SerializeField] private Text _skillName;

    [Header("hSkillList")]
    [SerializeField] private hSkillList _skillList;

    [Header("firstCharacterModel")]
    [SerializeField] private Character firstCharacterModel;

    [Header("PP")]
    [SerializeField] private Text[] PPText = new Text[3];
    
    //스킬 이미지 : 스킬정보가 바뀌면 이미지도 바뀌어야 함.
    //스킬 이미지 이름 > 스킬 리스트에서 받아와야 함.
    private Image _ImageInfo;

    #region 마우스오버
    //1번스킬 출력
    public void OnFirSkillInfo()
    {
        _skillInfo.text = $"{_skillList.skillUI[0]?.Data.skillTooltip}";
        _skillName.text = $"{_skillList.skillUI[0]?.Data.skillName}";
    }

    public void OnFirSkillExit()
    {
        _skillInfo.text = $"{_skillList.skillUI[0]?.Data.skillTooltip}";
        _skillName.text = $"{_skillList.skillUI[0]?.Data.skillName}";
    }


    //2번스킬 출력
    public void OnSecSkillInfo()
    {
        _skillInfo.text = $"{_skillList.skillUI[1]?.Data.skillTooltip}";
        _skillName.text = $"{_skillList.skillUI[1]?.Data.skillName}";
    }

    public void OnSecSkillExit()
    {
        _skillInfo.text = $"{_skillList.skillUI[1]?.Data.skillTooltip}";
        _skillName.text = $"{_skillList.skillUI[1]?.Data.skillName}";
    }

    //3번스킬 출력
    public void OnThirSkillInfo()
    {
        _skillInfo.text = $"{_skillList.skillUI[2]?.Data.skillTooltip}";
        _skillName.text = $"{_skillList.skillUI[2]?.Data.skillName}";
    }

    public void OnThirSkillExit()
    {
        _skillInfo.text = $"{_skillList.skillUI[2]?.Data.skillTooltip}";
        _skillName.text = $"{_skillList.skillUI[2]?.Data.skillName}";
    }

    #endregion

    public void PPInfo (Character currentCharacter)
    {
        for (int i = 0; i < 3; i++)
        {
            int maxPP = currentCharacter.Skills[i].Data.skillPP;
            PPText[i].text = $"{currentCharacter.Skills[i].CurrentPP} / {maxPP}";
            Debug.Log($"-------스킬이름 {currentCharacter.Skills[i].Data.skillName}");
        }
    }
}

// Jihoo 작업 부분
// 캐릭터 행동 선택 (Turn) 부분
public partial class MouseOverInfo : MonoBehaviour
{
    [SerializeField] private Text _turnInfo;
    public Skill skill;


    public void UpdateTurnInfo(int order)
    {
        Debug.Log($"[MouseOverInfo] ({order + 1}번 캐릭터) 행동 선택");
        _turnInfo.text = $"({order + 1}번 캐릭터) 행동 선택";
    }

    public void UpdateTurnChanged()
    {
        Debug.Log("[MouseOverInfo] 행동선택완료");
        _turnInfo.text = "행동 선택 완료";
    }
}