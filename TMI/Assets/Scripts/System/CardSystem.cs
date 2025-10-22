using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    //Target methods
    [SerializeField] private Handdetails handdetails;
    [SerializeField] private Transform Drawpoint;
    [SerializeField] private Transform Discardpoint;
    //list for deck
    private readonly List<Cards> drawpile = new();
    private readonly List<Cards> Discardpile = new();
    public readonly List<Cards> Hand = new();
    private void OnEnable()
    {

        ActionSystem.Attachperformmer<DrawCard>(DrawcardPerformer);
        ActionSystem.Attachperformmer<DiscardCardsGa>(Discardpreformer);
        ActionSystem.Attachperformmer<Playcard>(PlayCardPerformer);
        ActionSystem.Attachperformmer<Dealdamage>(Dealdamageperformer);
        ActionSystem.Attachperformmer<EnemyAttack>(EnemyAttackPerformer);
        
        ActionSystem.SubscribeReaction<Enemyturn>(EnemyturnReact, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<Enemyturn>(PostEnemyturnReact, ReactionTiming.POST);
       

    }
    private void OnDisable()
    {
        ActionSystem.Dettachperformer<DrawCard>();
        ActionSystem.Dettachperformer<DiscardCardsGa>();
        ActionSystem.Dettachperformer<Playcard>();
        ActionSystem.Dettachperformer<Dealdamage>();
        ActionSystem.Dettachperformer<EnemyAttack>();
        
        ActionSystem.UnSubscribeReaction<Enemyturn>(EnemyturnReact, ReactionTiming.PRE);
        ActionSystem.UnSubscribeReaction<Enemyturn>(PostEnemyturnReact, ReactionTiming.POST);
    }
    //setups
    public void Setup(List<CardData> deckData)
    {
        foreach (var cardData in deckData)
        {
            Debug.Log(cardData);
            Cards card = new(cardData);
            drawpile.Add(card);
        }
    }
    //performers

    private IEnumerator DrawcardPerformer(DrawCard drawperformer)
    {
        int CardAmount = Mathf.Min(drawperformer.Amount, drawpile.Count);
        int notDrawn = drawperformer.Amount- CardAmount;

        for(int i = 0; i < CardAmount; i++)
        {
           
            yield return DrawCards();
        }
        if(notDrawn > 0)
        {
            //potientail prob
            
            RefillDeck();
            for(int i = 0; i < notDrawn; i++)
            {
                yield return DrawCards();
            }
        }
    }
    //works fine
    private IEnumerator Discardpreformer(DiscardCardsGa Discardperformer)
    {
        foreach(var card in Hand)
        {
            Debug.Log("Discardperformer Check?");
            Discardpile.Add(card);
            CardDescriptions cardview = handdetails.RemoveCard(card);
            yield return dicardCard(cardview);

        }

        Debug.Log("hand clearded");

        Hand.Clear();

    }
    //reaction
    private void EnemyturnReact(Enemyturn enemyturn)
    {
        DiscardCardsGa discardga = new();
        ActionSystem.Instance.AddAction(discardga);
    }
    private void PostEnemyturnReact(Enemyturn enemyturn)
    {
        DrawCard drawcard = new(2);
        ActionSystem.Instance.AddAction(drawcard);


    }
    //helper methods
    public IEnumerator DrawCards()
    {
        Cards card = drawpile.Draw();
        Hand.Add(card);
        CardDescriptions view = CardviewsCreator.Instance.CreateCardView(card, Drawpoint.position, Drawpoint.rotation,handdetails.transform);
        yield return handdetails.AddCard(view);
    }
    private void RefillDeck()
    {
        drawpile.AddRange(Discardpile);
        Discardpile.Clear();
    }

    //not working
    public IEnumerator dicardCard(CardDescriptions cardview)
    {
        cardview.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardview.transform.DOMove(Discardpoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardview.gameObject);
        Debug.Log("working son");
    }
    public IEnumerator PlayCardPerformer(Playcard playCard)
    {
        if (!TurnSystem.Instance.canplay)
        {
            Debug.LogWarning("Blocked card play — not player's turn!");
            yield break;
        }

        Hand.Remove(playCard.card);
        CardDescriptions cardview = handdetails.RemoveCard(playCard.card);
        Debug.Log("Card has been play)");

        // Notify TurnSystem, but don't discard here
        TurnSystem turnSystem = FindObjectOfType<TurnSystem>();
        if (turnSystem != null)
        {
            turnSystem.SetPlayedCard(playCard.card, cardview);  // Ensure cardview is set
            turnSystem.NotifyCardPlayed();  // This just sets playedcard to true
        }

        // Do NOT call yield return dicardCard(cardview); here
        yield break;  // Exit early
    }
    public IEnumerator Dealdamageperformer(Dealdamage damage)
    {
        if (damage.Target != null)
        {
            damage.Target.takedamage(damage);
            Debug.Log($"Dealt {damage.Damage} damage!");
        }
        yield break;

    }
    private IEnumerator EnemyAttackPerformer(EnemyAttack attack)
    {
        
        Playcard playAction = new Playcard(attack.cardS);
        yield return PlayCardPerformer(playAction);
    }
    

}
