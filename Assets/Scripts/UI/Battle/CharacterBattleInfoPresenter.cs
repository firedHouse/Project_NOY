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
    private void Start()
    {
        BattleManager.Instance.OnBattleSetted += Initialize;

        //hyeju
        //infoModel.Death += UpdatePosition;
        //BattleManager.Instance.OnPlayerTurnStart += // 플레이어턴이 되면 실행할 이벤트들 (ex. 스킬 출력)

        //Debug.Log("[CharacterBattleInfoPresenter] 초기화");
        //Initialize();
    }

    //뷰 초기 설정
    public void Initialize()
    {
        characterModel = BattleManager.Instance.PlayerTeam[(int)position];
        // infoModel.OnDeath += // 사망 메서드;
        characterModel.OnHpChanged += HandleHpChanged;
        characterModel.OnMarkChanged += HandleMarkChanged;


        characterView.UpdateCharacterName(characterModel.UnitName);
        characterView.SetMaxHP(characterModel.MaxHP);
        characterView.UpdateSpeed(characterModel.Speed);
        //characterView.UpdatePosition(characterModel.Position);
    }

    private void HandleHpChanged(BattleUnit character, float hpChangedAmount)
    {
        characterView.UpdateHPBar(hpChangedAmount);
    }

    private void HandleMarkChanged(BattleUnit character, ElementType elementType)
    {
        //infoView.
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

