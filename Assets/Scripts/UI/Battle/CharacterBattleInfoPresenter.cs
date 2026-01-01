using System;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterBattleInfoPresenter : MonoBehaviour
{
    [Header("紐⑤뜽, �윴����엫 以� �옄�룞 異붽���맖.")]
    [SerializeField] private Character characterModel;
    [Header("罹먮┃�꽣 CharacterBox Panel, �쟾以묓썑�뿴�뿉 留욊쾶 媛곴컖 異붽��")]
    [SerializeField] private CharacterBattleInfoView characterView;
    //private CharacterData characterData;
    [Header("�쟾以묓썑�뿴 媛�")]
    [SerializeField] private UnitPosition position;

    [SerializeField] private SkillProcesser skillProcesser;

    [SerializeField] private RelicStatusUI relicStatusUI;
    private RelicComponent relicComponent;
    public ElementalManager elementalManager;

    #region position�뿉�꽌 �궗�슜
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

    //酉� 珥덇린 �꽕�젙
    public void Initialize()
    {
        Debug.Log("[CharacterBattleInfoPresenter] 珥덇린�솕");
        characterModel = BattleManager.Instance.PlayerTeam[(int)position];
        characterModel.OnDeath += HandleDeath;
        //characterModel.OnDeath += HandlePositionChanged;
        characterModel.OnHpChanged += HandleHpChanged;
        //characterModel.OnMarkChanged += HandleMarkChanged;


        //HP諛� �닔�젙 > �쁽�옱 泥대젰�쑝濡�
        characterView.SetMaxHP(characterModel.MaxHP);
        characterView.UpdateHPBar(characterModel.CurrentHP);
        characterView.SetSkillList(characterModel.Skills);
        characterView.UpdateCharacterName(characterModel.UnitName);
        characterView.UpdateSpeed(characterModel.Speed);
        characterView.UpdateCharacterPosition(characterModel.CharacterPosition);

        characterView.gameObject.SetActive(true);
        characterView.InitMark(characterModel);

        // 罹먮┃�꽣�뿉�뒗 �냽�꽦�씠 �뾾�뼱�꽌 肄붾뱶 �궘�젣
        //怨좎쑀�냽�꽦
        //elementUI濡� 蹂�寃쏀빐�빞 �븿
        // characterView.UpdateClass(characterModel.CurrentMark.ToString());
        // characterView.InitMark();

        BindRelicUI(characterModel);
    }

    private void HandleHpChanged(BattleUnit character, float hpChangedAmount)
    {
        characterView.UpdateHPBar(hpChangedAmount);

    }

    // �냽�꽦 �몴�떆 蹂�寃� �뜲�씠�꽣�씤�뜲 �냽�꽦�씠 �뜲�씠�꽣 �뀒�씠釉붿뿉 �뾾�뼱�꽌 吏�湲덉�� �궗�슜 �븞�븿
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
        //�몴�떇
        if (unit.UnitID == characterModel.UnitID)
        {
            characterView.UpdateMark(i, unit);
        }
    }
    public void SMark(BattleUnit unit, ElementReaction i)
    {
        //�몴�떇
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

        // 珥덇린 �긽�깭 1�쉶 諛섏쁺
        relicStatusUI.Refresh(relicComponent.CurrentRelic);
    }

    private void OnDestroy()
    {
        if (relicComponent != null && relicStatusUI != null)
            relicComponent.OnRelicChanged -= relicStatusUI.Refresh;
    }
}

