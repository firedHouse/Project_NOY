using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectUI : MonoBehaviour
{
    [Header("캐릭터 이미지")]
    [SerializeField] private Image selectedCharacterImage;

    [Header("스킬 버튼들")]
    [SerializeField] private SkillSlotUI[] skillSlots;

    [Header("스킬 설명들")]
    [SerializeField] private Text skillNameText;
    [SerializeField] private Text skillDiscriptText;

    [SerializeField] private Button CancelButton;

    private Action<BattleUnit, Skill> onSelect;
    private BattleUnit owner;
    private Action onCancel;

    public void Open(BattleUnit owner, Action<BattleUnit, Skill> onSelect, Action cancel)
    {
        this.owner = owner;
        this.onSelect = onSelect;
        onCancel = cancel;
        //UI 표시 로직 추가
        gameObject.SetActive(true);
        //선택된 캐릭터 이미지

        ApplyCharacterImage();
        RefreshSlots();

        CancelButton.onClick.RemoveAllListeners();
        CancelButton.onClick.AddListener(Close);
        
        ClearDescription();
    }

    private void ApplyCharacterImage()
    {
        if (owner is Character character)
            selectedCharacterImage.sprite = character.currentSkinSprite;
        else
            selectedCharacterImage.sprite = null;
    }

    private void RefreshSlots()
    {
        var skills = owner?.Skills;
        if (skills == null) return;

        for (int i = 0; i < skillSlots.Length; i++)
        {
            if (i >= skills.Count || !skills[i].IsValid())
            {
                skillSlots[i].Hide();
                continue;
            }

            skillSlots[i].Set(
                skills[i],
                OnSelectSkill,
                ShowSkillDescription,
                ClearDescription
            );
        }
    }

    private void ShowSkillDescription(Skill skill)
    {
        if (skill == null) return;

        skillNameText.text = skill.Data.skillName;
        skillDiscriptText.text = skill.Data.skillTooltip;
    }

    private void ClearDescription()
    {
        skillNameText.text = "";
        skillDiscriptText.text = "스킬에 마우스를 올리면 설명이 나타납니다.";
    }


    public void OnSelectSkill(Skill skill)
    {
        onSelect?.Invoke(owner, skill);
        Exit();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        onCancel?.Invoke();
    }
    private void Exit()
    {
        gameObject.SetActive(false);
    }

}
