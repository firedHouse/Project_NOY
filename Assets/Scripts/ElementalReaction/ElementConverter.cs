using UnityEngine;

public static class ElementConverter
{
    // CSV ±âÁØ: 0=Fire, 1=Water, 2=Electric, 3=None
    public static ElementType FromCSV(int value)
    {
        return value switch
        {
            0 => ElementType.Fire,
            1 => ElementType.Water,
            2 => ElementType.Electric,
            3 => ElementType.None,
            _ => ElementType.None
        };
    }
}
