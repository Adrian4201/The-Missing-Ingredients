using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction 
{
    public List<GameAction> Prereaction { get; private set; } = new();
    public List<GameAction> PerformReaction { get; private set; } = new();

    public List<GameAction> PostReaction {  get; private set; } = new();

    public List<GameAction> PerformCardEffect {  get; private set; } = new();


}
