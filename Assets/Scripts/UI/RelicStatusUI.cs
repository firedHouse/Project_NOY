using UnityEngine;
using UnityEngine.UI;

public class RelicStatusUI : MonoBehaviour
{
    [SerializeField] private Image relicIcon;

    public void Refresh(Character character)
    {
        if (character == null)
        {
            Clear();
            return;
        }

        var relicComp = character.GetComponent<RelicComponent>();
        if (relicComp == null || relicComp.CurrentRelic == null)
        {
            Clear();
            return;
        }

        var relic = relicComp.CurrentRelic;

        relicIcon.sprite = ResourceManager.Instance.LoadSprite(relic.itemData.itemEquipImage);
        relicIcon.gameObject.SetActive(true);
    }

    private void Clear()
    {
        relicIcon.sprite = null;
    }

}
