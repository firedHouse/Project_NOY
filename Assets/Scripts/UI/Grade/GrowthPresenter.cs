using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public partial class CharacterListPresenter : CharacterPresenterBase
{
    [SerializeField] protected GrowthView growthView;


    //성장 버튼 클릭 활성화 조건 : 캐릭터 해금
    //버튼 클릭 시 : 실링 확인
    //부족 > 실링부족 패널

    //충분 > 성장 > 학년증가(모델에 전달) > 회색별 > 노란별
    //성장 후 업데이트 사항
    //학년 증가 : 모델에서 할 일
    //다음 성장 필요 실링 출력
    //일러스트 변경

    //최고 학년 > 성장버튼 클릭 비활성화

    //레벨에 따라 변경되어야 할 사항
    public override void UpdateCharacterInfo(CharacterListModel model)
    {
        //비용 갱신
        growthView.UpgradeCost(model);
        //일러스트 갱신
        growthView.CharacterIllust(model);
        //별 갱신
        growthView.GradeSet(model);
        //버튼갱신
        CanClick(model);
    }


    //초기 값
    public override void Init(CharacterListModel model)
    {
        Debug.Log("[GrowthPresenter] Init");
        //모델에서 고유속성 불러오기
        growthView.CharacterElement(model);
        //이름, 코드네임
        growthView.CharacterName(model);
        //별 이미지 세팅(0:노란별 / 1,2:회색별)
        growthView.GradeSet(model);
        //패널 기본값 = false
        growthView.OnNotEnoughShilling(false);
        //버튼클릭 비활성화 // 테스트 임시 활성화
        growthView.ButtonActive(model.IsUnlocked);
        CanClick(model);
        //일러스트
        growthView.CharacterIllust(model);
        //한마디, 상세정보
        growthView.CharacterInfo(model);
        //실링값 부여
        model.SetNeedShilling();
        //실링 금액업데이트
        growthView.UpgradeCost(model);
    }


    //캐릭터가 해금 상태이면, 버튼 활성화
    // jihoo : 잠금 상태일 때 버튼이 비활성화 되도록 변경
    public void CanClick(CharacterListModel model)
    {
        growthView.ButtonActive(model.IsUnlocked);
    }

    public void GrowthButton(bool active)
    {
        Debug.Log($"[CharacterListPresenter] 성장 버튼 활성화 여부 {active}");
        growthView.ButtonActive(active);
    }



    //보유 실링 체크 : 구매 여부 체크
    public void IsCanUpgrade()
    {
        if (ShillingManager.Instance.OutGameShilling >= model.NeedShilling)
        {
            //실링 차감 매서드 호출, 비용만큼 차감
            //shillingModel.Decrease(model.NeedShilling);
            //업그레이드 정보 전달
            model.SuccessUpgrade();
            shillingPresenter?.UpdateUI();
            return;
        }
        //실링부족 > 패널 띄움
        growthView.OnNotEnoughShilling(true);
        Debug.Log($"[CharacterListPresenter] 패널 띄움");
    }

    public void ShillingUpdate(CharacterListModel model)
    {
        growthView.UpgradeCost(model);
    }

}