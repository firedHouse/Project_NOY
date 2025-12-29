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

    // 테스트용 필드
    private float HpChangeValue = 50;

    private void Awake()
    {
        //Debug.Log("[MonsterInfoPresenter] Awake");
        BattleManager.Instance.OnBattleSetted += Initialize;
    }

    void Initialize()
    {
        if(BattleManager.Instance.EnemyTeam != null)
        {
            monsterModel = BattleManager.Instance.EnemyTeam[(int)position];
        }
        Debug.Log($"[MonsterInfoPresenter] monsterData 내부 데이터 불러오기 성공");
        ViewInit();
    }

    void ViewInit()
    {
        if(BattleManager.Instance.EnemyTeam != null)
        {
            monsterModel = BattleManager.Instance.EnemyTeam[(int)position];
        }
        //Debug.Log($"[MonsterInfoPresenter] monsterData 내부 데이터 불러오기 성공");

        monsterModel.OnHpChanged += HandleHpChanged;
        //monsterView.UpdatePower(monsterModel.Monster.monsterAttack);

        monsterView.SetMaxHP(monsterModel.MaxHP);
        monsterView.UpdateMonsterName(monsterModel.UnitName);
        monsterView.UpdateSpeed(monsterModel.Speed);

        monsterView.UpdatePosition(monsterModel.Position);
        monsterView.InitMark();



        //고유속성
        //ElementUI 사용
        //monsterView.UpdateElementClass(monsterModel.ElementUI.ToString());
        //monsterView.UpdateCalss(monsterModel.Role);
    }

    private void HandleHpChanged(BattleUnit monster, float hpChangedAmount)
    {
        monsterView.UpdateHPBar(hpChangedAmount);
        //표식
        monsterView.UpdateMark(monsterModel.gameObject.layer);
    }

    //private void HandleMarkChanged(BattleUnit monster, ElementType elementType)
    //{
    //}

    private void HandleDeath(BattleUnit unit)
    {
        BattleManager.Instance.OnUnitDead(unit);
    }

    private void HandlePositionChanged(BattleUnit uni)
    {
        monsterView.UpdatePosition(position);
    }

}
