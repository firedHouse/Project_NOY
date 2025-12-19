using UnityEditor.U2D.Animation;
using UnityEngine;

public class MonsterInfoPresenter : MonoBehaviour
{
    [SerializeField] private Monster monsterModel;
    [SerializeField] private MonsterInfoView monsterView;
    private MonsterData monsterData;
    private string monId = "monster_id_10001";
    [SerializeField] private UnitPosition position;

    // 테스트용 필드
    private float HpChangeValue = 50;

    private void Start()
    {
        //monsterModel.DataLoaded += OnDataLoaded;
        Initialize();
    }

    void Initialize()
    {
        monsterModel.InitializeMonster(monId, position, false);
        Debug.Log($"[MonsterInfoPresenter] monsterData 내부 데이터 불러오기 성공");

        monsterModel.OnHpChanged += HandleHpChanged;
        monsterView.UpdateMonsterName(monsterModel.UnitName);
        //monsterView.UpdateElement(monsterModel.elementType);
        monsterView.UpdateSpeed(monsterModel.Speed);
        //monsterView.UpdatePower(monsterModel.Monster.monsterAttack);
        monsterView.SetMaxHP(monsterModel.MaxHP);
    }

    private void HandleHpChanged(BattleUnit monster, float hpChangedAmount)
    {
        monsterView.UpdateHPBar(hpChangedAmount);
    }

    private void HandleMarkChanged(BattleUnit monster, ElementType elementType)
    {

    }
}
