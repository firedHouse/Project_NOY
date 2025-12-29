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

            characterButtons[i].gameObject.SetActive(false);
            characterButtons[i].onClick.RemoveAllListeners();
        }

        for (int i = 0; i < HPSlider.Length; i++)
        {
            HPSlider[i].gameObject.SetActive(false);
            HPSlider[i].interactable = false;

            var img = HPSlider[i].GetComponent<Image>();
            if(img != null)
            { img.raycastTarget = false; }
        }

        for (int i = 0; i < team.Count; i++)
        {
            if (i >= characterButtons.Length || i >= HPSlider.Length)
            {
                break;
            }

            var character = team[i];

            characterButtons[i].gameObject.SetActive(true);
            HPSlider[i].gameObject.SetActive(true);

            float currentHP = character.CurrentHP;
            float maxHP = character.MaxHP;

            HPSlider[i].maxValue = maxHP;
            HPSlider[i].value = currentHP;

            // 캐릭터 이미지 입히기
            CharacterData data = TableManager.Instance.CharacterTable.Get(character.UnitID);
            if(data != null )
            {
                characterImage[i].sprite = ResourceManager.Instance.LoadSprite(data.characterSkin);
            }

            int index = i;
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
