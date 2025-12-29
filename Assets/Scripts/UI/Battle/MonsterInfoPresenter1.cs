using UnityEngine;

partial class MonsterInfoPresenter
{
    public void DeathCharacter()
    {
        monsterModel.OnDeath += PosReset;
    }

    public void PosReset(BattleUnit unit)
    {
        //사망 캐릭터 체크 후 비활성화 번호 전달
        Debug.Log($"[CharacterPosPresenter] : 사망자 발생{unit}");
        //사망 캐릭터 리스트에서 제거
        BattleManager.Instance.OnUnitDead(unit);
        //포지션 업데이트
        // monsterView.UpdateMonsterClass(unit.Position);

        //UI 비활성화
        monsterMoveView.MonsterInactive(unit.Position);
        //캐릭터 위치 재설정
    }
}
