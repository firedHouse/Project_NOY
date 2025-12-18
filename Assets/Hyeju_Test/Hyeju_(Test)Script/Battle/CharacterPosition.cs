using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterPosition : MonoBehaviour
{
    [SerializeField] private CharacterData _model;

    [SerializeField] public Text text0;
    [SerializeField] public Image Image0;

    [SerializeField] public Text text1;
    [SerializeField] public Image Image1;

    [SerializeField] public Text text2;
    [SerializeField] public Image Image2;


    private void Awake()
    {
    }

    private void Start()
    {
        
    }

    //체력 닳을때마다 체크 (캐릭터 체력 구독, 실행해야 함.)
    public void RemovePosition(int characterPosition)
    {
        Debug.Log($"[사망, 위치변경] : {characterPosition} 에서 변경");
    }

    public void ResetPosition(int characterPosition)
    {
        //플레이어가 배치한 캐릭터의 정보를 알아야 함.
        switch(characterPosition)
        {

        }
    }
}
