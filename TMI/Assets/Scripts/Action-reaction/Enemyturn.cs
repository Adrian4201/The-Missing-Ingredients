using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyturn : GameAction
{
    public IEnumerator ExecuteEnemyTurn()
    {
        Debug.Log("EnemyTurn ExecuteEnemyTurn method started");

        // Draw 1 card for the enemy (fixed: was 0)
        DrawCard enemydraw = new DrawCard(1);
        ActionSystem.Instance.Preform(enemydraw);
        yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);

        // Check enemy's own hand (fixed: was CardSystem.Instance.Hand)
        if (EnemyCardSystem.Instance.EnemyHand.Count > 0)
        {
            Cards card = EnemyCardSystem.Instance.EnemyHand[0];  // Select first card (or randomize/logic for selection)
            Debug.Log("Enemy picked up card: " + card.Title);

            // Perform attack with the card
            EnemyAttack enemAttack = new EnemyAttack(card);
            ActionSystem.Instance.Preform(enemAttack);
            yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);

            // Discard the used card (added: similar to player's turn)
            /*
            EnemyCardSystem.Instance.EnemyHand.Remove(card);
            EnemyDiscardpile.Add(card); 
            EnemyCardview cardView = EnemyHandView.RemoveCard(card); 
            yield return EnemyCardSystem.Instance.DiscardCard(cardView);  // Call discard method
            */
        }
        else
        {
            Debug.LogWarning("No cards in enemy hand!");
        }

        Debug.Log("EnemyTurn ExecuteEnemyTurn method completed");
        yield return null;
    }
}