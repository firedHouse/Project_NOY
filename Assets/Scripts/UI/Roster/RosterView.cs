using UnityEngine;
using UnityEngine.UI;

public class RosterView : MonoBehaviour
{
    [Header("캐릭터 일러스트")]
    [SerializeField] private Image[] characterIllust = new Image[3];
    //[SerializeField] private Image[] characterIllust = new Image[3];

    [Header("캐릭터 이름")]
    [SerializeField] private Text[] TextcharacterName = new Text[3];

    [Header("캐릭터 클래스")]
    [SerializeField] private Image[] characterClass = new Image[3];
    [SerializeField] private Sprite[] classIcon = new Sprite[3];

    [Header("캐릭터 속성")]
    [SerializeField] private Image[] characterElement = new Image[3];
    [SerializeField] private Sprite[] elementIcon = new Sprite[4];

    [Header("선택 캐릭터")]
    [SerializeField] private Image topCharacterImage;
    [SerializeField] private Image bottomCharacterImage;
    [SerializeField] private GameObject topCharacterObject;
    [SerializeField] private GameObject bottomCharacterObject;

    private void Start()
    {
        topCharacterObject.SetActive(false);
        bottomCharacterObject.SetActive(true);
    }

    public void TopImageActive(bool isActive)
    {
        topCharacterObject.SetActive(isActive);
    }

    public void BottomImageActive(bool isActive)
    {
        bottomCharacterObject.SetActive(isActive);
    }



    public void topImage(RosterModel model, int i)
    {
        ModelNullCheck(model);
        if (topCharacterImage == null)
        {
            Debug.Log("[RosterView] 상단 이미지 오브젝트 정보 없음");
            return;
        }

        topCharacterImage.sprite = Resources.Load<Sprite>(model.ChracterIllust[i]);
    }

    public void bottomImage(RosterModel model, int i)
    {
        ModelNullCheck(model);
        if (topCharacterImage == null)
        {
            Debug.Log("[RosterView] 하단 이미지 오브젝트 정보 없음");
            return;
        }

        bottomCharacterImage.sprite = Resources.Load<Sprite>(model.ChracterIllust[i]);
    }

    public void CharacterName(RosterModel model)
    {
        ModelNullCheck(model);

        if (TextcharacterName[0] == null)
        {
            Debug.Log("[RosterView] 이름 오브젝트 정보 없음");
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            TextcharacterName[i].text = model.ChracterName[i];
        }
    }

    public void CharacterIllust(RosterModel model)
    {
        ModelNullCheck(model);

        if (characterIllust[0] == null)
        {
            Debug.Log("[RosterView] 캐릭터 이미지 오브젝트 정보 없음");
            return;
        }


        for (int i = 0; i < 3; i++)
        {
            characterIllust[i].sprite = Resources.Load<Sprite>(model.ChracterIllust[i]);
        }
    }

    //클래스
    public void CharacterIcon(RosterModel model)
    {
        ModelNullCheck(model);

        if (characterElement[0] == null)
        {
            Debug.Log("[RosterView] 캐릭터 고유속성 오브젝트 정보 없음");
            return;
        }

        if (elementIcon[0] == null)
        {
            Debug.Log("[RosterView] 캐릭터 속성이미지 정보 없음");
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            switch (model.ChracterClass[i])
            {
                case 0:
                    characterElement[i].sprite = elementIcon[0];
                    break;
                case 1:
                    characterElement[i].sprite = elementIcon[1];
                    break;
                case 2:
                    characterElement[i].sprite = elementIcon[2];
                    break;
                case 3:
                    characterElement[i].sprite = elementIcon[3];
                    break;

            }
        }
    }

    public void CharacterClass(RosterModel model)
    {
        ModelNullCheck(model);

        if (characterClass[0] == null)
        {
            Debug.Log("[RosterView] 캐릭터 클래스 정보 없음");
            return;
        }

        if (classIcon[0] == null)
        {
            Debug.Log("[RosterView] 캐릭터 클래스이미지 정보 없음");
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            switch (model.ChracterClass[i])
            {
                //불 물 전기 무속성
                case 0:
                    characterClass[i].sprite = classIcon[0];
                    break;
                case 1:
                    characterClass[i].sprite = classIcon[1];
                    break;
                case 2:
                    characterClass[i].sprite = classIcon[2];
                    break;
            }
        }
    }

    public void ModelNullCheck(RosterModel model)
    {
        if (model == null)
        {
            Debug.Log("[RosterView] 캐릭터 정보 없음");
            return;
        }
    }

}
