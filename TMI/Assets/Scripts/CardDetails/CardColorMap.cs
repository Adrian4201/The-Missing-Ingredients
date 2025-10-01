using System.Collections.Generic;
using UnityEngine;

public static class CardColorMap
{
    public static readonly Dictionary<CardColor, Color> Colors = new()
    {
        { CardColor.Red,    Color.red },
        { CardColor.Orange, new Color(1f, 0.5f, 0f) }, // orange
        { CardColor.Yellow, Color.yellow },
        { CardColor.Green,  Color.green },
        { CardColor.Blue,   Color.blue },
        { CardColor.Purple, new Color(0.6f, 0f, 0.6f) } // purple
    };
}
