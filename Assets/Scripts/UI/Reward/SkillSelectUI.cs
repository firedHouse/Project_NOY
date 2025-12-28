using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectUI : MonoBehaviour
{
    [Header("캐릭터 이미지")]
    [SerializeField] private Image selectedCharacterImage;

    [Header("스킬 버튼들")]
    [SerializeField] private Button[] skillButtons;
    [SerializeField] private Image[] skillImage;
    [SerializeField] private Text[] ppText;

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

        // selectedCharacterImage.sprite = Resources.Load<Sprite>(this.owner.이미지);

        RefreshSkillButtons();


        CancelButton.onClick.RemoveAllListeners();
        CancelButton.onClick.AddListener(Close);
    }

    private void RefreshSkillButtons()
    {
        var skills = owner.Skills;

        for (int i = 0; i < skillButtons.Length; i++)
        {
            if (i >= skills.Count || !skills[i].IsValid())
            {
                skillButtons[i].gameObject.SetActive(false);
                continue;
            }


            var skill = skills[i];
            skillButtons[i].gameObject.SetActive(true);

            skillImage[i].sprite = Resources.Load<Sprite>(skill.Data.skillIcon);

            ppText[i].text = $"PP{skill.CurrentPP} / {skill.Data.skillPP}";

            bool canUse = skill.CurrentPP <= skill.Data.skillPP;
            skillButtons[i].interactable = canUse;

            int index = i;
            skillButtons[i].onClick.RemoveAllListeners();
            skillButtons[i].onClick.AddListener(() =>
            {
                OnSelectSkill(skills[index]);
            });
        }
    }

    public void OnSelectSkill(Skill skill)
    {
        onSelect?.Invoke(owner, skill);
        Close();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        onCancel?.Invoke();
    }

}
