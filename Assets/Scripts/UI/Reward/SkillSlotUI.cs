using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SkillSlotUI : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Button button;
    [SerializeField] private Image icon;
    [SerializeField] private Text ppText;

    private Skill skill;
    private Action<Skill> onClick;
    private Action<Skill> onHover;
    private Action onExit;

    public void Set(
        Skill skill,
        Action<Skill> onClick,
        Action<Skill> onHover,
        Action onExit)
    {
        this.skill = skill;
        this.onClick = onClick;
        this.onHover = onHover;
        this.onExit = onExit;

        icon.sprite = ResourceManager.Instance.LoadSprite(skill.Data.skillIcon);
        ppText.text = $"PP {skill.CurrentPP}/{skill.Data.skillPP}";
        button.interactable = skill.CurrentPP > 0;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => this.onClick?.Invoke(skill));

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onHover?.Invoke(skill);
    }

}