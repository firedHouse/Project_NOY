using UnityEngine;
using UnityEngine.UI;

public class MonsterInfoView : MonoBehaviour
{
    [SerializeField] private Text monsterNameText;
    [SerializeField] private Text elementText;
    [SerializeField] private Text posistionText;

    public void UpdateMonsterName(string text)
    {
        monsterNameText.text = text;
    }

    public void UpdateElement(int type)
    {
        elementText.text = type.ToString();
    }

    //public void UpdatePosition(string text)
    //{
    //    posistionText.text = text;
    //}
}
