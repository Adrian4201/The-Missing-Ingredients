using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealdamage : GameAction
{
    // Start is called before the first frame update
    public int Damage;
    public HealthTracker Target { get; private set; }
    public List<Combatantviews> Targets {  get; set; }
    public Dealdamage(int damage, List<Combatantviews> targets)
    {
        Damage = damage;
        Targets = targets;
    }
    
}
