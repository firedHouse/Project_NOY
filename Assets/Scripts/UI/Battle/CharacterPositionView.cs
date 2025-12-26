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

    public void Inactive(int basePosNum)
    {

        switch (basePosNum)
        {
            case 0:
                {
                    _characterBox[0].SetActive(false);
                    Debug.Log($"[CharacterPositionView] : {1}번 박스 비활성화");
                }
                break;

            case 1:
                {
                    _characterBox[1].SetActive(false);
                    Debug.Log($"[CharacterPositionView] : {2}번 박스 비활성화");
                }
                break;

            case 2:
                {
                    _characterBox[2].SetActive(false);
                    Debug.Log($"[CharacterPositionView] : {3}번 박스 비활성화");
                }
                    break;
        }
    }
}
