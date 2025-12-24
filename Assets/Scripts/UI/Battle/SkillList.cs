using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//model
public class SkillList : MonoBehaviour
{
    private BattleUnit _battleUnit;
    public List<Character> playerTeam;

    public Skill[] skillUI;
    public List<SkillSlot> slots; // 스킬 슬롯 리스트

    public BattleUnit BattleUnit { get => _battleUnit; set => _battleUnit = value; }

    private void Awake()
    {
        //skillUI = new Skill[3];

        //_battleUnit.DataLoaded += SkillDataLoad;
    }

    private void Start()
    {
        //SkillDataLoad();
        playerTeam = BattleManager.Instance.PlayerTeam;
    }

    //가져와야 하는 스킬 : 현재 턴 캐릭터의 스킬, 

    public void SkillDataLoad(UnityAction<Skill> onSkillClicked)
    {
        for(int j = 0; j < 3; j++)
        {
            if (playerTeam[j].Skills.Count == 0)
            {
                Debug.Log($"[MouseOverInfo] : 스킬 없음");
            }
            else if (playerTeam[j].Skills.Count > 0)
            {
                //스킬 정보 받아오기
                for (int i = 0; i < 3; i++)
                {
                    skillUI[i] = playerTeam[j].Skills[i];
                    Debug.Log($"[MouseOverInfo] :{skillUI[i].Data.skillName}");
                    slots[i].Setup(skillUI[i], onSkillClicked);
                    //slots[i].Setup(playerTeam[j].Skills[i], onSkillClicked);
                }

            }

        }
    }
}
