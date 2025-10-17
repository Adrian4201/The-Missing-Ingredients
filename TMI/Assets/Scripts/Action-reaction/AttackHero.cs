using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHero : GameAction
{
    public Enemyview Attacker {  get; private set; }
    public AttackHero(Enemyview attacker) 
    {
        Attacker = attacker;
    }
}
