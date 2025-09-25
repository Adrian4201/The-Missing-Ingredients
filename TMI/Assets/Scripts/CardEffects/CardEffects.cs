using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CardEffects : MonoBehaviour 
{
    [SerializeField] private Cards cards;
    public void Shuffelcard(List<CardData> deckData)
    {
        for (int i = 0; i < deckData.Count; i++)
        {
            for(int j = deckData.Count; j > 0; j--)
            {
                CardData temp = deckData[i];
                deckData[i] = deckData[j];
                deckData[j] = temp;
            }
        }
    }

}

