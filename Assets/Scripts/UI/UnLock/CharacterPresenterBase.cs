using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
    
public abstract class CharacterPresenterBase : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] protected CharacterListView characterListView;

    [SerializeField] protected GameObject characterList;
    [SerializeField] protected List<CharacterSlot> characterSlots;
    protected List<CharacterListModel> characters;

    [Header("화면에 표시될 캐릭터 데이터 모델")]
    [SerializeField]
    protected CharacterListModel model;

    // public Dictionary<int, (int, int)> gradeData = new Dictionary<int, (int, int)>();
    public CharacterListModel Model { get { return model; } }

    protected abstract void Start();
    protected abstract void LoadCharacterList();
    protected abstract void ShowDetailView(CharacterListModel character);
    public abstract void OnSlotClicked(CharacterListModel character);

    /// <summary>
    /// 이름, 해금 여부?, 코드네임, 속성, 대사, 인포, 업데이트
    /// </summary>
    public abstract void UpdateMainInfo(CharacterListModel character);

    public abstract void SetButtonEvent(CharacterListModel character, UnityAction<CharacterListModel> onClickCallBack);
    public abstract void UpdateCharacterInfo(CharacterListModel model);
    public abstract void Init(CharacterListModel model);

    /// <summary>
    /// 슬롯 오브젝트 받아와 리스트화하고 각 슬롯에 캐릭터 데이터 추가
    /// </summary>
    protected void SetSlotUI()
    {
            characterSlots = characterList.GetComponentsInChildren<CharacterSlot>().ToList();
            characters = LobbyManager.Instance.CharacterListModels;
    }
}