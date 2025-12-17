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


    //캐릭터 순서 변화 감지 시 스킬 이미지 변경
    //character 정보가 변경되었을 때 이미지도 바뀌어야 함.
    //캐릭터별 턴이 넘어가려면 : 전열 스킬 선택 > 중열 스킬 선택 > 후열스킬 선택

    // >> 스킬 클릭 시 <<
    //혹은
    // >> 스킬 순서를 저장하는 스택이 null 이 아닐때. <<
    //혹은
    // >> 그냥 턴 수를 int로 저장 <<

    #region 마우스오버
    //공통 : 해당 캐릭터의 첫번째 스킬이 기본적으로 출력된다.
    public void OnSkillExit()
    {
        _skillInfo.text = $"{_skillList._characterSkill[0]?.skillName}";
    }

    //1번스킬 출력
    public void OnFirSkillInfo()
    {
        _skillInfo.text = $"{_skillList._characterSkill[0]?.skillName}";
    }

    //2번스킬 출력
    public void OnSecSkillInfo()
    {
        _skillInfo.text = $"{_skillList._characterSkill[1]?.skillName}";
    }

    //3번스킬 출력
    public void OnThirSkillInfo()
    {
        _skillInfo.text = $"{_skillList._characterSkill[2]?.skillName}";
    }
    #endregion
}
