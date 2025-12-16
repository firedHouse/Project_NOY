using System;
using UnityEngine;

public class CharacterBattleInfoModel : MonoBehaviour
{
    [SerializeField] private string _characterId = "character_id_10001";
    [SerializeField] CharacterData character;
    public event Action HPChanged;

    private float maxHP;

    public CharacterData Character { get => character; set => character = value; }
    public string CharacterName { get => character.characterName; }

    private void Start()
    {
        character = TableManager.Instance.CharacterTable.Get("character_id_10001");
        if (character != null)
        {
            Debug.Log($"[CharacterBattleInfoModel] {character.characterName} {character.HPLevel1}");
            maxHP = character.HPLevel1;
        }
        else
        {
            Debug.LogError($"[CharacterBattleInfoModel] {_characterId} 데이터 가져오기 실패");
        }
    }

    public void IncreaseHP(float amount)
    {
        character.HPLevel1 += amount;
        character.HPLevel1 = Mathf.Clamp(character.HPLevel1, 0, maxHP);

        HPChanged?.Invoke();
    }

    public void DecreaseHP(float amount)
    {
        character.HPLevel1 -= amount;
        character.HPLevel1 = Mathf.Clamp(character.HPLevel1, 0, maxHP);

        HPChanged?.Invoke();
    }
}
