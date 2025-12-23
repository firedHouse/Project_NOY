using UnityEngine;

public class GrowthPresenter : MonoBehaviour
{
    [SerializeField] private GrowthView growthView;
    [SerializeField] private GrowthSkillView skillView;
    [SerializeField] private GrowthCharacterInfoView infoView;
    //[SerializeField] private Character model;
    [SerializeField] private CharacterData model;

    //임시 변수 : 캐릭터 해금 상태에서 가져와야 함.
    private bool IsUnlock = false;
    private float currentShilling = 0;
    private float upgradeCost;

    //업그레이드 성공 여부 > 모델에서 가져가면 되지 않을까/ 레벨 1회 상승 후 false
    private bool isUpgrade = false;
    public bool IsUpgrade { get { return isUpgrade; } private set { isUpgrade = value; } }
    public CharacterData Model { get { return model; } private set { model = value; } }

    //성장 버튼 클릭 활성화 조건 : 캐릭터 해금
    //버튼 클릭 시 : 실링 확인
    //부족 > 실링부족 패널

    //충분 > 성장 > 학년증가(모델에 전달) > 회색별 > 노란별
    //성장 후 업데이트 사항
    //학년 증가 : 모델에서 할 일
    //다음 성장 필요 실링 출력
    //일러스트 변경

    //최고 학년 > 성장버튼 클릭 비활성화


    private void Start()
    {
        //testModel, 출력대상 : 클릭한 캐릭터(대상 바뀔때마다 실행되어야 함. 이벤트 사용)
        model = TableManager.Instance.CharacterTable.Get("10001");
        //성장이벤트 구독 
        //별갱신 : model.성장시 발동 이벤트 += view.GradeSet
        //별갱신 : model.성장시 발동 이벤트 += skillView.CharacterSkill
        //비용갱신 : model.성장시 발동 이벤트 += view.UpgradeCost
        //캐릭터해금 : model.해금시 발동 이벤트 += CanClick

        Init();
    }

    //레벨에 따라 변경되어야 할 사항
    ////// CharacterData >> Character로 변경되어야 함. ///////
    public void UpdateCharacterInfo(CharacterData model)
    {
        //비용 갱신
        growthView.UpgradeCost(model.level);
        //일러스트 갱신
        growthView.CharacterIllust(model.level);
        //캐릭터 목록 아이콘 갱신
        //스킬 아이콘 갱신
        skillView.CharacterSkill(model);
        //캐릭터 한마디, 상세 설명 텍스트 갱신
        infoView.CharacterName(model.characterName, model.characterCodeName);
    }


    //초기 값
    private void Init()
    {
        //임시 속성값
        //모델에서 고유속성 불러오기
        infoView.CharacterElement(model.elementUI);
        //이름, 코드네임
        infoView.CharacterName(model.characterName , model.characterCodeName);
        //별 이미지 세팅(0:노란별 / 1,2:회색별)
        infoView.GradeSet(model.level);

        //패널 기본값 = false
        growthView.OnNotEnoughShilling(false);
        //버튼클릭 비활성화 // 테스트 임시 활성화
        growthView.ButtonActive(true);
        //일러스트
        growthView.CharacterIllust(model.level);
        Debug.Log($"[GrowthPresenter] : {model.characterName}");
        //한마디, 상세정보
        growthView.CharacterInfo(model.characterInfo, model.characterDialogue);

        skillView.CharacterSkill(model);
    }


    public void SuccessUpgrade()
    {
        //업그레이드 전달
        isUpgrade = true;
        //레벨 체크 - 버튼 활성/비활성
        MaxLevel();
    }

    //캐릭터가 해금 상태이면, 버튼 활성화
    public void CanClick()
    {
        if(IsUnlock == true)
        {
            growthView.ButtonActive(true);
        }
    }

    //보유 실링 체크 : 구매 여부 체크
    public bool IsCanUpgrade()
    {
        if(currentShilling < upgradeCost)
        {
            growthView.OnNotEnoughShilling(true);
            return true;
        }
        return false;
    }

    //최고 학년 체크
    public void MaxLevel()
    {
        if (model.level == 3)
        {
            growthView.ButtonActive(false);
        }
    }
}
