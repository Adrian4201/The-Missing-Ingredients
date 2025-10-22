using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/*
public class PlayerTurn : GameAction
{
    private CardSystem cardSystem;
    private CardDescriptions cardView;
    private Cards selectedCard;
    public IEnumerator ExecutePlayerTurn()
    {
        // Step 1: Draw cards
        DrawCard draw = new DrawCard(2);
        ActionSystem.Instance.Preform(draw);
        yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);
        UnityEngine.Debug.Log("Player drew 2 cards.");

        // Step 2: Wait for the player to select a card
        // This will pause the coroutine until selectedCard is set (via SelectCard())
        yield return new WaitUntil(() => selectedCard != null);
        UnityEngine.Debug.Log("Player selected card: " + selectedCard.Title);

        // Step 3: Play the card (add effects here if needed)
        PlaySelectedCard();
        yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);

        // Step 4: Discard the card
        DiscardSelectedCard();

        // Step 5: Notify TurnSystem that the turn is done
        TurnSystem.Instance.NotifyCardPlayed();
        UnityEngine.Debug.Log("Player turn complete.");
    }

    // Ensure SelectCard() is called from UI (e.g., on card click)
    public void SelectCard(Cards card, CardDescriptions view)
    {
        if (TurnSystem.Instance.canplay)  // Only allow if it's the player's turn
        {
            selectedCard = card;
            cardView = view;
            TurnSystem.Instance.SetPlayedCard(card, view);
            UnityEngine.Debug.Log("Card selected.");
        }
        else
        {
            UnityEngine.Debug.LogWarning("Cannot select card: Not player's turn.");
        }
    }

    // Placeholder for playing (expand with card effects)
    private void PlaySelectedCard()
    {
        if (CardSystem.Instance != null && selectedCard != null)
        {
            // Call CardSystem to play (adjust based on your CardSystem methods)
            CardSystem.Instance.PlayCard(selectedCard); 
            UnityEngine.Debug.Log("Card play triggered in CardSystem.");
        }
        else
        {
            UnityEngine.Debug.LogError("Cannot play: CardSystem or selectedCard is null.");
        }
    }

    // Discard logic
    private void DiscardSelectedCard()
    {
        if (cardView != null && CardSystem.Instance != null)
        {
            CardSystem.Instance.dicardCard(cardView);
            cardView = null;
            selectedCard = null;
            UnityEngine.Debug.Log("Card discarded.");
        }
    }

}
*/
