using UnityEngine;
using System;

// 추상 클래스로 선언하여 직접 인스턴스화를 방지
public abstract class BattleUnit : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] protected string unitName;
    [SerializeField] protected float maxHP;
    [SerializeField] protected float currentHP;
    [SerializeField] protected int speed; 
    [SerializeField] protected int attackPower; 

    [Header("State")]
    [SerializeField] protected UnitPosition position;
    [SerializeField] protected ElementType currentMark = ElementType.None; // 기본 무속성
    [SerializeField] protected bool isDead = false;

    //프로퍼티
    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;
    public int Speed => speed;
    public UnitPosition Position => position;
    public bool IsDead => isDead;
    public ElementType CurrentMark => currentMark;

    //UI 갱신 및 전투 로직 연결용
    public event Action<BattleUnit> OnDeath;       //사망 시
    public event Action<BattleUnit, float> OnHpChanged; //ㅊㅔ력 변경 시
    public event Action<BattleUnit, ElementType> OnMarkChanged; //표식 변경 시

    //초기화 (자식 클래스에서 override 할 듯?)
    public virtual void Initialize(string name, float hp, int spd, int atk, UnitPosition pos)
    {
        unitName = name;
        maxHP = hp;
        currentHP = hp;
        speed = spd;
        attackPower = atk;
        position = pos;

        isDead = false;
        currentMark = ElementType.None;
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
        else
        {
            //피격 애니메이션 등 재생(추후 구현)
            //PlayAnimation("Hit"); skill?
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

        //머리 위에 아이콘 표시 로직은 UI Manager가 이 이벤트를 구독해서 처리
    }

    //표식 제거 (원소 반응 발생 시 호출)
    public void ClearMark()
    {
        currentMark = ElementType.None;
        OnMarkChanged?.Invoke(this, ElementType.None);
    }

    //위치 변경 (빈자리 채울 때 사용) 
    public void MovePosition(UnitPosition newPosition)
    {
        position = newPosition;
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