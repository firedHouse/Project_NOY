using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public partial class CharacterListModel : CharacterModelBase
{
    [SerializeField] private CharacterListPresenter presenter;

    #region Field

    private int needShilling;
    private GradeData plusStat;

    //업그레이드 성공 여부 > 레벨 1회 상승 후 false
    //처음부터 true인 애들도 있음
    //private bool isUpgrade = false;

    #endregion

    #region Property 
    public string GradeID => gradeID;
    public int NeedShilling { get { return needShilling; } set { needShilling = value; } }
    #endregion

    public void UpdateGradeInfo()
    {
        int currentLevel = UserDataManager.Instance.GetCharacterGrade(characterID);

        level = currentLevel;
    }

    public void GradeCheck(int level)
    {
        plusStat = TableManager.Instance.GetGradeData(characterID, level);
        Debug.Log($"[CharacterListModel] plusStat {plusStat}");
    }

    public void SetNeedShilling()
    {
        int nextLevel = level + 1;

        GradeCheck(nextLevel);
        if (plusStat == null)
        {
            Debug.Log($"[CharacterListModel] {plusStat} 값없음");
            return;
        }
        needShilling = plusStat.needShilling1;
        Debug.Log($"[CharacterListModel] 실링 세팅 : {needShilling}");
    }


    public void SuccessUpgrade()
    {
        UserDataManager.Instance.TryUpgradeCharacter(characterID);

        level++;
        GradeCheck(level);
        needShilling = plusStat.needShilling1;
        UpdateGradeInfo();

        if (presenter == null)
        {
            Debug.Log($"[GrowthView] 프레젠터 null, 새로 참조");
            presenter = GameObject.Find("CharacterListPanel").GetComponent<CharacterListPresenter>();
        }

        //3레벨 달성 시 버튼 비활성화
        if (level == 2)
        {
            presenter.GrowthButton(false);
        }

        SetNeedShilling();
        presenter.ShillingUpdate(this);
        presenter.UpdateCharacterInfo(this);
        presenter.GrowthUp(this);
    }

    public void GetSaveCharacterData()
    {
        level = UserDataManager.Instance.GetCharacterGrade(characterID);
    }
}
