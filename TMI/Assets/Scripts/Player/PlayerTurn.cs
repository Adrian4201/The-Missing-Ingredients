using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerTurn : GameAction
{
    private CardSystem cardSystem;
    private CardDescriptions cardView;
    private Cards selectedCard;
    public IEnumerator EXcutePlayerturn()
    {
        
        DrawCard draw = new(2);
        ActionSystem.Instance.Preform(draw);
        yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);


        yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);

    }
    private void selectCard(Cards card, CardDescriptions cardview)
    {
        if (TurnSystem.Instance.canplay)  // Ensure it's the player's turn
        {
            selectedCard = card;
            cardView = cardview;
            TurnSystem.Instance.SetPlayedCard(card, cardview);  // Notify TurnSystem
        }

    }
    private void DiscardSelectedCard()
    {
        if (cardView != null && cardSystem != null)
        {
            cardSystem.dicardCard(cardView);  // Call CardSystem's discard method
            cardView = null;
            selectedCard = null;
            UnityEngine.Debug.Log("Card discarded.");
        }
        else
        {
            UnityEngine.Debug.LogWarning("Cannot discard: cardView or cardSystem is null.");
        }
    }
}
