using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : GameAction
{
    public int Amount{ get; set; }
    public DrawCard(int drawamount)
    {
       
            Debug.Log("draw");
            Amount = drawamount;

        
    }
    
}
