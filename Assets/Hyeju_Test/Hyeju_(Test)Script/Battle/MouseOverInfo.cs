using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class MouseOverInfo : MonoBehaviour
{
    //스킬 정보표시할 텍스트
    [SerializeField] private Text _skillInfo;
    //받아온 스킬 데이터 스크립트
    private CharacterData _characterData;


    //스킬 정보 받아오기 
    private void Start()
    {
        //캐릭터id에 해당하는 스킬을 불러와야 함.

    }

    //공통 : 해당 캐릭터의 첫번째 스킬이 기본적으로 출력된다.
    public void OnSkillExit()
    {
        _skillInfo.text = $"{_characterData.ownedSkill01}";
    }

    //1번스킬 출력
    public void OnFirSkillInfo()
    {
        _skillInfo.text = $"{_characterData.ownedSkill01}";
    }

    //2번스킬 출력
    public void OnSecSkillInfo()
    {
        _skillInfo.text = $"{_characterData.ownedSkill02}";
    }

    //3번스킬 출력
    public void OnThirSkillInfo()
    {
        _skillInfo.text = $"{_characterData.ownedSkill03}";
    }

}
