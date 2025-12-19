using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterPositionView : MonoBehaviour
{
    [SerializeField] private CharacterPosPresenter _presenter;

    //화면에 표시될 박스
    [SerializeField] private GameObject[] _characterBox = new GameObject[3];


    

    //칸 전체 초기화 : 노출된 정보 전체 제거
    //사망한 캐릭터만 모아서 호출해도 될 것 같음.
    //public void InitBox(List<(Character, int)> DeatList)
    //{
    //    // 2번칸부터 DeatList.Count 수 만큼 반복
    //    int cycle = 3 - DeatList.Count;

    //    for (int i = 2; i >= cycle; i--)
    //    {
    //        //칸 비활성화
    //        _characterBox[i].SetActive(false);
    //        DeatList[i].Item1.gameObject.SetActive(false);
    //    }
    //}

    //3번 칸부터 데이터 작성
    //[2] = 왼쪽칸
    //[1] = 중앙
    //[0] = 오른쪽칸

    //캐릭터 이동 누가?
    // 1. 오브젝트가 이동
    // 2. UI 이미지가 이동

    //체력 닳을때 체크 (캐릭터 체력 구독, 실행해야 함.)
    //public void ReSetPosition(List<(Character, int)> aliveList)
    //{
    //    bool[] isEnpty = new bool[3];
    //    for(int i = 0; i > 3; i++)
    //    {
    //        switch(aliveList[i].Item2)
    //        {
    //            //전열 : 앞으로 이동하지 않는다.
    //            case 0:
    //                isEnpty[0] = false;
    //                break;
    //            //중앙 : 앞에 비어있지 않으면 앞으로 이동
    //            case 1:
    //                if (isEnpty[0] == true)
    //                {
    //                    aliveList[i].Item1.Position = 
    //                }
    //                break;
    //            //후열 : 앞에 빈만큼 이동
    //            case 2:
    //                break;
    //        }

    //        if(aliveList[i].Item2 == 1 && )

    //            Debug.Log($"[CharacterPosition] : 위치 재설정");
    //    }
    //}
}
