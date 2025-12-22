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

    //[변경] 유닛이 보유한 스킬 리스트 (최대 3개)
    protected List<Skill> skills = new List<Skill>();

    //프로퍼티
    public string UnitName => unitName;
    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;
    public int Speed => speed;
    public UnitPosition Position => position;
    public bool IsDead => isDead;
    public ElementType CurrentMark => currentMark;
    public List<Skill> Skills => skills;
    public float AttackPower => attackPower;

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
    public virtual void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }

        currentHP = Mathf.Max(0, currentHP - damage);

        //UI 갱신 알림
        OnHpChanged?.Invoke(this, currentHP);

        //사망 판정 
        if (currentHP <= 0)
        {
            Die();
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

    //위치 변경 (빈자리 채울 때 사용) 
    //리팩토링 후순위(FieldManager제작 후)
    public void MovePosition(UnitPosition newPosition)
    {
        position = newPosition;
        //이동 애니메이션?(미정)
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