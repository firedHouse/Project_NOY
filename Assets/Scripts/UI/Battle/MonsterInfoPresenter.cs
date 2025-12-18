using UnityEditor.U2D.Animation;
using UnityEngine;

public class MonsterInfoPresenter : MonoBehaviour
{
    [SerializeField] private MonsterInfoModel monsterModel;
    [SerializeField] private MonsterInfoView monsterView;
    private MonsterData monsterData;

    // 테스트용 필드
    private float HpChangeValue = 50;

    private void Awake()
    {
        monsterModel.DataLoaded += OnDataLoaded;
    }

    void Initialize()
    {
        monsterData = monsterModel.Monster;
        HPBarController hPBarController = new HPBarController();
        if (monsterData != null)
        {
            Debug.Log($"[MonsterInfoPresenter] characterData 내부 데이터 불러오기 성공");

            monsterModel.MonsterHPChanged += OnHPChanged;
            monsterView.UpdateMonsterName(monsterModel.Monster.monsterName);
            monsterView.UpdateElement(monsterModel.Monster.elementType);
            monsterView.UpdateSpeed(monsterModel.Monster.monsterSpeed);
            monsterView.UpdatePower(monsterModel.Monster.monsterAttack);
            monsterView.SetMaxHP(monsterModel.MaxHP);
            UpdateUI();
        }
        else
        {
            Debug.Log($"[MonsterInfoPresenter] monsterData 내부 데이터 비어있음");
        }
    }

    private void OnDataLoaded()
    {
        Initialize();
    }

    private void OnHPChanged()
    {
        monsterView.UpdateHPBar(monsterModel.Monster.monsterHP);
    }

    private void UpdateUI()
    {
        //if (monsterModel.Monster != null)
        //{
        //    monsterView.UpdateMonsterName(monsterModel.MonsterName);
        //}
        //else
        //{
        //    Debug.LogError($"[MonsterInfoPresenter] {monsterModel} 없음");
        //}
    }
}
