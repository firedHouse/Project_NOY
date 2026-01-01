using System;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterBattleInfoPresenter : MonoBehaviour
{
    [Header("모델, 런타임 중 자동 추가됨.")]
    [SerializeField] private Character characterModel;
    [Header("캐릭터 CharacterBox Panel, 전중후열에 맞게 각각 추가")]
    [SerializeField] private CharacterBattleInfoView characterView;
    //private CharacterData characterData;
    [Header("전중후열 값")]
    [SerializeField] private UnitPosition position;

    [SerializeField] private SkillProcesser skillProcesser;

    [SerializeField] private RelicStatusUI relicStatusUI;
    private RelicComponent relicComponent;
    public ElementalManager elementalManager;

    #region position에서 사용
    private BattleUnit[] _character = new BattleUnit[3];
    [SerializeField] private GameObject[] _basePos = new GameObject[3];
    private Dictionary<BattleUnit, int> characterBoxPos = new Dictionary<BattleUnit, int>();

    int currentCount;
    public GameObject[] BasePos => _basePos;
    #endregion


    private void Awake()
    {
        //Debug.Log("[CharacterBattleInfoPresenter] Awake");
        BattleManager.Instance.OnBattleSetted += Initialize;
        skillProcesser.OnMarkChanged += Mark;
        skillProcesser.OnMarkReaction += SMark;

    }

    private void Start()
    {
        elementalManager = characterModel.GetComponent<ElementalManager>();
        elementalManager.OnMarkReaction += characterView.IsMarkReaction;
    }

    //뷰 초기 설정
    public void Initialize()
    {
        Debug.Log("[CharacterBattleInfoPresenter] 초기화");
        characterModel = BattleManager.Instance.PlayerTeam[(int)position];
        characterModel.OnDeath += HandleDeath;
        //characterModel.OnDeath += HandlePositionChanged;
        characterModel.OnHpChanged += HandleHpChanged;
        //characterModel.OnMarkChanged += HandleMarkChanged;


        //HP바 수정 > 현재 체력으로
        characterView.SetMaxHP(characterModel.MaxHP);
        characterView.UpdateHPBar(characterModel.CurrentHP);
        characterView.SetSkillList(characterModel.Skills);
        characterView.UpdateCharacterName(characterModel.UnitName);
        characterView.UpdateSpeed(characterModel.Speed);
        characterView.UpdateCharacterPosition(characterModel.CharacterPosition);

        characterView.gameObject.SetActive(true);
        characterView.InitMark(characterModel);

        // 캐릭터에는 속성이 없어서 코드 삭제
        //고유속성
        //elementUI로 변경해야 함
        // characterView.UpdateClass(characterModel.CurrentMark.ToString());
        // characterView.InitMark();

        BindRelicUI(characterModel);
    }

    private void HandleHpChanged(BattleUnit character, float hpChangedAmount)
    {
        characterView.UpdateHPBar(hpChangedAmount);

    }

    // 속성 표시 변경 데이터인데 속성이 데이터 테이블에 없어서 지금은 사용 안함
    //private void HandleMarkChanged(BattleUnit character, ElementType elementType)
    //{
    //}

    private void HandleDeath(BattleUnit unit)
    {
        BattleManager.Instance.OnUnitDead(unit);
        PosReset(unit);
    }

    private void HandlePositionChanged(BattleUnit unit)
    {
        //characterView.UpdatePosition(position);
    }

    public void Mark(BattleUnit unit, ElementType i)
    {
        //표식
        if (unit.UnitID == characterModel.UnitID)
        {
            characterView.UpdateMark(i, unit);
        }
    }
    public void SMark(BattleUnit unit, ElementReaction i)
    {
        //표식
        if (unit.UnitID == characterModel.UnitID)
        {
            characterView.SUpdateMark(i, unit);
        }

    }

    private void BindRelicUI(Character character)
    {
        relicComponent = character.GetComponent<RelicComponent>();

        if (relicComponent == null || relicStatusUI == null)
            return;

        relicComponent.OnRelicChanged += relicStatusUI.Refresh;

        // 초기 상태 1회 반영
        relicStatusUI.Refresh(relicComponent.CurrentRelic);
    }

    private void OnDestroy()
    {
        if (relicComponent != null && relicStatusUI != null)
            relicComponent.OnRelicChanged -= relicStatusUI.Refresh;
    }
}

