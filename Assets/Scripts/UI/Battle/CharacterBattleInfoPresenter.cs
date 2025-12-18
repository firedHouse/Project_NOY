using UnityEngine;
using System.Collections.Generic;

public class CharacterBattleInfoPresenter : MonoBehaviour
{
    [SerializeField] private CharacterBattleInfoModel infoModel;
    [SerializeField] private CharacterBattleInfoView infoView;
    private CharacterData characterData;

    //Hyeju
    //[SerializeField] private CharacterPosition deathMove;

    private void Awake()
    {
        infoModel.DataLoaded += OnDataLoaded;
        //hyeju
        //infoModel.Death += UpdatePosition;
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
            Debug.LogError($"[CharacterBattleInfoPresenter] {infoModel} ����");
        }
    }

    //Hyeju : 캐릭터 사망 정보 추가
    // Queue<CharacterData> aliveList = new Queue<CharacterData>();
    // private void UpdatePosition(string characterID)
    // {
    //     aliveList.Clear();
    //     //����� ĳ���� ���� / for�� 3�� ĳ���� �˻� �� ����Ʈ�� �߰�
    //     for (int i = 0; i < 3; i++)
    //     {
    //         if (infoModel.testCharacter[i] == null)
    //         {
    //             continue;
    //         }
    //         else if (infoModel.testCharacter[i].characterID != characterID)
    //         {
    //             //����ִ� ĳ���͸� ����Ʈ�� �߰�
    //             aliveList.Enqueue(infoModel.testCharacter[i]);

    //             Debug.Log($"[CharacterBattleInfoPresenter] : ��ġ �缳��");
    //         }
    //     }

    //     if (aliveList.Count == 0)
    //     {
    //         Debug.Log("[CharacterBattleInfoPresenter] : ����");
    //     }
    //     else if(aliveList.Count > 0)
    //     {
    //         deathMove.ReSetPosition(aliveList);
    //     }
    // }
    
}

