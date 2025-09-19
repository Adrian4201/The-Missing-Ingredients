using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : GameAction
{
    public int drawAmount{ get; set; }
    public DrawCard(int drawamount)
    {
        drawAmount = drawamount;
    }
}
