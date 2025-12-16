using UnityEngine;
using UnityEngine.UI;

public class GoldInfo : MonoBehaviour
{
    [SerializeField] private Text _goldInfoText;
    [SerializeField] private GoldModelScript _goldModelScript;

    private void Start()
    {
        _goldInfoText.text = $"{_goldModelScript.Gold}";
    }
    public void UpdateGold(int gold)
    {
        _goldInfoText.text = $"{gold}";
    }
}
