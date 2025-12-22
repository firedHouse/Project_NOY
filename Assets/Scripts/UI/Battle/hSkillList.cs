using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//model
public class hSkillList : MonoBehaviour
{
    private BattleUnit _battleUnit;
    public List<Character> playerTeam;

    public Skill[] skillUI;
    public List<SkillSlot> slots; // 스킬 슬롯 리스트

    public BattleUnit BattleUnit { get => _battleUnit; set => _battleUnit = value; }

    private void Awake()
    {
        skillUI = new Skill[3];

        //_battleUnit.DataLoaded += SkillDataLoad;
    }
    //가져와야 하는 스킬 : 현재 턴 캐릭터의 스킬, 

    public void SkillDataLoad(UnityAction<Skill> onSkillClicked)
    {
        List<Skill> currentSkills = _battleUnit.Skills;
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < currentSkills.Count)
            {
                //로그 체크용, 누구의 무슨 스킬인지
                skillUI[i] = currentSkills[i];
                Debug.Log($"{_battleUnit.name}의 스킬 {i}: {skillUI[i].Data.skillName}");
                slots[i].Setup(skillUI[i], onSkillClicked);
            }

        }
    }
}