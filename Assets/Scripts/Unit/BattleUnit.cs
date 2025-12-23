using System;
using System.Collections.Generic;
using UnityEngine;

// 추상 클래스로 선언하여 직접 인스턴스화를 방지
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


    //이거 이관작업 해야 할 거 같은데
    //일단 보류
    protected List<Skill> skills = new List<Skill>();

    //전투 중 변동되는 스탯(버프/디버프 값)
    //스테이지 진행마다 증가하는 스탯은 나중에 반영
    protected float currentAttackPower;
    protected int currentSpeed;

    //방어막
    protected float shieldValue = 0f;



    //프로퍼티
    public string UnitName => unitName;
    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;
    public int Speed => speed;
    public UnitPosition Position => position;
    public bool IsDead => isDead;
    public ElementType CurrentMark => currentMark;
    public List<Skill> Skills => skills;
    public float AttackPower => currentAttackPower; //12.22 외부에서 현재 공격력을 적용하도록 프로퍼티 수정
    public float BaseAttackPower => attackPower; //기존 공격력


    //12.23 방어막 UI 갱신용
    public event Action<BattleUnit, float> OnShieldChanged;

    //UI 갱신 및 전투 로직 연결용
    //UI 갱신 및 전투 로직 연결용
    //UI 갱신 및 전투 로직 연결용
    public event Action<BattleUnit> OnDeath;       //사망 시
    public event Action<BattleUnit, float> OnHpChanged; //ㅊㅔ력 변경 시
    public event Action<BattleUnit, ElementType> OnMarkChanged; //원소표식 변경 시
    //UI 갱신 및 전투 로직 연결용
    //UI 갱신 및 전투 로직 연결용
    //UI 갱신 및 전투 로직 연결용



    //초기화 (자식 클래스에서 override 할 듯?)
    public virtual void InitializeBase(string id, string name, float hp, int spd, float atk, UnitPosition pos)
    {

        unitID = id;
        unitName = name;
        maxHP = hp;
        currentHP = hp;
        speed = spd;
        attackPower = atk;

        //12.23 현재 스탯 반영
        //초기화 시 스탯도 초기화
        currentAttackPower = atk;
        currentSpeed = spd;
        shieldValue = 0f;

        position = pos;
        isDead = false;
        currentMark = ElementType.None;
    }

    //스킬 로드 공통 로직 (스킬ID 리스트를 받아 Skill 객체 생성)
    protected void LoadSkills(List<string> skillIDs)
    {
        skills.Clear();
        foreach (var id in skillIDs)
        {
            Debug.Log($"[Battleunit] {id} ");
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

    //버프/디버프 적용
    public void ApplyBuff(SkillType type, float value)
    {
        if (isDead)
        {
            return;
        }
        //타입 지정
        switch (type)
        {
            case SkillType.AttackBuff:
                currentAttackPower += value;
                Debug.Log($"{unitName} 공격력 증가: +{value}");
                break;
            case SkillType.SpeedBuff:
                currentSpeed += (int)value; // 속도는 int
                break;
            case SkillType.AttackDebuff:
                currentAttackPower = Mathf.Max(0, currentAttackPower - value);
                Debug.Log($" {unitName} 공격력 감소: -{value}");
                break;
            case SkillType.SpeedDebuff:
                currentSpeed = Mathf.Max(0, currentSpeed - (int)value);
                break;
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

        //캐릭터가 없어지고 빈자리 발생 시, FieldManager가 이 이벤트를 수신하여 캐릭터 이동
        //몬스터는 리워드 제공
        OnDeath?.Invoke(this);
        gameObject.SetActive(false);
    }
}