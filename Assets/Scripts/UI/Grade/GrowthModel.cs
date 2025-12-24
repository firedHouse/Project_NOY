using System.Collections.Generic;
using System;
using UnityEngine;

public partial class CharacterListModel : MonoBehaviour
{
    [SerializeField] private CharacterListPresenter presenter;

    #region Field
    private string gradeID;
    private string gradeCharacterID;
    private int attackUP;
    private int hpUP;
    private int needShilling;
    private string gradeInfo;
    private string changeSkin;
    private string desc;

    //업그레이드 성공 여부 > 레벨 1회 상승 후 false
    //처음부터 true인 애들도 있음
    private bool isUpgrade = false;

    #endregion

    #region Property 
    public string GradeID => gradeID;
    public string GradeCharacterID => gradeCharacterID;
    public int AttackUP => attackUP;
    public int HpUP => hpUP;
    public int NeedShilling => needShilling;
    public string GradeInfo => gradeInfo;
    public string ChangeSkin => changeSkin;
    public string Desc => desc;
    #endregion

    public event Action<CharacterListModel> OnUpgrade;
    public event Action OnUnlock;

    private Dictionary<string, (int, int)> gradeData = new Dictionary<string, (int, int)>();

    public void GetGradeData(CharacterListModel model)
    {
        int gradeID = int.Parse(model.gradeID);
        for(int i = 0; i > 9; i++)
        {
            gradeData.Add(model.characterID, (gradeID, gradeID + 1));
            gradeID += 2;
        }
        // 딕셔너리 10001 캐릭터는 50001, 50002의 의 레벨업 정보를 가지고 있다. 정도의 내용
    }

    public void SuccessUpgrade()
    {
        //업그레이드 전달
        isUpgrade = true;
        //레벨 체크 - 버튼 활성/비활성
        level++;

        if (level == 3)
        {
           
        }
    }
}
