using System;
using UnityEngine;

public class GoldModelScript : MonoBehaviour
{
    private int _gold = 1000;

    public int Gold { get { return _gold; } set { _gold = value; } }

    public event Action<int> OnGoldChanged;

    public bool SpendGold(int amount)
    {
        //
        if(Gold >= amount)
        {
            Gold -= amount;
            OnGoldChanged?.Invoke(Gold);
            return true;
        }

        return false;
    }
}
