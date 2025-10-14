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

    private enum TurnState { PlayerTurn, EnemyTurn, GameOver }

    private TurnState currentTurn = TurnState.PlayerTurn;

    private System.Action onCardPlayedCallback;
    
    public bool canplay = false;

    private bool playedcard = false;
    // Have player draw 2 cards

    private void Start()
    {
        StartCoroutine(StartTurn());
        
    }

    public IEnumerator StartTurn()
    {
        Debug.Log("Start() called");

        Debug.Log("Player turn");
        while(currentTurn != TurnState.GameOver)
        { 
            if(currentTurn == TurnState.PlayerTurn)
            {
                //Player's turn
                canplay = true;
                playedcard = false;

                if (!canplay)
                {
                    yield break;
                }
                //Player draw card method
                DrawCard draw = new(2);
                ActionSystem.Instance.Preform(draw);





                //Don't do anything else until player plays a card
                yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);
                onCardPlayedCallback = () => { playedcard = true; Debug.Log("Card played—unblocking turn!"); };
                yield return new WaitUntil(() => playedcard);
                Debug.Log("reached");
                if (cardview != null)
                {
                    yield return CardSystem.Instance.dicardCard(cardview);

                }
                else
                {
                    Debug.LogWarning("cardview is null cant discard");

                }



                //Once all actions or "effects" are done, run this same StartTurn method
                //With the opposite boolean
                currentTurn = TurnState.EnemyTurn;
            }
            else if (currentTurn == TurnState.EnemyTurn)
            {
                Debug.Log("enemyturn");
                canplay = false;

                Enemyturn enemyTurnAction = new Enemyturn();
                yield return StartCoroutine(enemyTurnAction.ExecuteEnemyTurn());

                currentTurn = TurnState.PlayerTurn;
            }
            yield return new WaitForSeconds(0.5f);
        

        }

        
    }

    
   
    public void CardEffectPerformer(bool hasPerformed)
    {
        if (hasPerformed)
        {

        }CardEffectPerformer(false);

    }

   

    
    

   

}
