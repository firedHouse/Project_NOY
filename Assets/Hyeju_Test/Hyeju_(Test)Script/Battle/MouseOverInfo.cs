using UnityEngine;
using UnityEngine.UI;

public class MouseOverInfo : MonoBehaviour
{
    [SerializeField] private GameObject _speedAttackInfoPanel;
    [SerializeField] private Text _skillInfo;
    [SerializeField] private CharacterSkill _characterSkill;
    [SerializeField] private SkillData[] _useSkillList = new SkillData[3];

    private void Start()
    {
        _useSkillList[0] = _characterSkill.TestSkill1;
    }

    public void OnCharacterOver()
    {
        _speedAttackInfoPanel.SetActive(true);
    }

    public void OnCharacterExit()
    {
        _speedAttackInfoPanel.SetActive(false);
    }

    public void OnFirSkillInfo()
    {
        _skillInfo.text = $"{_useSkillList[0].skillName}";
    }

    public void OnSkillExit()
    {
        _skillInfo.text = $"{_useSkillList[0].skillName}";
    }
    public void OnSecSkillInfo()
    {
        _skillInfo.text = $"2번 스킬";
    }

    public void OnThirSkillInfo()
    {
        _skillInfo.text = $"3번 스킬";
    }

}
