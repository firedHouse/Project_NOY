using UnityEngine;
using System.Collections.Generic;

public class CharacterBattleInfoPresenter : MonoBehaviour
{
    [SerializeField] private CharacterBattleInfoModel infoModel;
    [SerializeField] private CharacterBattleInfoView infoView;
    private CharacterData characterData;

    //Hyeju
    [SerializeField] private CharacterPosition characterPosition;

    private void Awake()
    {
        infoModel.DataLoaded += OnDataLoaded;
        //hyeju
        infoModel.Death += AddDeathList;
        infoModel.Alive += AddDeathList;
    }

    void Initialize()
    {
        characterData = infoModel.character;
        if(characterData != null)
        {
            Debug.Log($"[CharacterBattleInfoPresenter] characterData 내부 데이터 불러오기 성공");
            infoModel.HPChanged += OnHPChanged;
            infoView.SetMaxHP(infoModel.MaxHP);
            UpdateUI();
        }
        else
        {
            Debug.Log($"[CharacterBattleInfoPresenter] characterData 내부 데이터 비어있음");
        }
    }

    // 모델에 데이터가 들어오면 프레젠터에서도 가져온다
    private void OnDataLoaded()
    {
        Initialize();
    }

    private void OnHPChanged()
    {
        infoView.UpdateHPBar(infoModel.Character.HPLevel1);
    }

    private void IncreaseHP(float hpChangeAmount)
    {
        infoModel.IncreaseHP(hpChangeAmount);
    }

    private void DecreaseHP(float hpChangeAmount)
    {
        infoModel.DecreaseHP(hpChangeAmount);
    }

    private void UpdateUI()
    {
        if(infoModel.Character != null)
        {
            infoView.UpdateCharacterName(infoModel.CharacterName);
        }
        else
        {
            Debug.LogError($"[CharacterBattleInfoPresenter] {infoModel} 없음");
        }
    }

    //Hyeju :
    public Queue<CharacterData> aliveList = new Queue<CharacterData>();
    public Queue<CharacterData> deathList = new Queue<CharacterData>();
    private void AddDeathList(string characterID)
    {
        for (int i = 0; i < 3; i++)
        {
            if (infoModel.Character == null)
            {
                continue;
            }
            else if (infoModel.Character.characterID != characterID)
            {
                //
                aliveList.Enqueue(infoModel.Character);

                Debug.Log($"[CharacterBattleInfoPresenter] : 큐에 {infoModel.Character} 추가");
            }
        }

        if (aliveList.Count == 0)
        {
            Debug.Log("[CharacterBattleInfoPresenter] : 생존 캐릭터 없음");
        }
        else if (aliveList.Count > 0)
        {
            characterPosition.ReSetPosition(aliveList);
        }
    }

}

