using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCardSystem : Singleton<CardSystem>
{
    [SerializeField] private Handdetails Enumhanddetails;
    [SerializeField] private Transform ENumDrawpoint;
    [SerializeField] private Transform EnumDiscardpoint;
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
