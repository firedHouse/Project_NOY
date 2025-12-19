using System;
using UnityEngine;

public class CharacterBattleInfoModel : MonoBehaviour
{
    [SerializeField] private string _characterId = "character_id_10001";
    [SerializeField] public CharacterData character;
    public event Action HPChanged;
    public event Action DataLoaded;

    private float maxHP;

    //Hyeju : 캐릭터 생존상태 이벤트 추가
    public bool isDeath;
    public event Action<string> Death;
    public event Action<string> Alive;

    public CharacterData Character { get => character; set => character = value; }
    public string CharacterName { get => character.characterName; }
    public float MaxHP { get => maxHP; set => maxHP = value; }

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
        //hyeju 
        PlayerSurvialState();
    }

    //Hyeju : 캐릭터의 포지션 값을 전달
    public void PlayerSurvialState()
    {
        if (character.HPLevel1 <= 0)
        {
            Death?.Invoke(character.characterID);
        }
        else
        {
            Alive?.Invoke(character.characterID);
        }
    }
}
