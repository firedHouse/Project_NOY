using UnityEngine;

public class CharacterSkill : MonoBehaviour
{
    private SkillData _testSkill1;

    public SkillData TestSkill1 { get { return _testSkill1; } set { _testSkill1 = value; } }

    void Start()
    {
        _testSkill1 = TableManager.Instance.SkillTable.Get("skill_00001");


        if (_testSkill1 != null)
        {
           
            Debug.Log($"[테스트스크립트] 스킬이름: {_testSkill1.skillName}, ");
        }
        else
        {
            Debug.LogError("테스트실패");
        }
    }
}
