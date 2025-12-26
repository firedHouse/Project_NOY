using UnityEngine;
using UnityEngine.UI;

public class RosterView : MonoBehaviour
{
    [Header("캐릭터 일러스트")]
    [SerializeField] private Text characterIllust;
    //[SerializeField] private Image[] characterIllust = new Image[3];

    [Header("캐릭터 이름")]
    [SerializeField] private Text TextcharacterName;

    [Header("캐릭터 클래스")]
    [SerializeField] private Text characterClass;

    [Header("캐릭터 속성")]
    [SerializeField] private Text characterElement;




    public void InitPage(RosterModel model)
    {
        characterIllust.text = model.ChracterIllust;
        TextcharacterName.text = model.ChracterName;
        characterClass.text = model.ChracterClass.ToString();
        characterElement.text = model.ChracterElement.ToString();
    }

    public void OnSkipButton()
    {
        Debug.Log("[RosterView] 스킵버튼 눌렀음");
    }
}
