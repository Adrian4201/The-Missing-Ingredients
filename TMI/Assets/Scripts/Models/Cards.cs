using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Cards 
{
    public string Title => data.name;
    public string Description => data.Description;
    public Sprite Image => data.Image;

    public int Damage {  get; private set; }

    private readonly CardData data;
    public Cards(CardData card)
    {
        data = card;
        Damage = card.Damage;
    }
    
  
  
}
