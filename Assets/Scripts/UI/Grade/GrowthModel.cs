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
    private GradeData plusStat;

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
    public int NeedShilling { get { return needShilling; } set { needShilling = value; } }
    #endregion

    public event Action OnUpgrade;
    public event Action<bool> OnUnlock;

    
    
    public void GradeCheck()
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
        if (level == 0)
        {
            gradeIDNum = presenter.gradeData[int.Parse(characterID)].Item1;
        }
        //3레벨
        else if (level == 1)
        {
            gradeIDNum = presenter.gradeData[int.Parse(characterID)].Item2;
        }

        plusStat = TableManager.Instance.GradeTable.Get($"{gradeIDNum}");

    }

    public void SetNeedShilling()
    {
        GradeCheck();
        needShilling = plusStat.needShilling1;
        Debug.Log($"[CharacterListModel] 실링 세팅 : {needShilling}");
    }


    public void SuccessUpgrade()
    {
        //학년 체크
        GradeCheck();
    
        //업그레이드 전달
        isUpgrade = true;
        //레벨 체크 - 버튼 활성/비활성

        Debug.Log($"[GrowthView] --- 업그레이드 전 ---");
        Debug.Log($"[GrowthView] --- 레벨 : {level} ---");
        Debug.Log($"[GrowthView] --- 공격력 : {attackLevel1} ---");
        Debug.Log($"[GrowthView] --- HP : {HPLevel1} ---");
        Debug.Log($"[GrowthView] --- 소모실링 : {needShilling} ---");

        level++;
        AttackLevel += plusStat.attackUP;
        HpLevel += plusStat.hpUP;
        needShilling = plusStat.needShilling1;

        Debug.Log($"[GrowthView] --- 업그레이드 목록---");
        Debug.Log($"[GrowthView] --- 레벨 : {level} ---");
        Debug.Log($"[GrowthView] --- 공격력 : {attackLevel1} ---");
        Debug.Log($"[GrowthView] --- HP : {HPLevel1} ---");
        Debug.Log($"[GrowthView] --- 소모실링 : {needShilling} ---");
        Debug.Log($"[GrowthView] --- 업그레이드 완료 ---");

        //3레벨 달성 시 버튼 비활성화
        if (level == 2)
        {
            presenter.CanClick(false);
        }

        OnUpgrade.Invoke();
    }
}
