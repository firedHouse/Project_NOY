using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBattleInfoView : MonoBehaviour
{
    [SerializeField] private Text characterNameText;
    [SerializeField] private Text elementText;
    [SerializeField] private Text positionText;
    [SerializeField] private Image characterIllurstration;
    [SerializeField] private Text characterSpeedText;
    [SerializeField] private Text characterPowerText;
    [SerializeField] private Slider HPSlider;
 
    private void OnEnable()
    {
        // 여러 데이터로 뷰를 채우는 방법 찾아보기, List로 받아와 채워줘야 하나 생각
    }

    // 캐릭터 이름으로 UI 변경
    public void UpdateCharacterName(string text)
    {
        characterNameText.text = text;
    }

    public void UpdateElement()
    {
        //elementText.text = 
    }

    public void SetMaxHP(float maxHP)
    {
        HPSlider.maxValue = maxHP;
        HPSlider.value = maxHP;
        Debug.Log($"[CharacterBattleInfoView] 최대 Hp 설정 완료({HPSlider.maxValue})");
    }

    public void UpdateHPBar(float currentHP)
    {
        HPSlider.value = currentHP;
    }

    public void UpdateElementMark(ElementType elementType)
    {

    }

    public void UpdateSpeed(int speed)
    {
        characterSpeedText.text = speed.ToString();
    }
}
