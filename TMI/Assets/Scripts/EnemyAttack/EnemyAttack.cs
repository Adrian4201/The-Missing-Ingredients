using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : GameAction
{
    public EnemyCards Attack { get; set; }
    public EnemyAttack(EnemyCards card)
    {
        Attack = card;
    }
}
