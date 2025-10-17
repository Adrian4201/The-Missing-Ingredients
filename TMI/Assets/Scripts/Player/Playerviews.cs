using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerviews : Combatantviews
{
   public void setup(PlayerData data)
    {
        setupBase(data.Health, data.Image);
    }
}
