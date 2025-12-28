using UnityEngine;
using UnityEngine.UI;

public class Roster_ChangeView : MonoBehaviour
{
    [SerializeField] RosterPresenter presenter;

    [Header("캐릭터 일러스트")]
    [SerializeField] private Text[] characterIllust = new Text[3];
    //[SerializeField] private Image[] characterIllust = new Image[3];

    [Header("선택 캐릭터")]
    [SerializeField] private Text topCharacterImage;
    [SerializeField] private Text bottomCharacterImage;
    //[SerializeField] private Image topCharacterImage;
    //[SerializeField] private Image bottomCharacterImage;
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

        topCharacterImage.text = model.ChracterName[i];
        //topCharacterImage.sprite = Resources.Load<Sprite>(model.ChracterIllust[i]);
    }

    public void bottomImage(RosterModel model, int i)
    {
        ModelNullCheck(model);
        if (topCharacterImage == null)
        {
            Debug.Log("[RosterView] 하단 이미지 오브젝트 정보 없음");
            return;
        }

        bottomCharacterImage.text = model.ChracterName[i];
        //bottomCharacterImage.sprite = Resources.Load<Sprite>(model.ChracterIllust[i]);
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
            //characterIllust[i].sprite = Resources.Load<Sprite>(model.ChracterIllust[i]);
            characterIllust[i].text = model.ChracterName[i];
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
