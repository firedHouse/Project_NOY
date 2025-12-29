using UnityEngine;

partial class MonsterInfoPresenter
{
    public void DeathMonster()
    {
        currentCount = Mathf.Min(3, BattleManager.Instance.EnemyTeam.Count);

        //n번 위치에 있던 캐릭터
        for (int i = 0; i < currentCount; i++)
        {
            _monster[i] = BattleManager.Instance.EnemyTeam[i];

            monsterBoxPos[_monster[i]] = i;

            Debug.Log($"[CharacterPosPresenter] : 저장개수 {monsterBoxPos.Keys.Count}");
        }
    }

    public void PosReset(BattleUnit unit)
    {
        Debug.Log($"[CharacterPosPresenter] : 사망자 발생{unit.Position}");

        monsterMoveView.Inactive(monsterBoxPos[unit]);
        //UI 비활성화
        //캐릭터 위치 재설정
    }
}
