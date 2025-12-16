using UnityEngine;
using UnityEngine.UI;

public class MouseOverInfo : MonoBehaviour
{
    [SerializeField] private GameObject _speedAttackInfoPanel;
    [SerializeField] private Text _skillInfo;


    public void OnCharacterOver()
    {
        _speedAttackInfoPanel.SetActive(true);
    }

    public void OnCharacterExit()
    {
        _speedAttackInfoPanel.SetActive(false);
    }

    public void OnSkillOver()
    {
        _skillInfo.text = "마우스 올라감";
    }

    public void OnSkillExit()
    {
        _skillInfo.text = "기본적으로 1번 스킬 내용 출력";
    }
}
