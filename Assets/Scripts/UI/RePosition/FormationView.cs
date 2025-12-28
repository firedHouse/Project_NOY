using UnityEngine;
using UnityEngine.UI;

public class FormationView : MonoBehaviour
{
    [SerializeField] FormationPresenter presenter;

    [Header("캐릭터 일러스트")]
    [SerializeField] private Text[] characterIllust = new Text[3];
    //[SerializeField] private Image[] characterIllust = new Image[3];

    [Header("선택 캐릭터")]
    [SerializeField] private Text topCharacterImage;
    //[SerializeField] private Image topCharacterImage;
    [SerializeField] private GameObject topCharacterObject;

    private void Start()
    {
        topCharacterObject.SetActive(false);
    }

    public void TopImageActive(bool isActive)
    {
        topCharacterObject.SetActive(isActive);
    }


    public void topImage(PlayerTeamListModel model, int i)
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

    public void CharacterIllust(PlayerTeamListModel model)
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
    public void ModelNullCheck(PlayerTeamListModel model)
    {
        if (model == null)
        {
            Debug.Log("[RosterView] 캐릭터 정보 없음");
            return;
        }
    }
}
