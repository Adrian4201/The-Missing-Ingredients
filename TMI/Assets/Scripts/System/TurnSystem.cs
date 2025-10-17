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
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
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

            
                DrawCard draw = new(2);
                ActionSystem.Instance.Preform(draw);
                onCardPlayedCallback = () => {
                    playedcard = true;
                    CardSystem.Instance.dicardCard(cardview);
                    //Destroy(cardview.gameObject);
                    Debug.Log("Card played—unblocking turn!"); 
                };
                //Player draw card method





                //Don't do anything else until player plays a card
                yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);
                yield return new WaitUntil(() => playedcard);
                canplay = false;
                Debug.Log("reached");
                if (cardview != null)
                {
                    yield return CardSystem.Instance.dicardCard(cardview);
                    cardview = null;
                    Debug.Log("Card discarded and destroyed.");
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

                onCardPlayedCallback = () => {
                    Debug.Log("Enemy card played—proceeding with turn!");  // You can customize this if needed
                    // If you want to add more logic, like advancing the turn, do it here
                };

                Enemyturn enemyTurnAction = new Enemyturn();
                yield return StartCoroutine(enemyTurnAction.ExecuteEnemyTurn());

                onCardPlayedCallback = null;
                currentTurn = TurnState.PlayerTurn;
            }
            yield return new WaitForSeconds(3f);
        

        }

        
    }
    public void NotifyCardPlayed()
    {
        if (onCardPlayedCallback != null)
        {
            onCardPlayedCallback.Invoke();
            onCardPlayedCallback = null; // Prevent double-calls
            Debug.Log("TurnSystem: Card played callback invoked");
        }
        else
        {
            Debug.LogWarning("TurnSystem: No callback registered for card play");
        }
    }


    public void SetPlayedCard(Cards playedCard, CardDescriptions playedCardView)
    {
        card = playedCard;
        cardview = playedCardView;
        Debug.Log("SetPlayedCard called: cardview is now set.");  // Add this for debugging
    }
    
    /*public void CardEffectPerformer(bool hasPerformed)
    {
        if (hasPerformed)
        {

        }CardEffectPerformer(false);

    }
    */

   

    
    

   

}
