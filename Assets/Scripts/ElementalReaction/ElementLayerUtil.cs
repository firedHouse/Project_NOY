using UnityEngine;

public static class ElementLayerUtil
{
    public static ElementType LayerToElement(int layer)
    {
        string layerName = LayerMask.LayerToName(layer);

        return layerName switch
        {
            "Fire" => ElementType.Fire,
            "Water" => ElementType.Water,
            "Electric" => ElementType.Electric,
            "None" => ElementType.None,
            _ => ElementType.None
        };
    }

    public static int ElementToLayer(ElementType element)
    {
        return element switch
        {
            ElementType.Fire => LayerMask.NameToLayer("Fire"),
            ElementType.Water => LayerMask.NameToLayer("Water"),
            ElementType.Electric => LayerMask.NameToLayer("Electric"),
            _ => LayerMask.NameToLayer("None")
        };
    }
}
