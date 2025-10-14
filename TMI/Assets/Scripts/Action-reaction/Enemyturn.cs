using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyturn : GameAction
{
    public IEnumerator ExecuteEnemyTurn()
    {
        Debug.Log("EnemyTurn ExecuteEnemyTurn method started");
        // Draw a card for the enemy
        DrawCard enemydraw = new DrawCard(1);
        ActionSystem.Instance.Preform(enemydraw);
        yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);
        if (CardSystem.Instance.Hand.Count > 0)
        {
            Cards card = CardSystem.Instance.Hand[0];
            Debug.Log("Enemy picked up card: " + card.Title);

            EnemyAttack enemAttack = new EnemyAttack(card);
            ActionSystem.Instance.AddAction(enemAttack);
            ActionSystem.Instance.Preform(enemAttack);
            yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);
        }
        else
        {
            Debug.LogError("No cards for enemy!");
        }
        Debug.Log("EnemyTurn ExecuteEnemyTurn method completed");
        yield return null;
    }
}
