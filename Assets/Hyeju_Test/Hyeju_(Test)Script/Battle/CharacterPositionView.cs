using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.UIElements;

 class CharacterPositionView
{
    //화면에 표시될 박스
    [SerializeField] private GameObject[] _characterBox = new GameObject[3];
    [SerializeField] private GameObject[] _pos = new GameObject[3];
    UnitPosition currentPosition;

    //사망 캐릭터 박스 비활성화

    public void Inactive(BattleUnit unit)
    {
        //사망 캐릭터 번호와 같은 번호의 박스 비활성화

        currentPosition = unit.Position;
        switch (currentPosition)
        {
            case UnitPosition.Front:
                {
                    _characterBox[0].SetActive(false);
                    Debug.Log($"[CharacterPositionView] : 전열 비활성화");
                }
                break;

            case UnitPosition.Mid:
                {
                    _characterBox[1].SetActive(false);
                    Debug.Log($"[CharacterPositionView] : 중열 비활성화");
                }
                break;

            case UnitPosition.Back:
                {
                    _characterBox[2].SetActive(false);
                    Debug.Log($"[CharacterPositionView] : 후열 비활성화");
                }
                    break;
        }
    }
}
