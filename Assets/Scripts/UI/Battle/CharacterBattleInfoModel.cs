using System;
using UnityEngine;

public class CharacterBattleInfoModel : MonoBehaviour
{
    [SerializeField] private string _characterId = "character_id_10001";
    //[SerializeField] public CharacterData character;
    //Hyeju : 테스트용 데이터 배열
    //[SerializeField] public CharacterData[] testCharacter = new CharacterData[3];
    [SerializeField] private UnitPosition position = UnitPosition.Front;
    [SerializeField] public Character character;
    public event Action DataLoaded;

    private float maxHP;

    //Hyeju : 캐릭터 사망 이벤트 추가

    //public CharacterData Character { get => character; set => character = value; }
    //public string CharacterName { get => character.characterName; }
    public float MaxHP { get => maxHP; set => maxHP = value; }

    private void Start()
    {
        //Hyeju
        // testCharacter[0] = character;
        // testCharacter[1] = TableManager.Instance.CharacterTable.Get("character_id_10002");

        character.InitializeCharacter(_characterId, position);
        if (character != null)
        {
           
            // 모델이 데이터를 받아오면 프레젠터에게 알림
            DataLoaded?.Invoke();
        }
        else
        {
            Debug.LogError($"[CharacterBattleInfoModel] {_characterId} 데이터 가져오기 실패");
        }
    }

    //public void IncreaseHP(float amount)
    //{
    //    character.HPLevel1 += amount;
    //    character.HPLevel1 = Mathf.Clamp(character.HPLevel1, 0, maxHP);

    //    HPChanged?.Invoke();
    //}

    //public void DecreaseHP(float amount)
    //{
    //    character.HPLevel1 -= amount;
    //    character.HPLevel1 = Mathf.Clamp(character.HPLevel1, 0, maxHP);

        //hyeju 
        //Die();
    //}

    //Hyeju : 캐릭터의 포지션 값을 전달
    // public void Die()
    // {
    //     if (character.HPLevel1 <= 0)
    //     {
    //         Death?.Invoke(character.characterID);
    //     }
    // }
    //    HPChanged?.Invoke();
    //}
}
