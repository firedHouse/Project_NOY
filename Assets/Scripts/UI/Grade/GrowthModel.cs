using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class CharacterListModel : MonoBehaviour
{
    private CharacterListPresenter presenter;

    #region Field
    private string gradeID;
    private int needShilling;
    private int gradeIDNum;

    //업그레이드 성공 여부 > 레벨 1회 상승 후 false
    //처음부터 true인 애들도 있음
    private bool isUpgrade = false;

    private void Start()
    {
        presenter = GameObject.Find("CharacterListPanel").GetComponent<CharacterListPresenter>();
    }
    #endregion

    #region Property 
    public string GradeID => gradeID;
    public int NeedShilling => needShilling;
    #endregion

    public event Action OnUpgrade;
    public event Action<bool> OnUnlock;


    public void SuccessUpgrade()
    {
        if (presenter == null)
        {
            Debug.Log("프레젠터 비었음");
            return;
        }
        if (presenter.gradeData == null)
        {
            Debug.Log("딕셔너리 비었음");
            return;
        }
        if (presenter.gradeData.Count == 0)
        {
            Debug.Log("딕셔너리 데이터 비었음");
            return;
        }

        //2레벨
        if(level == 0)
        {
            gradeIDNum = presenter.gradeData[int.Parse(characterID)].Item1;
        }
        //3레벨
        else if (level == 1)
        {
            gradeIDNum = presenter.gradeData[int.Parse(characterID)].Item2;
        }

        GradeData plusStat = TableManager.Instance.GradeTable.Get($"{gradeIDNum}");

        //업그레이드 전달
        isUpgrade = true;
        //레벨 체크 - 버튼 활성/비활성

        Debug.Log($"[GrowthView] --- 업그레이드 전 ---");
        Debug.Log($"[GrowthView] --- 레벨 : {level} ---");
        Debug.Log($"[GrowthView] --- 공격력 : {attackLevel1} ---");
        Debug.Log($"[GrowthView] --- HP : {HPLevel1} ---");
        Debug.Log($"[GrowthView] --- 소모실링 : {needShilling} ---");

        level++;
        attackLevel1 += plusStat.attackUP;
        HPLevel1 += plusStat.hpUP;
        needShilling = plusStat.needShilling1;

        Debug.Log($"[GrowthView] --- 업그레이드 완료 ---");
        Debug.Log($"[GrowthView] --- 레벨 : {level} ---");
        Debug.Log($"[GrowthView] --- 공격력 : {attackLevel1} ---");
        Debug.Log($"[GrowthView] --- HP : {HPLevel1} ---");
        Debug.Log($"[GrowthView] --- 소모실링 : {needShilling} ---");

        OnUpgrade.Invoke();

        //3레벨 달성 시 버튼 비활성화
        if (level == 3)
        {
            presenter.CanClick(false);
        }
    }
}
