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
        yield return EnemyCardSystem.Instance.EnemyDrawperformer(enemydraw);
        yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);

        // Check enemy's own hand (fixed: was CardSystem.Instance.Hand)
        if (EnemyCardSystem.Instance.EnemyHand.Count > 0)
        {
            EnemyCards card = EnemyCardSystem.Instance.EnemyHand[0];  
            Debug.Log("Enemy picked up card: " + card.Title);

            //perform playcard
            EnemyPlayCard playcard = new EnemyPlayCard(card);
            ActionSystem.Instance.Preform(playcard);
            yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);


            // Perform attack with the card
            EnemyAttack enemAttack = new EnemyAttack(card);
            ActionSystem.Instance.Preform(enemAttack);
            yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);


            // Discard the used card (added: similar to player's turn)
            
          
            EnemyCardSystem.Instance.EnemyDiscardpile.Add(card);
            EnemyCardview cardView = EnemyCardSystem.Instance.Enumhanddetails.RemoveCard(card);
            yield return EnemyCardSystem.Instance.DiscardCard(cardView); 
            
        }
        else
        {
            Debug.LogWarning("No cards in enemy hand!");
        }

        Debug.Log("EnemyTurn ExecuteEnemyTurn method completed");
        yield return null;
    }
}