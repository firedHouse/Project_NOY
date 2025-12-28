using System.Reflection;
using UnityEngine;
using System.Collections.Generic;

public class TableManager : Singleton<TableManager>
{
    //테이블 목록
    //추가 시 제일 아래에 이어 작성
    public TableBase<MonsterData> MonsterTable = new TableBase<MonsterData>();
    public TableBase<CharacterData> CharacterTable = new TableBase<CharacterData>();
    public TableBase<SkillData> SkillTable = new TableBase<SkillData>();
    public TableBase<ItemData> ItemTable = new TableBase<ItemData>();
    public TableBase<ItemEquipData> ItemEquipTable = new TableBase<ItemEquipData>();
    public TableBase<ElementalReactionData> ElementalReactionTable = new TableBase<ElementalReactionData>();
    public TableBase<GradeData> GradeTable = new TableBase<GradeData>();
    public TableBase<StageData> StageTable = new TableBase<StageData>();
    public TableBase<MonsterGroupData> MonsterGroupTable = new TableBase<MonsterGroupData>();
    public TableBase<SkinData> SkinTable = new TableBase<SkinData>();


    protected override void Awake()
    {
        base.Awake();
        LoadAllData();
    }

    private void LoadAllData()
    {
        //TableManager의 모든 public 변수를 가져옴
        FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);

        foreach (FieldInfo field in fields)
        {
            //변수 타입이 TableBase로 시작하는 것만
            if (field.FieldType.Name.Contains("TableBase"))
            {
                //변수 이름을 가져오기
                //12.15 수정, 변수명에서 Table 떼고 파일 찾기
                string fileName = field.Name.Replace("Table", "");
                string path = $"Data/{fileName}"; // 결과: "Data/MonsterTable"

                //해당 변수의 인스턴스를 가져옴
                object tableInstance = field.GetValue(this);

                //Load 함수 실행
                MethodInfo loadMethod = field.FieldType.GetMethod("Load");

                if (loadMethod != null)
                {
                    loadMethod.Invoke(tableInstance, new object[] { path });
                    Debug.Log($"{fileName} 로드 완료 (경로: {path})");
                }
            }
        }

        Debug.Log("[TableManager] 데이터 리플렉션 완료");
    }


    //12.28 캐릭터의 ID와 학년으로 성장 데이터를 찾는 메서드
    public GradeData GetGradeData(string charID, int targetGradeLevel)
    {
        //1학년 이하는 성장 없음
        if (targetGradeLevel <= 0)
        {
            return null;
        }

        List<GradeData> foundGrades = new List<GradeData>();

        //GradeTable GetAll
        List<GradeData> allGrades = GradeTable.GetAll();

        
        foreach (var data in allGrades)
        {
            //내 캐릭터 아이디와 동일한 애만 집어넣기
            if (data.characterID == charID)
            {
                foundGrades.Add(data);
            }
        }
        //ID 문자열 비교해서 GradeID 기준 오름차순 정렬
        foundGrades.Sort((a, b) => string.Compare(a.gradeID, b.gradeID));

        //2학년은 리스트0, 3학년은 1이니까 -1
        int listIndex = targetGradeLevel - 1;

        if (listIndex >= 0 && listIndex < foundGrades.Count)
        {
            return foundGrades[listIndex];
        }
        return null;

    }
}