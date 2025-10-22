using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCardSystem : Singleton<CardSystem>
{
    //list for 
    private readonly List<Cards> drawpile = new();
    private readonly List<Cards> Discardpile = new();
    public readonly List<Cards> Hand = new();
    private void OnEnable()
    {
        
        
    }
    private void OnDisable()
    {
        
    }

}
