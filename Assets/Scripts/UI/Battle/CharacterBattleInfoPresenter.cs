using UnityEngine;
using System.Collections.Generic;

public class CharacterBattleInfoPresenter : MonoBehaviour
{
    [SerializeField] private CharacterBattleInfoModel infoModel;
    [SerializeField] private CharacterBattleInfoView infoView;
    private CharacterData characterData;

    //Hyeju : 추가
    [SerializeField] private CharacterPosition deathMove;

    private void Awake()
    {
        infoModel.DataLoaded += OnDataLoaded;
        //hyeju : 이벤트 추가
        infoModel.Death += UpdatePosition;
    }

    void Initialize()
    {
        characterData = infoModel.character;
        if (characterData != null)
        {
            Debug.Log($"[CharacterBattleInfoPresenter] characterData 내부 데이터 불러오기 성공");

            infoModel.HPChanged += OnHPChanged;
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
        UpdateUI();
    }

    private void IncreaseHP(float hpChangeAmount)
    {
        infoModel.Increase(hpChangeAmount);
    }

    //Hyeju : 테스트 위해서 public으로 임시 변경
    public void DecreaseHP(float hpChangeAmount)
    {
        infoModel.Decrease(hpChangeAmount);
    }

    private void UpdateUI()
    {
        if (infoModel.Character != null)
        {
            infoView.UpdateCharacterName(infoModel.CharacterName);
        }
        else
        {
            Debug.LogError($"[CharacterBattleInfoPresenter] {infoModel} 없음");
        }
    }

    //Hyeju : 캐릭터 사망 정보 추가
    Queue<CharacterData> aliveList = new Queue<CharacterData>();
    private void UpdatePosition(string characterID)
    {
        aliveList.Clear();
        //사망한 캐릭터 제외 / for로 3개 캐릭터 검사 후 리스트에 추가
        for (int i = 0; i < 3; i++)
        {
            if (infoModel.testCharacter[i] == null)
            {
                continue;
            }
            else if (infoModel.testCharacter[i].characterID != characterID)
            {
                //살아있는 캐릭터면 리스트에 추가
                aliveList.Enqueue(infoModel.testCharacter[i]);

                Debug.Log($"[CharacterBattleInfoPresenter] : 위치 재설정");
            }
        }

        if (aliveList.Count == 0)
        {
            Debug.Log("[CharacterBattleInfoPresenter] : 전멸");
        }
        else if(aliveList.Count > 0)
        {
            deathMove.ReSetPosition(aliveList);
        }
    }
    
}

