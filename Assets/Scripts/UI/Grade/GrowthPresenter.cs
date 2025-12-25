using UnityEngine;

public partial class CharacterListPresenter : MonoBehaviour
{
    [Header("화면에 표시될 캐릭터 데이터 모델")]
    [SerializeField] private CharacterListModel model;
    [SerializeField] private GrowthView growthView;
    [SerializeField] private GrowthSkillView skillView;

    //임시 변수 : 캐릭터 해금 상태에서 가져와야 함.
    private float currentShilling = 5000;

    //성장 버튼 클릭 활성화 조건 : 캐릭터 해금
    //버튼 클릭 시 : 실링 확인
    //부족 > 실링부족 패널

    //충분 > 성장 > 학년증가(모델에 전달) > 회색별 > 노란별
    //성장 후 업데이트 사항
    //학년 증가 : 모델에서 할 일
    //다음 성장 필요 실링 출력
    //일러스트 변경

    //최고 학년 > 성장버튼 클릭 비활성화


    //private void Start()
    //{
    //    Init();
    //    model.OnUnlock += CanClick;
    //    model.OnUpgrade += UpdateCharacterInfo;
    //}

    //레벨에 따라 변경되어야 할 사항
    public void UpdateCharacterInfo(CharacterListModel model)
    {
        //비용 갱신
        growthView.UpgradeCost(model);
        //일러스트 갱신
        growthView.CharacterIllust(model);
        skillView.CharacterIllust(model);
        //별 갱신
        growthView.GradeSet(model);
        skillView.GradeSet(model);
    }


    //초기 값
    private void Init()
    {
        //모델에서 고유속성 불러오기
        growthView.CharacterElement(model);
        skillView.CharacterElement(model);
        //이름, 코드네임
        growthView.CharacterName(model);
        skillView.CharacterName(model);
        //별 이미지 세팅(0:노란별 / 1,2:회색별)
        growthView.GradeSet(model);
        skillView.GradeSet(model);
        //패널 기본값 = false
        growthView.OnNotEnoughShilling(false);
        //버튼클릭 비활성화 // 테스트 임시 활성화
        growthView.ButtonActive(true);
        //일러스트
        growthView.CharacterIllust(model);
        //한마디, 상세정보
        growthView.CharacterInfo(model);
        //스킬
        skillView.CharacterSkill(model);
    }


    //캐릭터가 해금 상태이면, 버튼 활성화
    public void CanClick(bool active)
    {
        growthView.ButtonActive(active);
    }

    //보유 실링 체크 : 구매 여부 체크
    public void IsCanUpgrade(CharacterListModel model)
    {
        if (currentShilling < model.NeedShilling)
        {
            //업그레이드 정보 전달
            model.SuccessUpgrade(model);
            return;
        }
        //실링부족 > 패널 띄움
        growthView.OnNotEnoughShilling(true);
        return;
    }

}