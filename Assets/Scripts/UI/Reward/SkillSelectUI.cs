using System;
using UnityEngine;

public class SkillSelectUI : MonoBehaviour
{
    private Action<BattleUnit, Skill> onSelect;
    private BattleUnit owner;

    public void Open(BattleUnit owner, Action<BattleUnit, Skill> onSelect)
    {
        this.owner = owner;
        this.onSelect = onSelect;
        //UI 표시 로직 추가
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SelectSkill(Skill skill)
    {
        onSelect?.Invoke(owner, skill);
        Close();
    }
}
