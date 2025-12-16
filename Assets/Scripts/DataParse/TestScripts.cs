using UnityEngine;

public class TestScripts : MonoBehaviour
{
    void Start()
    {
        var monster = TableManager.Instance.MonsterTable.Get("monster_0001");
        if (monster != null)
        {
            Debug.Log($"[테스트스크립트] 이름: {monster.monsterName}, HP: {monster.monsterHP}, 공격력: {monster.monsterAttack}");
        }
        else
        {
            Debug.LogError("테스트실패");
        }
    }
}
