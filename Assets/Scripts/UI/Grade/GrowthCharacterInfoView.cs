using UnityEngine;
using UnityEngine.UI;

public class GrowthCharacterInfoView : MonoBehaviour
{
    [Header("GrowthPresenter")]
    [SerializeField] private GrowthPresenter presenter;

    [Header("고유속성 출력 배열/리소스(물/불/번개/무속)")]
    [SerializeField] private Image elementImage;
    //[SerializeField] private Sprite[] elementImageResources = new Sprite[4];

    [Header("캐릭터 이름/코드네임")]
    [SerializeField] private Text charName;
    [SerializeField] private Text codeName;

    [Header("별 출력 배열/리소스")]
    [SerializeField] private Image[] starImage = new Image[3];
    //[SerializeField] private Sprite yellowStar;
    //[SerializeField] private Sprite grayStar;

    public void UpdateCharacterInfo(CharacterData model)
    {
        //별개수 갱신
        GradeSet(model.level);
        //캐릭터 목록 아이콘 갱신
        //스킬 아이콘 갱신
    }

    //이름, 코드네임
    public void CharacterName(string name, string _codeName)
    {
        charName.text = name;
        codeName.text = _codeName;
    }

    //고유속성 출력 : ElementUI elementUI
    //0불 /1물 / 2번개 / 3무속성
    public void CharacterElement(int element)
    {
        //elementImage.sprite = elementImageResources[element];
    }

    //별 이미지 갱신
    public void GradeSet(int grade)
    {
        //if(yellowStar == null || grayStar ==null)
        //{
        //    Debug.Log("[GrowthView] 별 이미지 정보 없음");
        //    return;
        //}
        //if (grade > 3)
        //{
        //    Debug.Log("[GrowthView] 등급 최대치 넘어감");
        //    return;
        //}

        //등급만큼 노란별
        for (int i = 0; i < grade; i++)
        {
            //starImage[i].sprite = yellowStar;
        }
        //
        for (int i = grade; i < 3; i++)
        {
            //starImage[i].sprite = grayStar;
        }
    }

}
