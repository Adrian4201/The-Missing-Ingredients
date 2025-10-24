using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayCard : GameAction
{
    public EnemyCards Cards { get; private set; }
    public EnemyPlayCard(EnemyCards card)
    {
        Cards = card;
    }
}
