using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class CharacterBattleInfoView : MonoBehaviour
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
    [Header("캐릭터 표식 상태")]
    //추후에 이미지로 변경

    [Header("표식1")]
    [SerializeField] Text currentMark;
    //[SerializeField] Image currentMark;
    [Header("표식2")]
    [SerializeField] Text attackMark;
    //[SerializeField] Image attackMark;

    private List<Skill> characterskills;


    // 캐릭터 이름으로 UI 변경
    public void UpdateCharacterName(string text)
    {
        characterNameText.text = text;
    }

    // 고유속성
    public void UpdateClass(string text)
    {
        characterElementText.text = text;
    }

    //표식
    //public void UpdateElementMark(string text)
    //{
    //    mark1.text = text;
    //}

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
