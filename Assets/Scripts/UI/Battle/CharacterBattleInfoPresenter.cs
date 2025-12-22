using UnityEngine;
using System.Collections.Generic;

public class CharacterBattleInfoPresenter : MonoBehaviour
{
    [SerializeField] private Character characterModel;
    [SerializeField] private CharacterBattleInfoView characterView;
    //private CharacterData characterData;
    [SerializeField] private UnitPosition position;

    //Hyeju
    //[SerializeField] private CharacterPosition deathMove;
    private void Awake()
    {

        //Debug.Log("[CharacterBattleInfoPresenter] Awake");
        BattleManager.Instance.OnBattleSetted += Initialize;

        //hyeju
        //infoModel.Death += UpdatePosition;
        //BattleManager.Instance.OnPlayerTurnStart += // 플레이어턴이 되면 실행할 이벤트들 (ex. 스킬 출력)
    }

    //뷰 초기 설정
    public void Initialize()
    {
        Debug.Log("[CharacterBattleInfoPresenter] 초기화");
        characterModel = BattleManager.Instance.PlayerTeam[(int)position];
        characterModel.OnDeath += HandleDeath;
        characterModel.OnDeath += HandlePositionChanged;
        characterModel.OnHpChanged += HandleHpChanged;
        //characterModel.OnMarkChanged += HandleMarkChanged;


        characterView.SetMaxHP(characterModel.MaxHP);
        characterView.SetSkillList(characterModel.Skills);
        characterView.UpdateCharacterName(characterModel.UnitName);
        characterView.UpdateSpeed(characterModel.Speed);
        characterView.UpdatePosition(characterModel.Position);
    }

    private void HandleHpChanged(BattleUnit character, float hpChangedAmount)
    {
        characterView.UpdateHPBar(hpChangedAmount);
    }

    // 속성 표시 변경 데이터인데 속성이 데이터 테이블에 없어서 지금은 사용 안함
    //private void HandleMarkChanged(BattleUnit character, ElementType elementType)
    //{
    //}

    private void HandleDeath(BattleUnit unit)
    {
        BattleManager.Instance.OnUnitDead(unit);
    }

    private void HandlePositionChanged(BattleUnit unit)
    {
        characterView.UpdatePosition(position);
    }

    //Hyeju :
    // Queue<CharacterData> aliveList = new Queue<CharacterData>();
    // private void UpdatePosition(string characterID)
    // {
    //     aliveList.Clear();
    //     //
    //     for (int i = 0; i < 3; i++)
    //     {
    //         if (infoModel.testCharacter[i] == null)
    //         {
    //             continue;
    //         }
    //         else if (infoModel.testCharacter[i].characterID != characterID)
    //         {
    //             //
    //             aliveList.Enqueue(infoModel.testCharacter[i]);

    //             Debug.Log($"[CharacterBattleInfoPresenter] : 큐에 {infoModel.testCharacter[i]} 추가");
    //         }
    //     }

    //     if (aliveList.Count == 0)
    //     {
    //         Debug.Log("[CharacterBattleInfoPresenter] : 생존 캐릭터 없음");
    //     }
    //     else if(aliveList.Count > 0)
    //     {
    //         deathMove.ReSetPosition(aliveList);
    //     }
    // }

}

