using UnityEngine;

//실제 게임 오브젝트가 아닌 스킬 객체 뼈대
//추후 커맨드패턴으로 다 변경할 수도
[System.Serializable]
public class Skill
{
    //TableManager에서 가져온 원본 데이터
    public SkillData Data { get; private set; }

    //현재 남은 PP (런타임 시점에서 변동)
    public int CurrentPP { get; private set; }

    //생성자: 데이터 테이블의 정보를 받아 초기화
    public Skill(string skillID)
    {
        if (string.IsNullOrEmpty(skillID) || skillID == "None")
        {
            Debug.Log($"[Skill] ");
            Data = null;
            return;
        }
        Data = TableManager.Instance.SkillTable.Get(skillID);

        if (Data != null)
        {
            CurrentPP = Data.skillPP;
        }
        else
        {
            Debug.LogError($"{skillID}가 테이블에 없음");
        }
    }

    //비어있으면 false 반환
    public bool IsValid()
    {
        return Data != null;
    }

    //스킬 사용 시도
    public bool TryUse()
    {
        //PP 차감 성공 시 true 반환
        if (CurrentPP > 0)
        {
            CurrentPP--;
            return true;
        }
        return false;
    }

    //스킬 데미지 계산식
    public float CalculateValue(float userStat)
    {
        if (Data == null)
        {
            return 0f;
        }
        return Data.skillBaseValue + (userStat * Data.skillFactor);
    }

    //PP 회복 (아이템 사용 등)
    public void RestorePP(int amount)
    {
        if (Data == null)
        {
            return;
        }
        CurrentPP = Mathf.Min(Data.skillPP, CurrentPP + amount);
    }
}