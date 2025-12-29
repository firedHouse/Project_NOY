using UnityEngine;
using System;

public class MonsterInfoModel : MonoBehaviour
{
    [SerializeField] private string _monsterId; // = "monster_id_10001";
    [SerializeField] private MonsterData monster;
    public event Action MonsterHPChanged;
    public event Action DataLoaded;

    private float maxHP;

    public MonsterData Monster { get => monster; set => monster = value; }

    public string MonsterName { get => monster.monsterName; }
    public float MaxHP { get => maxHP; set => maxHP = value; }

    private void Start()
    {
        monster = TableManager.Instance.MonsterTable.Get(_monsterId);
        if (monster != null)
        {
            maxHP = monster.monsterHP;

            DataLoaded?.Invoke();
        }
        else
        {
            Debug.LogError($"[MonsterInfoModel] {_monsterId} 데이터 가져오기 실패");
        }
    }

    public void IncreaseMonsterHP(float amount)
    {
        monster.monsterHP += amount;
        monster.monsterHP = Mathf.Clamp(monster.monsterHP, 0, maxHP);

        MonsterHPChanged?.Invoke();
    }

    public void DecreaseMonsterHP(float amount)
    {
        monster.monsterHP -= amount;
        monster.monsterHP = Mathf.Clamp(monster.monsterHP, 0, maxHP);

        MonsterHPChanged?.Invoke();
    }
}
