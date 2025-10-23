using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayCard : GameAction
{
    public Cards Card { get; private set; }
    public EnemyPlayCard(Cards card)
    {
        Card = card;
    }
}
