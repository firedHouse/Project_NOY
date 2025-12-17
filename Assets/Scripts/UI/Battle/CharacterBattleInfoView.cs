using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBattleInfoView : MonoBehaviour
{
    [SerializeField] private Text characterNameText;
    //[SerializeField] private Text elementText;
    //[SerializeField] private Text positionText;
    [SerializeField] private Image characterIllurstration;
    // HP MVP만 분리하는 게 좋을까?

    private void OnEnable()
    {
        // 여러 데이터로 뷰를 채우는 방법 찾아보기, List로 받아와 채워줘야 하나 생각
    }

    // 캐릭터 이름으로 UI 변경
    public void UpdateCharacterName(string text)
    {
        characterNameText.text = text;
    }
}
