using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPosition : MonoBehaviour
{
    [SerializeField] private CharacterData _model;

    [SerializeField] private GameObject[] _characterBox;

    [SerializeField] public Text[] nameText;
    //[SerializeField] public Text[] elementText;
    //[SerializeField] public Text[] positionText;
    //[SerializeField] public Image[] illustImage;

    private void Start()
    {
        
    }

    //체력 닳을때마다 체크 (캐릭터 체력 구독, 실행해야 함.)
    public void ReSetPosition(Queue<CharacterData> aliveList)
    {
        //칸 초기화
        for (int i = 0; i < nameText.Length; i++)
        {
            nameText[i].text = "";
            //칸도 비활성화 시켜야 함.
            _characterBox[i].SetActive(false);
        }

        //사망한 캐릭터 출력 중단
        //살아있는 캐릭터 수만큼 반복
        for (int i = nameText.Length-1; i >= 0; i--)
        {
            

            if (aliveList.Count > 0)
            {
                //3번 칸부터 데이터 작성
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
