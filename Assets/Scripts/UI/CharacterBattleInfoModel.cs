using System;
using UnityEngine;

public class CharacterBattleInfoModel : MonoBehaviour
{
    [SerializeField] private string _characterId = "character_id_10001";
    [SerializeField] public CharacterData character;
    public event Action HPChanged;
    public event Action DataLoaded;

    private float maxHP;

    public CharacterData Character { get => character; set => character = value; }
    public string CharacterName { get => character.characterName; }

    private void Start()
    {
        character = TableManager.Instance.CharacterTable.Get(_characterId);
        if (character != null)
        {
            maxHP = character.HPLevel1;
            // 모델이 데이터를 받아오면 프레젠터에게 알림
            DataLoaded?.Invoke();
        }
        else
        {
            Debug.LogError($"[CharacterBattleInfoModel] {_characterId} 데이터 가져오기 실패");
        }
    }

    public void Increase(float amount)
    {
        character.HPLevel1 += amount;
        character.HPLevel1 = Mathf.Clamp(character.HPLevel1, 0, maxHP);

        HPChanged?.Invoke();
    }

    public void Decrease(float amount)
    {
        character.HPLevel1 -= amount;
        character.HPLevel1 = Mathf.Clamp(character.HPLevel1, 0, maxHP);

        HPChanged?.Invoke();
    }
}
