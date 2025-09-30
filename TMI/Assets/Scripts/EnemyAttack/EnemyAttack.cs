using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : GameAction
{
    public Cards cardS { get; set; }
    public EnemyAttack(Cards card)
    {
        cardS = card;
    }
}
