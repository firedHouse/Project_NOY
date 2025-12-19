using System;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;


//model
public class SkillList : MonoBehaviour
{
    [SerializeField] private CharacterBattleInfoModel _model;
    private CharacterData _characterData;

    public SkillData[] _characterSkill;
    public SkillData[] _monsterSkill;


    private void Awake()
    {
        _characterSkill = new SkillData[3];
        _monsterSkill = new SkillData[3];

        _model.DataLoaded += SkillDataLoad;
    }

    public void SkillDataLoad()
    {
        _characterData = _model.character;
        if(_characterData == null)
        {
            Debug.Log($"[MouseOverInfo] : 캐릭터 데이터 없음");
        }
        else if (_characterData != null)
        {
            //스킬 정보 받아오기
            _characterSkill[0] = TableManager.Instance.SkillTable.Get($"{_characterData.ownedSkill01}");
            _characterSkill[1] = TableManager.Instance.SkillTable.Get($"{_characterData.ownedSkill02}");
            _characterSkill[2] = TableManager.Instance.SkillTable.Get($"{_characterData.ownedSkill03}");

            Debug.Log($"[MouseOverInfo] :{_characterSkill[0].skillName}");
        }

       
    }

    public void MonsterSkill()
    { 
        //몬스터 모델에 맞춰서 작성 - UI에 표시 안되는데 여기서 해야할까? 싶은 생각.
    }
}
