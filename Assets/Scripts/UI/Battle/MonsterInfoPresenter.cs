using System.Collections.Generic;
using UnityEngine;

partial class MonsterInfoPresenter : MonoBehaviour
{
    [Header("모델, 런타임 중 자동 추가")]
    [SerializeField] private Monster monsterModel;

    [Header("몬스터 CharacterBox Panel, 전중후열에 맞게 각각 추가")]
    [SerializeField] private MonsterInfoView monsterView;

    [Header("MonsterBoxGroupPanel 추가")]
    [SerializeField] private MonsterPositionView monsterMoveView;

    //private MonsterData monsterData;
    [SerializeField] private UnitPosition position;

    [SerializeField] private SkillProcesser skillProcesser;

    #region position에서 사용
    private BattleUnit[] _monster = new BattleUnit[3];
    [SerializeField] private GameObject[] _basePos = new GameObject[3];
    private Dictionary<BattleUnit, int> monsterBoxPos = new Dictionary<BattleUnit, int>();

    int currentCount;
    public GameObject[] BasePos => _basePos;
    #endregion


    private void Awake()
    {
        //Debug.Log("[MonsterInfoPresenter] Awake");
        BattleManager.Instance.OnBattleSetted += Initialize;
        BattleManager.Instance.OnBattleSetted += DeathMonster;
        skillProcesser.OnMarkChanged += Mark;
        skillProcesser.OnMarkReaction += SMark;
    }

        void Initialize()
        {
            if (BattleManager.Instance.EnemyTeam != null)
            {
                monsterModel = BattleManager.Instance.EnemyTeam[(int)position];
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

            monsterView.SetMaxHP(monsterModel.MaxHP);
            monsterView.UpdateMonsterName(monsterModel.UnitName);
            monsterView.UpdateSpeed(monsterModel.Speed);

            monsterView.UpdateMonsterClass(monsterModel.MonsterPosition);
            monsterView.InitMark();



            //고유속성
            //ElementUI 사용
            //monsterView.UpdateElementClass(monsterModel.ElementUI.ToString());
            //monsterView.UpdateCalss(monsterModel.Role);
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

    public void Mark(BattleUnit unit, ElementType i)
    {
        //표식
        if (unit.UnitID == monsterModel.UnitID)
        {
            monsterView.UpdateMark(i);
            Debug.Log($"[CharacterBattleInfoPresenter] {unit.UnitName}에게 {i} 부여");
        }

    }

    public void SMark(BattleUnit unit, ElementReaction i)
    {
        //표식
        if (unit.UnitID == monsterModel.UnitID)
        {
            monsterView.SUpdateMark(i);
            Debug.Log($"[CharacterBattleInfoPresenter] {unit.UnitName}에게 {i} 부여");
        }

    }
}

