using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CharacterPositionView : MonoBehaviour
{
    [SerializeField] private CharacterPosPresenter _presenter;

    //화면에 표시될 박스
    [SerializeField] private GameObject[] _characterBox = new GameObject[3];
    bool[] isEnpty = new bool[3];

    //사망 캐릭터 박스 비활성화
    public void Inactive()
    {
        //칸 비활성화
        if (_characterBox[2].activeSelf == true)
        {
            _characterBox[2].SetActive(false);
            Debug.Log($"[CharacterPositionView] : 후열 비활성화");
            return;
        }
        if (_characterBox[1].activeSelf == true)
        {
            _characterBox[1].SetActive(false);
            Debug.Log($"[CharacterPositionView] : 중열 비활성화");
            return;
        }
        if (_characterBox[0].activeSelf == true)
        {
            _characterBox[0].SetActive(false);
            Debug.Log($"[CharacterPositionView] : 전열 비활성화");
        }
    }

    public void IsEnpty(Character deathChar)
    {
        isEnpty[(int)deathChar.Position - 1] = true;
        Debug.Log($"[CharacterPositionView] : 포지션 이넘값{(int)deathChar.Position - 1}");
    }

    //3번 칸부터 데이터 작성
    //[2] = 왼쪽칸
    //[1] = 중앙
    //[0] = 오른쪽칸

    //캐릭터 이동 누가?
    // 1. 오브젝트가 이동
    // 2. UI 이미지가 이동

    //체력 닳을때 체크 (캐릭터 체력 구독, 실행해야 함.)
    public void ReSetPosition(Character character, GameObject[] pos)
    {
    }
}
