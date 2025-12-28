using System;
using UnityEngine;
using UnityEngine.UI;

public class TargetSelectUI : MonoBehaviour
{   // 아이템을 사용할 타겟과 스킬을 선택하는 UI
    // 스킬을 선택할 UI는 작성해야함.
    [Header("Button")]
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button[] characterButtons;
    [SerializeField] private Image[] characterImage;

    [Header("HPSlider")]
    [SerializeField] private Slider[] HPSlider;

    [SerializeField] private SkillSelectUI skillSelectUI;

    private object currentItem;
    private Action<BattleUnit, Skill> onTargetSelected;
    private Action onCancel;

    // 타겟 선택 창 오픈
    public void Open(object item, Action<BattleUnit, Skill> onSelected, Action Cancel)
    {
        currentItem = item;
        onTargetSelected = onSelected;
        onCancel = Cancel;

        gameObject.SetActive(true);
        // 타겟 선택 UI 표시 로직 추가

        RefreshCharacterButton();

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(Close);
    }

    private void RefreshCharacterButton()
    {
        var team = BattleManager.Instance.PlayerTeam;

        for (int i = 0; i < characterButtons.Length; i++)
        {
            if (i >= team.Count)
            {
                characterButtons[i].gameObject.SetActive(false);
                HPSlider[i].gameObject.SetActive(false);
                continue;
            }

            var character = team[i];
            characterButtons[i].gameObject.SetActive(true);
            HPSlider[i].gameObject.SetActive(true);

            float currentHP = character.CurrentHP;
            float maxHP = character.MaxHP;

            HPSlider[i].maxValue = maxHP;
            HPSlider[i].value = currentHP;

            // 캐릭터 이미지 입히기
            // characterImage[i].sprite = Resources.Load<Sprite>(path: CharacterData.characterSkin);

            int index = i;
            characterButtons[i].onClick.RemoveAllListeners();
            characterButtons[i].onClick.AddListener(() => { OnCharacterSelected(team[index]); });
        }
    }

    private void OnCharacterSelected(Character character)
    {
        if (currentItem is RunTimeItem item && item.useType == UsableItemType.PPPotion)
        {
            skillSelectUI.Open(character, Confirm, ReOpen);
            gameObject.SetActive(false);
            return;
        }
        Confirm(character, null);
    }

    private void Confirm(BattleUnit target, Skill skill)
    {
        onTargetSelected?.Invoke(target, skill);
        gameObject.SetActive(false);
    }

    public void ReOpen()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        onCancel?.Invoke();
    }

}
