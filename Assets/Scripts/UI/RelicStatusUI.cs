using UnityEngine;
using UnityEngine.UI;

public class RelicStatusUI : MonoBehaviour
{
    [SerializeField] private Image relicIcon;

    public void Refresh(RunTimeRelic relic)
    {
        if (relic == null)
        {
            Clear();
            return;
        }


        relicIcon.sprite = ResourceManager.Instance.LoadSprite(relic.itemData.itemEquipImage);
        relicIcon.gameObject.SetActive(true);
    }

    private void Clear()
    {
        relicIcon.sprite = null;
        relicIcon.gameObject.SetActive(false);
    }

}
