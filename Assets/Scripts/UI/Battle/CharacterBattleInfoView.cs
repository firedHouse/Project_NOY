using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBattleInfoView : MonoBehaviour
{
    [Header("캐릭터 이름")]
    [SerializeField] private Text characterNameText;
    [Header("캐릭터 속성")]
    [SerializeField] private Text characterElementText;
    [Header("캐릭터 클래스")]
    [SerializeField] private Text characterPositionText;
    [Header("캐릭터 이미지")]
    [SerializeField] private Image characterIllurstration;
    [Header("캐릭터 스피드")]
    [SerializeField] private Text characterSpeedText;
    [Header("캐릭터 공격력")]
    [SerializeField] private Text characterPowerText;
    [Header("캐릭터 HP 바")]
    [SerializeField] private Slider HPSlider;
    
    private List<Skill> characterskills;

    // 캐릭터 이름으로 UI 변경
    public void UpdateCharacterName(string text)
    {
        characterNameText.text = text;
    }

    // 속성 표시 변경 데이터인데 속성이 데이터 테이블에 없어서 지금은 사용 안함
    public void UpdateElement(string text)
    {
        characterElementText.text = text;
    }

    public void SetMaxHP(float maxHP)
    {
        HPSlider.maxValue = maxHP;
        HPSlider.value = maxHP;
        //Debug.Log($"[CharacterBattleInfoView] 최대 Hp 설정 완료({HPSlider.maxValue})");
    }

    public void UpdateHPBar(float currentHP)
    {
        HPSlider.value = currentHP;
    }

    // 포지션 업데이트
    // 각 유닛 객체의 포지션을 변경? <- 이미 구현되어 있을 것 같은데
    // 갱신된 포지션에 따라 패널에 다시 불러오기 realTargets로 불러오기?

    //public void UpdatePosition(CharacterPosition role)
    //{
    //    characterPositionText.text = role.ToString();
    //}

    //public void UpdateElementMark(ElementType elementType)
    //{

    //}

    public void UpdateSpeed(int speed)
    {
        characterSpeedText.text = speed.ToString();
    }

    // 스킬 리스트 UI에서 불러올 캐릭터 스킬 리스트
    public void SetSkillList(List<Skill> skills)
    {
        characterskills = skills;
    }
}
