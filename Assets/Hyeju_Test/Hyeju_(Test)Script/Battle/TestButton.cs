using UnityEngine;

public class TestButton : MonoBehaviour
{
    [SerializeField] CharacterBattleInfoPresenter m;

    public void OnHP0()
    {
        Debug.Log($"HP ªË¡¶");
        //m.DecreaseHP(100);
    }
}
