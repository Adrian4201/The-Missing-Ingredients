using System.Collections.Generic;

public static class RarityLabelMap
{
    public static readonly Dictionary<CardRarity, string> Labels = new()
    {
        { CardRarity.Common, "C" },
        { CardRarity.Rare, "R" },
        { CardRarity.Epic, "E" },
        { CardRarity.Legendary, "L" }
    };
}
