using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealdamage : GameAction
{
    // Start is called before the first frame update
    public int Damage;
   
    public List<HealthTracker> Targets {  get; set; }
    public Dealdamage(int damage, List<HealthTracker> targets)
    {
        Damage = damage;
        Targets = targets;
        
    }
    
}
