using UnityEngine;

public class CharacterBattleInfoPresenter : MonoBehaviour
{
    [SerializeField] private CharacterBattleInfoModel infoModel;
    [SerializeField] private CharacterBattleInfoView infoView;
    private CharacterData characterData;

    private void Awake()
    {
        infoModel.DataLoaded += OnDataLoaded;
    }

    void Initialize()
    {
        characterData = infoModel.character;
        if(characterData != null)
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

    private void DecreaseHP(float hpChangeAmount)
    {
        infoModel.Decrease(hpChangeAmount);
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
}
