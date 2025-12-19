using UnityEngine;

public class TestButton : MonoBehaviour
{
    [SerializeField] CharacterBattleInfoModel _model;

    public void OnHP0()
    {
        Debug.Log($"[TestButton] HP 삭제");
        _model.DecreaseHP(100);
    }
}
