using System;
using UnityEngine;

// 캐릭터 슬롯과 상세 정보 UI에 사용할 모델
// 필요한 정보: 아이디, 해금 여부, 이름, 코드네임, 포지션, 속성, 레벨(학년), 대사, 세부 설정, 스킨, 
// 팀구성 팝업만 사용할 정보 : 스킬(팀구성), 
//[Serializable]
public partial class CharacterListModel : CharacterModelBase
{

    #region Field
    [Header("해금을 위한 임시 실링요구량")]
    [SerializeField] private int unlockShilling = 2000;

    #endregion

    #region Property
    public int UnlockShilling { get => unlockShilling; private set => unlockShilling = value; }
    
    #endregion

    // 해금 처리 
    public void Unlock()
    {
        if (!isUnlocked)
        {
            Debug.Log($"[CharacterListModel] {isUnlocked} > 해금");
            Debug.Log($"[CharacterListModel] 필요한 실링 : ");
            // 해금을 위해 실링 빼가는 처리
            ShillingManager.Instance.SpendShilling(unlockShilling);
            isUnlocked = true;
        }
        else
        {
            Debug.Log($"[CharacterListModel] {isUnlocked} > 이미 해금됨!");
        }
    }


}
