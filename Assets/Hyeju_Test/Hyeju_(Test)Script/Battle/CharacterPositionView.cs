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
    public void Inactive(int i)
    {
        //사망 캐릭터 번호와 같은 번호의 박스 비활성화
        int posNum = i;
        //칸 비활성화
        if (posNum == 2)
        {
            _characterBox[2].SetActive(false);
            Debug.Log($"[CharacterPositionView] : 후열 비활성화");
            return;
        }
        if (posNum == 1)
        {
            _characterBox[1].SetActive(false);
            Debug.Log($"[CharacterPositionView] : 중열 비활성화");
            return;
        }
        if (posNum == 0)
        {
            _characterBox[0].SetActive(false);
            Debug.Log($"[CharacterPositionView] : 전열 비활성화");
        }
    }
}
