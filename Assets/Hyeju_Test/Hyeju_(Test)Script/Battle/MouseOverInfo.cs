using UnityEngine;
using UnityEngine.UI;

//view
public class MouseOverInfo : MonoBehaviour
{
    //스킬 정보표시할 텍스트
    [SerializeField] private Text _skillInfo;
    [SerializeField] private SkillList _skillList;

    //스킬 이미지 : 스킬정보가 바뀌면 이미지도 바뀌어야 함.
    //스킬 이미지 이름 > 스킬 리스트에서 받아와야 함.
    private Image _ImageInfo;


    #region 마우스오버
    //1번스킬 출력
    public void OnFirSkillInfo()
    {
        _skillInfo.text = $"{_skillList._characterSkill[0]?.skillName}";
    }

    public void OnFirSkillExit()
    {
        _skillInfo.text = $"{_skillList.skillUI[0]?.Data.skillName}";
    }

    public void OnFirSkillExit()
    {
        _skillInfo.text = $"{_skillList.skillUI[0]?.Data.skillName}";
    }

    //2번스킬 출력
    public void OnSecSkillInfo()
    {
        _skillInfo.text = $"{_skillList.skillUI[1]?.Data.skillName}";
    }

    public void OnSecSkillExit()
    {
        _skillInfo.text = $"{_skillList.skillUI[1]?.Data.skillName}";
    }

    public void OnSecSkillExit()
    {
        _skillInfo.text = $"{_skillList._characterSkill[1]?.skillName}";
    }

    //3번스킬 출력
    public void OnThirSkillInfo()
    {
        _skillInfo.text = $"{_skillList.skillUI[2]?.Data.skillName}";
    }

    public void OnThirSkillExit()
    {
        _skillInfo.text = $"{_skillList.skillUI[2]?.Data.skillName}";
    }

    public void OnThirSkillExit()
    {
        _skillInfo.text = $"{_skillList._characterSkill[2]?.skillName}";
    }
    #endregion
}
