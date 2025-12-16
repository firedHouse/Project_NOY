using UnityEngine;

public class TestScripts : MonoBehaviour
{
    void Start()
    {
        var monster = TableManager.Instance.MonsterTable.Get("monster_0001");
        if (monster != null)
        {
            Debug.Log($"[�׽�Ʈ��ũ��Ʈ] �̸�: {monster.monsterName}, HP: {monster.monsterHP}, ���ݷ�: {monster.monsterAttack}");
        }
        else
        {
            Debug.LogError("�׽�Ʈ����");
        }
    }
}
