using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class TestButton : MonoBehaviour
{
    [SerializeField] private Character _character;

    public void Test()
    {
        _character.TakeDamage(1000);
    }
}
