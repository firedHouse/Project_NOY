using UnityEngine;

public class TestPresenter : MonoBehaviour
{
    [SerializeField] private GoldModelScript _testModelScript;
    [SerializeField] private GoldInfo _testViewScript;

    private void Start()
    {
        _testModelScript.OnGoldChanged += HandGoldChanged;
    }

    private void HandGoldChanged(int gold)
    {
        _testViewScript.UpdateGold(gold);
    }
}
