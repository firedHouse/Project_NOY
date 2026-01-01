using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterSlotBase : MonoBehaviour
{
    [Header("캐릭터 슬롯 프리팹")]
    [SerializeField]
    protected Button slotButton;
    [SerializeField] protected Image slotImage;

    public Image SlotImage { get => slotImage; set => slotImage = value; }

    [SerializeField] protected CharacterListModel slotModel;

    protected string id;
    public Text slotCharacter;
    public Button SlotButton => slotButton;
    public CharacterListModel SlotModel { get =>  slotModel; set => slotModel = value; }
    public Text SlotCharacter { get => slotCharacter; set => slotCharacter = value; }

    private void Awake()
    {
        slotButton = GetComponent<Button>();
        slotCharacter = gameObject.GetComponentInChildren<Text>();
    }

    // 슬롯 클릭시 변경
    public void SetButtonEvent(CharacterListModel model, UnityAction<CharacterListModel> onClickCallBack)
    {
        this.slotModel = model;
        slotButton.onClick.AddListener(() => onClickCallBack(this.slotModel));
    }
}