using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.UIElements;

 class CharacterPositionView : MonoBehaviour
{
    [Header("CharacterBoxFrontPanel : Front-Middle-Roar 순으로 추가")] 
    [SerializeField] private GameObject[] _characterBox = new GameObject[3];

    //사망 캐릭터 박스 비활성화

    public void Inactive(UnitPosition unit)
    {
        //사망 캐릭터 번호와 같은 번호의 박스 비활성화

        switch (unit)
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
