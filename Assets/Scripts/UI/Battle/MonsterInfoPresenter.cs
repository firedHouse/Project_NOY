using System.Collections.Generic;
using UnityEngine;

partial class MonsterInfoPresenter : MonoBehaviour
{
    [Header("모델, 런타임 중 자동 추가")]
    [SerializeField] private Monster monsterModel;
    [SerializeField] private MonsterInfoModel infoModel;

    [Header("몬스터 CharacterBox Panel, 전중후열에 맞게 각각 추가")]
    [SerializeField] private MonsterInfoView monsterView;

    //private MonsterData monsterData;
    [SerializeField] private UnitPosition position;

    [SerializeField] private SkillProcesser skillProcesser;
    [SerializeField] public ElementalManager elementalManager;

    #region position에서 사용
    private BattleUnit[] _monster;
    [SerializeField] private GameObject[] _basePos = new GameObject[3];
    private Dictionary<BattleUnit, int> monsterBoxPos = new Dictionary<BattleUnit, int>();

    int currentCount;
    public GameObject[] BasePos => _basePos;
    #endregion


    private void Awake()
    {
        //Debug.Log("[MonsterInfoPresenter] Awake");
        BattleManager.Instance.OnBattleSetted += Initialize;
        skillProcesser.OnMarkChanged += Mark;
        skillProcesser.OnMarkReaction += SMark;
    }

    private void Start()
    {
        elementalManager = monsterModel.GetComponent<ElementalManager>();
        elementalManager.OnMarkReaction += monsterView.IsMarkReaction;
    }

    void Initialize()
    {
        if (BattleManager.Instance.EnemyTeam != null)
        {
            if (BattleManager.Instance.EnemyTeam != null)
            {
                // 임시방편
                // 리스트에 크기를 넘어가는 걸 확인하지 않고 참조하는 것이 문제
                if ((int)position >= BattleManager.Instance.EnemyTeam.Count)
                {
                    gameObject.SetActive(false);
                    return;
                }
                monsterModel = BattleManager.Instance.EnemyTeam[(int)position];
            }
            monsterModel.OnDeath += HandleDeath;
            Debug.Log($"[MonsterInfoPresenter] monsterData 내부 데이터 불러오기 성공");
            ViewInit();
        }
        monsterModel.OnDeath += HandleDeath;
        Debug.Log($"[MonsterInfoPresenter] monsterData 내부 데이터 불러오기 성공");
        ViewInit();
    }

    void ViewInit()
    {
        if (BattleManager.Instance.EnemyTeam != null)
        {
            monsterModel = BattleManager.Instance.EnemyTeam[(int)position];
        }
        //Debug.Log($"[MonsterInfoPresenter] monsterData 내부 데이터 불러오기 성공");

        monsterModel.OnHpChanged += HandleHpChanged;
        //monsterView.UpdatePower(monsterModel.Monster.monsterAttack);

        //HP바 수정 > 현재 체력으로
        monsterView.SetMaxHP(monsterModel.CurrentHP);
        monsterView.UpdateHPBar(monsterModel.CurrentHP);
        monsterView.UpdateMonsterName(monsterModel.UnitName);
        monsterView.UpdateSpeed(monsterModel.Speed);
        GetElementUI(monsterModel.UnitID);



        monsterView.UpdateMonsterClass(monsterModel.MonsterPosition);
        monsterView.InitMark(monsterModel);

        monsterView.gameObject.SetActive(true);

        //고유속성
        //ElementUI 사용
        // monsterView.UpdateElementClass(monsterModel.MonsterElementUI);
        // monsterView.UpdateCalss(monsterModel.Role);
    }
    //Debug.Log($"[MonsterInfoPresenter] monsterData 내부 데이터 불러오기 성공");

    private void HandleHpChanged(BattleUnit monster, float hpChangedAmount)
    {
        monsterView.UpdateHPBar(hpChangedAmount);
    }

    private void HandleDeath(BattleUnit unit)
    {
        PosReset(unit);
    }

    ElementType elementType;
    //0불 1물 2전기 3무속성
    private void GetElementUI(string ID)
    {
        int monster = TableManager.Instance.MonsterTable.Get(ID).elementUI;

        switch (monster)
        {
            case 0:
                elementType = ElementType.Fire;
                break;
            case 1:
                elementType = ElementType.Water;
                break;
            case 2:
                elementType = ElementType.Electric;
                break;
            case 3:
                elementType = ElementType.None;
                break;
        }

        monsterView.UpdateElement(elementType.ToString());
    }

    public void Mark(BattleUnit unit, ElementType i)
    {
        //표식
        if (unit.UnitID == monsterModel.UnitID)
        {
            monsterView.UpdateMark(i, unit);
        }

    }

    public void SMark(BattleUnit unit, ElementReaction i)
    {
        //표식
        if (unit.UnitID == monsterModel.UnitID)
        {
            monsterView.SUpdateMark(i, unit);
        }

    }
}

