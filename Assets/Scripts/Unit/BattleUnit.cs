using System;
using System.Collections.Generic;
using UnityEngine;

//추상 클래스로 선언하여 직접 인스턴스화를 방지
public abstract class BattleUnit : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] protected string unitID;
    [SerializeField] protected string unitName;
    [SerializeField] protected float maxHP;
    [SerializeField] protected float currentHP;
    [SerializeField] protected int speed;
    [SerializeField] protected float attackPower;

    [Header("State")]
    [SerializeField] protected UnitPosition position;
    [SerializeField] protected ElementType currentMark = ElementType.None; // 기본 무속성
    [SerializeField] protected bool isDead = false;


    //유닛 보유 스킬 리스트
    protected List<Skill> skills = new List<Skill>();

    //전투 중 변동되는 스탯(버프/디버프 값)
    //스테이지 진행마다 증가하는 스탯은 나중에 반영
    protected float currentAttackPower;
    protected int currentSpeed;

    //방어막
    protected float shieldValue = 0f;

    //12.23 버프/디버프 지속시간 관리용 딕셔너리 (Key: 타입, Value: 남은 턴)
    protected Dictionary<SkillType, int> buffDurations = new Dictionary<SkillType, int>();
    //12.23 버프/디버프 적용 수치 저장용 딕셔너리 (효과 해제 시 스탯 복구용)
    protected Dictionary<SkillType, float> buffValues = new Dictionary<SkillType, float>();

    //프로퍼티
    public string UnitName => unitName;
    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;
    public UnitPosition Position => position;
    public bool IsDead => isDead;
    public ElementType CurrentMark => currentMark;
    public List<Skill> Skills => skills;
    public int Speed => currentSpeed; //12.23 외부에서 현재 속도를 적용하도록 프로퍼티 수정
    public float AttackPower => currentAttackPower; //12.22 외부에서 현재 공격력을 적용하도록 프로퍼티 수정
    public float BaseAttackPower => attackPower; //기존 공격력

    public string UnitID => unitID; //12.26 hSkillList에서 


    //12.23 방어막 UI 갱신용
    public event Action<BattleUnit, float> OnShieldChanged; //쉴드 변경 시
    public event Action<BattleUnit> OnDeath;       //사망 시
    public event Action<BattleUnit, float> OnHpChanged; //체력 변경 시
    public event Action<BattleUnit, ElementType> OnMarkChanged; //원소표식 변경 시




    //초기화 (자식 클래스에서 override 할 듯?)
    public virtual void InitializeBase(string id, string name, float hp, int spd, float atk, UnitPosition pos)
    {

        unitID = id;
        unitName = name;
        maxHP = hp;
        currentHP = hp;
        speed = spd;
        attackPower = atk;
        position = pos;

        //12.23 현재 스탯 반영
        //초기화 시 스탯도 초기화
        currentAttackPower = atk;
        currentSpeed = spd;
        shieldValue = 0f;
        buffDurations.Clear();
        buffValues.Clear();


        isDead = false;
        currentMark = ElementType.None;
    }

    //스킬 로드 공통 로직 (스킬ID 리스트를 받아 Skill 객체 생성)
    protected void LoadSkills(List<string> skillIDs)
    {
        skills.Clear();
        foreach (var id in skillIDs)
        {
            Debug.Log($"[Battleunit] {id} 스킬 로드");
            Skill newSkill = new Skill(id);
            //IsValid()가 true일 때만 리스트에 추가
            if (newSkill.IsValid())
            {
                skills.Add(newSkill);
            }
        }
    }

    //데미지 처리 로직 
    //12.23 방어막 소진 이후 체력 소진으로 변경
    public virtual void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }

        float remainingDamage = damage;

        if (shieldValue > 0f)
        {
            if (shieldValue >= remainingDamage)
            {
                shieldValue -= remainingDamage;
                remainingDamage = 0f;
            }
            else
            {
                remainingDamage -= shieldValue;
                shieldValue = 0f;
            }
        }
        //남은 데미지로 체력 차감
        if (remainingDamage > 0)
        {
            currentHP = Mathf.Max(0, currentHP - remainingDamage);
            OnHpChanged?.Invoke(this, currentHP);
        }

        //사망 판정 
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (isDead)
        {
            return;
        }
        currentHP = Mathf.Min(MaxHP, currentHP + amount);
        OnHpChanged?.Invoke(this, currentHP);
        Debug.Log($"{unitName} {amount}만큼 회복");
    }

    public void AddShield(float amount)
    {
        if (isDead)
        {
            return;
        }
        shieldValue += amount;
        OnShieldChanged?.Invoke(this, shieldValue);
        Debug.Log($"{unitName} 방어막 부여 {amount}");
    }

    //12.23스탯 변경 통합 관리용 함수(true 적용, false 복구)
    private void ModifyStat(SkillType type, float value, bool isAdd)
    {
        float modifier = isAdd ? value : -value; //더할지 뺄지 결정

        switch (type)
        {
            case SkillType.AttackBuff: //공격력 증가
                currentAttackPower += modifier;
                break;
            case SkillType.SpeedBuff: //속도 증가
                currentSpeed += (int)modifier;
                break;

            case SkillType.AttackDebuff: //공격력 감소
                currentAttackPower -= modifier;
                break;
            case SkillType.SpeedDebuff: // 속도 감소
                currentSpeed -= (int)modifier;
                break;
        }
    }

    //12.23버프/디버프 적용
    public void ApplyBuff(SkillType type, float value, int duration = 3)
    {
        if (isDead)
        {
            return;
        }

        //이미 적용 중이면 제거 후 재적용 (중첩 방지 및 갱신)
        if (buffDurations.ContainsKey(type))
        {
            RemoveBuff(type);
        }

        //정보 등록
        buffDurations[type] = duration;
        buffValues[type] = value;

        //실제 스탯 변경
        ModifyStat(type, value, true);

        Debug.Log($"[Buff] {unitName}에게 {type} 적용 (값: {value}, {duration}턴)");
    }

    //12.23버프 디버프 해제
    private void RemoveBuff(SkillType type)
    {
        if (buffValues.ContainsKey(type))
        {
            float value = buffValues[type];
            ModifyStat(type, value, false); // false = 복구

            buffDurations.Remove(type);
            buffValues.Remove(type);
        }
    }

    //속성 표식 부여
    public void SetElementalMark(ElementType newMark)
    {
        if (isDead)
        {
            return;
        }

        //이미 동일한 속성이 부여된 경우 표식 부여되지 않음
        if (currentMark == newMark)
        {
            return;
        }

        currentMark = newMark;
        OnMarkChanged?.Invoke(this, currentMark);
    }

    //표식 제거 (원소 반응 발생 시 호출)
    public void ClearMark()
    {
        currentMark = ElementType.None;
        OnMarkChanged?.Invoke(this, ElementType.None);
    }

    //위치 변경 (사망, 영입, 방출, 부활 시 사용) 
    //12.22 추가
    public void MovePosition(UnitPosition newPosition, Vector3 targetWorldPos)
    {
        position = newPosition;
        //이동 애니메이션?(미정)
        //일단 순간이동
        transform.position = targetWorldPos;
        Debug.Log($"MOVE: {unitName} => {newPosition} 위치로 이동");
    }

    protected virtual void Die()
    {
        isDead = true;

        OnDeath?.Invoke(this);
        gameObject.SetActive(false);
    }

    //턴 종료 시 호출 (과부하 지속 피해 및 버프, 디버프 지속시간 관리)
    public void OnTurnEnd(IEnumerable<BattleUnit> myTeam)
    {
        //과부하 체크
        var elemental = GetComponent<ElementalManager>();

        if (elemental != null && elemental.IsOverloadActive)
        {
            //데미지 먼저
            Debug.Log($"{unitName} 과부하 도트딜 적용");
            ReactionDamageProcesser.ApplyOverload(this);

            //턴 차감 및 종료 메서드 
            elemental.DecreaseOverloadTurn();
        }

        //버프/디버프 지속시간 관리
        //딕셔너리 수정을 위해 키 리스트 복사
        List<SkillType> keys = new List<SkillType>(buffDurations.Keys);
        foreach (var key in keys)
        {
            buffDurations[key]--; // 1턴 차감

            if (buffDurations[key] <= 0)
            {
                //시간 다 되면 해제 및 스탯 복구
                RemoveBuff(key); 
                Debug.Log($"[버프] {unitName}의 {key} 효과 종료");
            }
        }
    }
}