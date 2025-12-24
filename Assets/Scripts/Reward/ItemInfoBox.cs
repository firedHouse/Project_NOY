using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    [SerializeField] private Text _itemInfoText;
    [SerializeField] private GameObject _ItemUsecharacterChoicePanel;
    //[SerializeField] private GoldModelScript _goldModelScript;


    public void OnPointOver()
    {
        _itemInfoText.text = "아이템 설명";
    }

    public void OnPointExit()
    {
        _itemInfoText.text = "비어있음";
    }

    public void OnItemClick()
    {
        // 아이템 구매
        // 골드 감소(아이템 가격만큼)
        //_goldModelScript.SpendGold(1000);

        // 사용할 캐릭터 팝업 출력
        _ItemUsecharacterChoicePanel.SetActive(true);
    }
}
