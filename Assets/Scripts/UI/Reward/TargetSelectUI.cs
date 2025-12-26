using System;
using UnityEngine;

public class TargetSelectUI : MonoBehaviour
{   // 아이템을 사용할 타겟과 스킬을 선택하는 UI
    // 스킬을 선택할 UI는 작성해야함.
    [SerializeField] SkillSelectUI skillSelectUI;
    private object currentItem;
    private Action<BattleUnit, Skill> onTargetSelected;

    public void Open(object item, Action<BattleUnit, Skill> onSelected)
    {
        currentItem = item;
        onTargetSelected = onSelected;
        gameObject.SetActive(true);
        // 타겟 선택 UI 표시 로직 추가
    }

    public void SelectUnit(BattleUnit target)
    {
        if (currentItem is ItemData item && item.useItemType == UsableItemType.PPPotion)
        {
            skillSelectUI.Open(target, (unit, skill) =>
            {
                Confirm(unit, skill);
            });
            return;
        }

        Confirm(target, null);
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

}
