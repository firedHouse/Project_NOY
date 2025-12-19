using System;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;


//model
public class SkillList : MonoBehaviour
{
    [SerializeField] private BattleUnit _battleUnit;

    public Skill[] skillUI;


    private void Awake()
    {
        skillUI = new Skill[3];

        //_battleUnit.DataLoaded += SkillDataLoad;
    }

    private void Start()
    {
        SkillDataLoad();
    }

    //가져와야 하는 스킬 : 현재 턴 캐릭터의 스킬, 

    public void SkillDataLoad()
    {
        if (_battleUnit.Skills.Count == 0)
        {
            Debug.Log($"[MouseOverInfo] : 스킬 없음");
        }
        else if (_battleUnit.Skills.Count > 0)
        {
            //스킬 정보 받아오기
            for (int i = 0; i < 3; i++)
            {
                skillUI[i] = _battleUnit.Skills[i];
                Debug.Log($"[MouseOverInfo] :{skillUI[i].Data.skillName}");
            }

        }
    }
}
