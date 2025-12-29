using System.Collections.Generic;
using UnityEngine;

//아군 유닛 뼈대 스크립트
public class Character : BattleUnit
{   //한솔 현재 스킨 받아올 프로퍼티 작성
    public Sprite currentSkinSprite { get; private set; }

    // Jihoo, 12.29
    [SerializeField] private CharacterPosition characterPosition;
    public CharacterPosition CharacterPosition => characterPosition;
    // Jihoo
    
    //초기화 메서드 필요
    public void InitializeCharacter(string charID, UnitPosition pos)
    {
        //캐릭터 기본 데이터 조회(1학년)
        CharacterData baseData = TableManager.Instance.CharacterTable.Get(charID);
        Debug.Log(baseData);

        if (baseData == null)
        {
            Debug.LogError($"캐릭터 데이터를 찾지 못했읍니다 {charID}");
            return;
        }

        //12.28 성장 반영 로직 추가

        //유저데이터매니저로 현재 이 캐릭터의 학년을 조회
        int currentGradeLevel = UserDataManager.Instance.GetCharacterGrade(charID);

        //최종 스탯 변수 (일단 1학년 기본값으로 시작)
        float finalHP = baseData.HPLevel1;
        float finalAtk = baseData.attackLevel1;
        string finalSkinID = baseData.characterSkin;
        //성장이 되어있다면
        if (currentGradeLevel > 0)
        {
            //성장 데이터 가져오기
            GradeData gradeData = TableManager.Instance.GetGradeData(charID, currentGradeLevel);
            if (gradeData != null)
            {
                //기본 스탯에 성장치 더하기
                finalHP += gradeData.hpUP;
                finalAtk += gradeData.attackUP;
                if (!string.IsNullOrEmpty(gradeData.changeSkin))
                {
                    finalSkinID = gradeData.changeSkin;
                }
                Debug.Log($"성장 반영 완료(HP:{finalHP}, ATK:{finalAtk})");
            }
        }
        //12.29 스킨ID로 진짜 파일명 찾기
        SkinData skinData = TableManager.Instance.SkinTable.Get(finalSkinID);

        if (skinData != null)
        {
            //csv 컬럼명
            string spriteFileName = skinData.skinSprite;
            //파일명 전달하고 이미지 로드
            Sprite finalSprite = ResourceManager.Instance.LoadSprite(spriteFileName);
            //스프라이트 반영
            GetComponent<SpriteRenderer>().sprite = finalSprite;
            //12.30 한솔 스킨 값 받아오기 메서드 사용
            ApplySkin(skinData);
        }
        else
        {
            Debug.LogError($"스킨 데이터를 찾을 수 없읆,,, {finalSkinID}");
        }
        //성장 반영하여 유닛 초기화
        InitializeBase(
            charID,
            baseData.characterName,
            finalHP,
            baseData.speed,
            finalAtk,
            pos
        );  
            // Jihoo 12.29
            // 탱딜힐 포지션 저장
            characterPosition = (CharacterPosition)baseData.position; 

        //스킬 로두
        List<string> skillIDs = new List<string>()
        {
            baseData.ownedSkill01,
            baseData.ownedSkill02,
            baseData.ownedSkill03
        };
        LoadSkills(skillIDs);
    }

    //스킬 사용 함수 (인덱스: 0, 1, 2)
    public void UseSkill(int skillIndex, BattleUnit target)
    {
        if (skillIndex < 0 || skillIndex >= skills.Count)
        {
            return;
        }

        Skill skill = skills[skillIndex];

        //PP 체크 및 소모
        if (skill.TryUse())
        {
            //데미지 계산 및 적용 로직은 BattleManager 에서 처리 예정
            Debug.Log($"{unitName} 가 스킬: {skill.Data.skillName} 사용. (남은 PP: {skill.CurrentPP})");
        }
        else
        {
            Debug.Log("PP 부족!");

        }
    }
    //12.30 한솔 스킨 값 가져오기
    public void ApplySkin(SkinData skinData)
    {
        if (skinData == null || skinData.skinSprite == null)
        {
            Debug.Log("스킨 없음 !");
            return;
        }

        Sprite sprite = ResourceManager.Instance.LoadSprite(skinData.skinSprite);

        if (sprite == null)
        {
            Debug.LogWarning($"{unitName} 스킨 Sprite 로드 실패 : {skinData.skinSprite}");
        }

        currentSkinSprite = sprite;

        GetComponent<SpriteRenderer>().sprite = currentSkinSprite;
    }
}
