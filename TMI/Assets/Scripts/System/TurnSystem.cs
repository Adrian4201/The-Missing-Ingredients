using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : CardSystem
{
    // Start is called before the first frame update

     private Cards card;

    private CardDescriptions cardview;
    private Playcard Card;
    public CardSystem cardSystem;
    public bool canplay = true;
    // Have player draw 2 cards

    private void Start()
    {
        StartTurn(canplay);
    }

    public void StartTurn(bool turn)
    {
        if(turn)
        {
            //Player's turn

            //Player draw card method
            CardSystem.Instance.DrawCards();
            CardSystem.Instance.PlayCardPerformer(Card);
            //Don't do anything else until player plays a card

            //Once all actions or "effects" are done, run this same StartTurn method
            CardSystem.Instance.dicardCard(cardview);
            //With the opposite boolean
        }
        else
        {
            //Opponent turn
            StartCoroutine(EnemyTurn());
        }

        
    }

    public IEnumerator EnemyTurn()
    {

        yield return CardSystem.Instance.DrawCards();

        EnemyAttack enemAttack = new EnemyAttack(card);

        ActionSystem.Instance.AddAction(enemAttack);
    }
    public void CardEffectPerformer(bool hasPerformed)
    {
        if (hasPerformed)
        {

        }CardEffectPerformer(false);

    }

   

    
    

   

}
