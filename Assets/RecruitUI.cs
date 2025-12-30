using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class RecruitUI : MonoBehaviour
{
    [Header("영입 후보 선택단계 오브젝트")]
    public GameObject selectionPanel; //3장 카드 뜨는 패널
    public Transform selectionContainer; //카드가 생성될 부모
    public GameObject selectionCardPrefab; //선택용 카드 프리팹

    [Header("실제 배치패널")]
    public GameObject placementPanel; //배치 화면 패널
    public Image newCharPortrait; //중앙 신규 캐릭터 아이콘
    public UI_DraggableItem[] teamPortraits; //기존 팀 3개

    [Header("Resources")]
    public Sprite emptySprite; //빈 슬롯 이미지 (투명 하나?)

    [Header("집에간다 스프라이트")]
    public Image dismissPortrait; //집에 간다 위치에 있는 캐릭터 이미지 (인스펙터 연결)

    //내부 데이터
    private string dismissedID = ""; //집에 간다 자리에 있는 캐릭터 ID


    //내부 데이터
    private List<string> tempRoster = new List<string>(); //현재 팀 배치(확정 전까지 계속 바뀌도록)
    private string newCandidateID; //새로 뽑은 캐릭터 ID
    private CharacterData newCandidateData; //그 캐릭터의 데이터

    private void Start()
    {
        selectionPanel.SetActive(false);
        placementPanel.SetActive(false);
        //Invoke("Open", 1.0f);//테스트용
    }

    //리스트업 3장 출력
    public void Open()
    {
        selectionPanel.SetActive(true); //1단계 켜고
        placementPanel.SetActive(false); //2단계 끄기
        GenerateCandidates(); //후보 생성 시작
    }

    //후보 생성 메서드 (이전 수정사항 반영 - 버튼 안전장치 등)
    private void GenerateCandidates()
    {
        //기존 카드 초기화
        foreach (Transform child in selectionContainer)
        {
            Destroy(child.gameObject);
        }

        if (TableManager.Instance == null)
        {
            return;
        }

        //전체 캐릭터 목록 가져오기
        var allChars = TableManager.Instance.CharacterTable.GetAll();

        //유저데이터에 저장해둔 이미 사용되었던 아이디는 제외
        var validPool = allChars.Where(x => !UserDataManager.Instance.IsCharacterUsed(x.characterID)).ToList();

        //3명 랜덤 뽑기
        int count = Mathf.Min(3, validPool.Count);
        for (int i = 0; i < count; i++)
        {
            int rnd = Random.Range(0, validPool.Count);
            CharacterData picked = validPool[rnd];
            validPool.RemoveAt(rnd);

            //카드 생성
            GameObject go = Instantiate(selectionCardPrefab, selectionContainer);

            //NewCharacterBox1 찾기
            Transform boxTr = go.transform.Find("NewCharacterBox1");

            if (boxTr != null)
            {
                //이미지 설정
                Transform imgTr = boxTr.Find("CharacterImage");
                if (imgTr != null)
                {
                    Image img = imgTr.GetComponent<Image>();
                    Sprite sprite = GetCharacterSprite(picked.characterID);
                    if (img != null && sprite != null) img.sprite = sprite;
                }

                //이름 설정
                Transform nameTr = boxTr.Find("NameText");
                if (nameTr != null)
                {
                    Text nameTxt = nameTr.GetComponent<Text>();
                    if (nameTxt != null) nameTxt.text = picked.characterName;
                }

                //역할군 설정
                Transform classTr = boxTr.Find("ClassText");
                if (classTr != null)
                {
                    Text classTxt = classTr.GetComponent<Text>();
                    if (classTxt != null)
                    {
                        //아래에 있는 GetRoleText 함수로 변환해서 넣기
                        classTxt.text = GetRoleText(picked.position);
                    }
                }
            }

            // 버튼 안전장치
            Button btn = go.GetComponent<Button>();
            if (btn == null) btn = go.AddComponent<Button>();

            btn.onClick.AddListener(() => SelectCandidate(picked));
        }
    }

    //후보 중 하나를 클릭하면 2단계 진행
    public void SelectCandidate(CharacterData data)
    {
        newCandidateData = data;
        newCandidateID = data.characterID;

        //현재 팀 정보 복사해오기 (임시 리스트 생성)
        tempRoster.Clear();
        if (BattleManager.Instance.PlayerTeam.Count > 0)
        {
            tempRoster = BattleManager.Instance.PlayerTeam.Select(c => c.UnitID).ToList();
        }
        else
        {
            //테스트용 빈 리스트
            tempRoster = new List<string> { "", "", "" };
        }
        //항상 3칸 유지
        while (tempRoster.Count < 3)
        {
            tempRoster.Add("");
        }

        //2단계 UI 열기
        selectionPanel.SetActive(false);
        placementPanel.SetActive(true);

        RefreshPlacementUI();
    }

    //드래그 앤 드랍 시 리프래쉬
    //UI 갱신 (데이터 -> 화면)
    private void RefreshPlacementUI()
    {
        //중앙 신규 캐릭터 표시
        if (!string.IsNullOrEmpty(newCandidateID))
        {
            newCharPortrait.gameObject.SetActive(true);

            Sprite sprite = GetCharacterSprite(newCandidateID);
            if (sprite != null)
            {
                newCharPortrait.sprite = sprite;
            }

            //99번은 신규캐릭 슬롯
            var drag = newCharPortrait.GetComponent<UI_DraggableItem>();
            if (drag != null)
            {
                drag.SlotIndex = 99;
            }

        }
        else
        {
            newCharPortrait.gameObject.SetActive(false); //비어있음
        }

        //현재 팀 표시
        for (int i = 0; i < 3; i++)
        {
            string id = tempRoster[i];
            if (!string.IsNullOrEmpty(id))
            {
                teamPortraits[i].gameObject.SetActive(true);

                Sprite sprite = GetCharacterSprite(id);
                //Image컴포넌트 찾고 sprite 수정
                Image img = teamPortraits[i].GetComponent<Image>();
                if (sprite != null)
                {
                    img.sprite = sprite;
                }

            }
            else
            {
                teamPortraits[i].gameObject.SetActive(false); //빈칸
            }

            var dragTeam = teamPortraits[i].GetComponent<UI_DraggableItem>();
            if (dragTeam != null)
            {
                dragTeam.SlotIndex = i; //0, 1, 2번 슬롯
            }
        }

        //집에 간다(-1)' 자리 표시 로직
        if (!string.IsNullOrEmpty(dismissedID))
        {
            dismissPortrait.gameObject.SetActive(true);

            //스킨 이미지 가져오기
            Sprite skin = GetCharacterSprite(dismissedID);
            if (skin != null) dismissPortrait.sprite = skin;

            //드래그 가능하도록 세팅
            UI_DraggableItem draggable = dismissPortrait.GetComponent<UI_DraggableItem>();
            if (draggable != null) draggable.SlotIndex = -1; //-1번 슬롯임을 명시
        }
        else
        {
            dismissPortrait.gameObject.SetActive(false); //비어있으면 숨김
        }
    }

    //드래그 앤 드롭 이벤트
    //from 드래그 시작점, to 드랍위치
    public void OnDropItem(int fromIndex, int toIndex)
    {
        //처음 선택된 캐릭터가 들어가는 대기석 드래그 금지
        if (toIndex == 99)
        {
            return;
        }

        //이동할 캐릭터들의 ID 확인
        string fromID = GetIDByIndex(fromIndex);
        string toID = GetIDByIndex(toIndex); //목적지에 이미 누가 있는지 확인

        //대기석(뽑힌놈) 이동 
        if (fromIndex == 99 && toIndex >= 0 && toIndex <= 2)
        {
            //신규 캐릭터를 팀 자리에 넣음
            tempRoster[toIndex] = fromID;

            //원래 팀에 있던 캐릭터 집에 간다로 보내기
            //만약 빈 자리였다면 dismissedID는 비워두기
            dismissedID = toID;

            //신규 대기석은 비움처리
            newCandidateID = "";
        }
        //그 외 이동
        else
        {
            //단순 교체 로직
            SetIDByIndex(toIndex, fromID); //목적지에 온 놈 넣기
            SetIDByIndex(fromIndex, toID); //출발지에 원래 있던 놈 넣기
        }
        RefreshPlacementUI();
    }

    //인덱스로 ID 가져오기
    private string GetIDByIndex(int index)
    {
        if (index == 99) return newCandidateID; //신규 대기석
        if (index == -1) return dismissedID; //집에 간다
        if (index >= 0 && index < 3) return tempRoster[index]; //팀
        return "";
    }

    //인덱스에 ID 넣기
    private void SetIDByIndex(int index, string id)
    {
        if (index == 99) newCandidateID = id;
        else if (index == -1) dismissedID = id;  //집에 간다로 지정
        else if (index >= 0 && index < 3) tempRoster[index] = id;
    }

    //배치 확정버튼
    public void OnClickConfirm()
    {
        //팀 저장
        LobbyManager.Instance.SetTeam(tempRoster.ToArray());

        //Ban List 등록 (이번 게임에 등장했던 모든 애들 등록)
        foreach (string id in tempRoster)
        {
            if (!string.IsNullOrEmpty(id))
            {
                UserDataManager.Instance.AddUsedCharacter(id);
            }
        }

        //방출된 애도 등록
        if (!string.IsNullOrEmpty(dismissedID))
        {
            UserDataManager.Instance.AddUsedCharacter(dismissedID);
        }

        //종료
        placementPanel.SetActive(false);
        StageManager.Instance.OnRewardProcessCompleted();
    }

    //건너뛰기버튼
    public void OnClickSkip()
    {
        //저장 안 하고 그냥 닫음
        selectionPanel.SetActive(false);
        
        RewardUI rewardUI = FindFirstObjectByType<RewardUI>();
        if (rewardUI != null)
        {
            rewardUI.gameObject.SetActive(false);
        }
        StageManager.Instance.OnRewardProcessCompleted();
    }

    private Sprite GetCharacterSprite(string charID)
    {
        //캐릭터 기본 데이터 조회
        CharacterData cData = TableManager.Instance.CharacterTable.Get(charID);
        if (cData == null)
        {
            return null;
        }

        string targetSkinID = cData.characterSkin; //기본 스킨 ID

        //성장 조회
        if (UserDataManager.Instance != null)
        {
            int grade = UserDataManager.Instance.GetCharacterGrade(charID);

            //성장이 되어있다면(1학년 이상), 스킨 교체 여부 확인
            if (grade > 0)
            {
                GradeData gData = TableManager.Instance.GetGradeData(charID, grade);

                //GradeData에 changeSkin 값이 있으면 덮어쓰기
                if (gData != null && !string.IsNullOrEmpty(gData.changeSkin))
                {
                    targetSkinID = gData.changeSkin;
                }
            }
        }

        //결정된 SkinID로 SkinTable 조회
        SkinData sData = TableManager.Instance.SkinTable.Get(targetSkinID);
        if (sData == null)
        {
            return null;
        }
        //리턴
        return ResourceManager.Instance.LoadSprite(sData.skinSprite);
    }

    //역할군(position) => 한글 텍스트 변환용
    private string GetRoleText(int positionIndex)
    {
        //형변환
        CharacterPosition pos = (CharacterPosition)positionIndex;

        switch (pos)
        {
            case CharacterPosition.Tanker: return "탱커"; 
            case CharacterPosition.Dealer: return "딜러";  
            case CharacterPosition.Healer: return "힐러";  
            default: return "기타";
        }
    }
}