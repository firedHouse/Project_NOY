using UnityEngine;

public static class ElementConverter
{
    // CSV 기준: 0=Fire, 1=Water, 2=Electric, 3=None
    //12.23 교체 전기0, 물1, 불2
    public static ElementType FromCSV(int value)
    {
        return value switch
        {
            0 => ElementType.Electric,
            1 => ElementType.Water,
            2 => ElementType.Fire,
            3 => ElementType.None,
            _ => ElementType.None
        };
    }
}
