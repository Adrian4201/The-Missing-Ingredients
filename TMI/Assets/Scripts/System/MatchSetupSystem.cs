using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] private List<CardData> cardData;
    private void Start()
    {
        CardSystem.Instance.Setup(cardData);
        DrawCard card = new(5);
        ActionSystem.Instance.Preform(card);
    }
}
