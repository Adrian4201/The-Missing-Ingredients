using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playcard : GameAction
{
    public Cards card { get;  set; }
    public Playcard(Cards cards) 
    {
        card = cards;
    }
}
