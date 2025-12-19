using UnityEngine;
using System.Collections.Generic;

public class CharacterBattleInfoPresenter : MonoBehaviour
{
    //[SerializeField] private CharacterBattleInfoModel infoModel;
    [SerializeField] private Character infoModel;
    [SerializeField] private CharacterBattleInfoView infoView;
    private CharacterData characterData;
    private string charId = "character_id_10001";
    [SerializeField] private UnitPosition position;

    //Hyeju
    //[SerializeField] private CharacterPosition deathMove;
    private void Start()
    {
        //hyeju
        //infoModel.Death += UpdatePosition;
        //BattleManager.Instance.OnPlayerTurnStart += // 플레이어턴이 되면 실행할 이벤트들 (ex. 스킬 출력)
        Debug.Log("[CharacterBattleInfoPresenter] 초기화");
        Initialize();
    }

    //뷰 초기 설정
    void Initialize()
    {
        // Character.cs에 초기화 메서드가 없어서 임시로 추가해둔 코드
        infoModel.InitializeCharacter(charId, position);
        // infoModel.OnDeath += // 사망 메서드;
        infoModel.OnHpChanged += HandleHpChanged;
        infoModel.OnMarkChanged += HandleMarkChanged;


        //infoView.UpdateCharacterName(infoModel.); // 이름 프로퍼티 못찾음
        infoView.SetMaxHP(infoModel.MaxHP);
        infoView.UpdateSpeed(infoModel.Speed);
    }

    private void HandleHpChanged(BattleUnit character, float hpChangedAmount)
    {
        infoView.UpdateHPBar(hpChangedAmount);
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

