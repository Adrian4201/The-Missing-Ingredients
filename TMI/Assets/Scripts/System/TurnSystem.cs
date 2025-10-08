using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    // Start is called before the first frame update

    private Cards card;

    private CardDescriptions cardview;
    private Playcard Card;

    [SerializeField] private CardSystem cardSystem;

    public bool canplay = true;

    private bool playedcard = false;
    // Have player draw 2 cards

    private void Start()
    {
        StartCoroutine(StartTurn(true));
    }

    public IEnumerator StartTurn(bool turn)
    {
       
        Debug.Log("Player turn");
        if(turn)
        {
            //Player's turn
            for (int i = 0; i < 2; i++)
            {
                yield return CardSystem.Instance.DrawCards();
            }
            


            canplay = true;
            playedcard = false;
            //Player draw card method
           
            //Don't do anything else until player plays a card

            yield return new WaitUntil(() => playedcard == true);

            yield return CardSystem.Instance.dicardCard(cardview);


            //Once all actions or "effects" are done, run this same StartTurn method
            //With the opposite boolean
            yield return EnemyTurn();
        }
        else
        {
            //Opponent turn
            StartCoroutine(EnemyTurn());
        }

        
    }

    public IEnumerator EnemyTurn()
    {
        Debug.Log("My turn");
        yield return CardSystem.Instance.DrawCards();

        EnemyAttack enemAttack = new EnemyAttack(card);

        ActionSystem.Instance.AddAction(enemAttack);

        StartTurn(canplay);
    }
    public void CardEffectPerformer(bool hasPerformed)
    {
        if (hasPerformed)
        {

        }CardEffectPerformer(false);

    }

   

    
    

   

}
