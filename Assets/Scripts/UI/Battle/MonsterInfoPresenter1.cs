using UnityEngine;

partial class MonsterInfoPresenter
{
    public void PosReset(BattleUnit unit)
    {
        Debug.Log($"[CharacterPosPresenter] : 사망자 발생{unit.Position}");

        monsterView.gameObject.SetActive(false);

        //UI 비활성화
        //캐릭터 위치 재설정
    }
}
