using UnityEditor.U2D.Animation;
using UnityEngine;

public class MonsterInfoPresenter : MonoBehaviour
{
    [SerializeField] private MonsterInfoModel monsterModel;
    [SerializeField] private MonsterInfoView monsterView;
    private MonsterData monsterData;

    private void Awake()
    {
        monsterModel.DataLoaded += OnDataLoaded;
    }

    void Initialize()
    {
        monsterData = monsterModel.Monster;
        if (monsterData != null)
        {
            Debug.Log($"[MonsterInfoPresenter] characterData 내부 데이터 불러오기 성공");

            monsterModel.MonsterHPChanged += OnHPChanged;
            monsterView.UpdateMonsterName(monsterModel.Monster.monsterName);
            monsterView.UpdateElement(monsterModel.Monster.elementType);
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
        //UpdateUI();
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
