using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPositionView : MonoBehaviour
{
    [SerializeField] private CharacterData _model;

    [SerializeField] private GameObject[] _characterBox = new GameObject[3];

    [SerializeField] public Text[] nameText = new Text[3];
    //[SerializeField] public Text[] elementText;
    //[SerializeField] public Text[] positionText;
    //[SerializeField] public Image[] illustImage;

    private void Start()
    {
        
    }

    //체력 닳을때마다 체크 (캐릭터 체력 구독, 실행해야 함.)
    public void ReSetPosition(Queue<CharacterData> aliveList)
    {
        //사망한 캐릭터 출력 중단
        //칸 전체 초기화
        for (int i = 0; i < nameText.Length; i++)
        {
            nameText[i].text = "";
            //칸도 비활성화 시켜야 함.
            _characterBox[i].SetActive(false);
        }

        //칸수만큼 반복
        for (int i = 3; i > 0; i--)
        {
            //살아있는 캐릭터 수만큼 반복
            if (aliveList.Count > 0)
            {
                //3번 칸부터 데이터 작성
                //[0] = 왼쪽칸
                //[1] = 중앙
                //[2] = 오른쪽칸

                _characterBox[i].SetActive(true);
                nameText[i].text = aliveList.Dequeue().characterCodeName;
            }
            else
            {
                nameText[i].text = "";
            }
        }

        Debug.Log($"[CharacterPosition] : 위치 재설정");
    }
}
