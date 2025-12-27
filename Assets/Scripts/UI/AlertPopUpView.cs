using UnityEngine;
using UnityEngine.UI;

// 팝업 띄우고 관리하기 위한 view
public class AlertPopUpView : MonoBehaviour
{
    [Header("띄워줄 팝업")]
    [SerializeField] private GameObject popUp;

    [SerializeField] private GameObject button;

    // private void Awake()
    // {
    //     popUp = this.gameObject;
    //     this.gameObject.SetActive(false);
    // }

    public void ShowPopup()
    {
        this.gameObject.SetActive(true);
    }


}
