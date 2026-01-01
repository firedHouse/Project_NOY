using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TeamOrganizationSkillList : MonoBehaviour
{
    // private BattleUnit _battleUnit;
    [SerializeField] private CharacterListModel model;

    public Skill[] skillUI;
    public List<SkillSlot> slots; // 스킬 슬롯 리스트

    // public BattleUnit BattleUnit { get => _battleUnit; set => _battleUnit = value; }

    public CharacterListModel Model
    {
        get => model;
        set => model = value;
    } 
    private void Awake()
    {
        skillUI = new Skill[3];
        slots = gameObject.GetComponentsInChildren<SkillSlot>().ToList();
        // SetSkillSlotAvailable(false);
    }

    
    // 스킬 슬롯에 모델의 스킬들 넣어줌
    public void UpdateSkillView(CharacterListModel character)
    {
        // 스킬들 받아옴
        List<Skill> currentSkills = character.Skills;

        for (int i = 0; i < currentSkills.Count; i++)
        {
            slots[i].MySkill = currentSkills[i];
            slots[i].SKillResource(currentSkills[i]);
            skillUI[i] = currentSkills[i];
            Debug.Log($"[TeamOrganizationSkillList] {slots[i].MySkill.Data.skillName}");        
        }
    }
    
    public void SkillDataLoad(UnityAction<Skill> onSkillClicked)
    {
        List<Skill> currentSkills = model.Skills;

        // //가장 강한 스킬 찾기(PP낮은놈)
        // int minMaxPP = int.MaxValue;
        // if (currentSkills.Count > 0)
        // {
        //     foreach (Skill skill in currentSkills)
        //     {
        //         if (skill.Data.skillPP < minMaxPP)
        //         {
        //             minMaxPP = skill.Data.skillPP;
        //         }
        //     }
        // }
        // else
        // {
        //     minMaxPP = 0;
        // }
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < currentSkills.Count)
            {
                //로그 체크용, 누구의 무슨 스킬인지
                skillUI[i] = currentSkills[i];
                Debug.Log($"{model.name}의 스킬 {i}: {skillUI[i].Data.skillName}");

                //셋업 실행 전에 대사부터 출력
                slots[i].Setup(skillUI[i], (clickedSkill) =>
                {
                    //클릭 시 대사 데이터 확인 및 출력
                    // CheckDialogue(clickedSkill, minMaxPP);

                    //다시 기능 실행
                    onSkillClicked?.Invoke(clickedSkill);

                });
            }
        }
    }

    // private void CheckDialogue(Skill skill, int minPP)
    // {
    //     //클릭한 스킬의 MaxPP가 가장 작은 지
    //     if (skill.Data.skillPP == minPP)
    //     {
    //         //BattleUnit에서 UnitID 프로퍼티 사용
    //         string id = model.CharacterID;
    //
    //         //CharacterData에서 아이디 가져오기
    //         CharacterData charData = TableManager.Instance.CharacterTable.Get(id);
    //
    //         //대사가 존재하면 로그 출력
    //         if (charData != null && !string.IsNullOrEmpty(charData.characterDialogue))
    //         {
    //             string logMessage = $"{model.CharacterName}: {charData.characterDialogue}";
    //             BattleLogManager.Instance.AddLog(logMessage);
    //         }
    //     }
    // }

    // 스킬 버튼 활성화 여부 변경
    public void SetSkillSlotAvailable(bool isAvailable)
    {
        foreach (SkillSlot slot in slots)
        {
            slot.ChangeButtonAvailable(isAvailable);
        }
    }
}