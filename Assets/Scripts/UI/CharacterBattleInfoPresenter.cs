using UnityEngine;

public class CharacterBattleInfoPresenter : MonoBehaviour
{
    [SerializeField] private CharacterBattleInfoModel infoModel;
    [SerializeField] private CharacterBattleInfoView infoView;

    void OnEnable()
    {
        infoModel.HPChanged += OnHPChanged;
        UpdateUI();
    }

    private void OnHPChanged()
    {
        UpdateUI();
    }


    private void UpdateUI()
    {
        // info에 접근하는게 안됨
        infoView.UpdateCharacterName(infoModel.CharacterName);
        Debug.Log($"[CharacterBattleInfoPresenter] {infoModel.CharacterName}");
        Debug.Log($"[CharacterBattleInfoPresenter] {infoModel.Character.characterID}");
    }
}
