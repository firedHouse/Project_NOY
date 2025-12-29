using UnityEngine;
using UnityEngine.UI;

public class SpendGoldEnoughUI : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake()
    {
        gameObject.SetActive(false);

        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(Close);
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
