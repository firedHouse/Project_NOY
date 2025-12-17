using UnityEngine;
using UnityEngine.UI;

//model
public class SkillList : MonoBehaviour
{
    [SerializeField] private CharacterBattleInfoModel _characterBattleInfoModel;

    public SkillData[] _characterSkill;
    public SkillData[] _monsterSkill;

    private void Awake()
    {
        _characterSkill = new SkillData[3];
        _monsterSkill = new SkillData[3];
    }

    void Start()
    {
        CharacterSkill();
        MonsterSkill();
    }

    public void CharacterSkill()
    {
        //스킬 정보 받아오기
        _characterSkill[0] = TableManager.Instance.SkillTable.Get($"{_characterBattleInfoModel.character.ownedSkill01}");
        _characterSkill[1] = TableManager.Instance.SkillTable.Get($"{_characterBattleInfoModel.character.ownedSkill02}");
        _characterSkill[2] = TableManager.Instance.SkillTable.Get($"{_characterBattleInfoModel.character.ownedSkill03}");

        if (_characterSkill != null)
        {
            Debug.Log($"[MouseOverInfo] : {_characterSkill[0].skillName}");
            Debug.Log($"[MouseOverInfo] : {_characterSkill[1].skillName}");
            Debug.Log($"[MouseOverInfo] : {_characterSkill[2].skillName}");
        }
    }

    public void MonsterSkill()
    { 
        //몬스터 모델에 맞춰서 작성 - UI에 표시 안되는데 여기서 해야할까? 싶은 생각.
    }
}
