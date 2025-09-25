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
    private readonly List<Cards> Hand = new();
    private void OnEnable()
    {

        ActionSystem.Attachperformmer<DrawCard>(DrawcardPerformer);
        ActionSystem.Attachperformmer<DiscardCardsGa>(Discardpreformer);
        ActionSystem.Attachperformmer<Playcard>(PlayCardPerformer);
        Debug.Log("We work");
        ActionSystem.SubscribeReaction<Enemyturn>(EnemyturnReact, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<Enemyturn>(PostEnemyturnReact, ReactionTiming.POST);
        Debug.Log("So do wee!!!");

    }
    private void OnDisable()
    {
        ActionSystem.Dettachperformer<DrawCard>();
        ActionSystem.Dettachperformer<DiscardCardsGa>();
        ActionSystem.Dettachperformer<Playcard>();

        Debug.Log("we off");
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
            Debug.Log("i might be being drawn");
            yield return DrawCards();
        }
        if(notDrawn > 0)
        {
            //potientail prob
            Debug.Log("fill her up");
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
            Discardpile.Add(card);
            CardDescriptions cardview =handdetails.RemoveCard(card);
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
        DrawCard drawcard = new(5);
        ActionSystem.Instance.AddAction(drawcard);


    }
    //helper methods
    public IEnumerator DrawCards()
    {
        Cards card = drawpile.Draw();
        Hand.Add(card);
        CardDescriptions view = CardviewsCreator.Instance.CreateCardView(card, Drawpoint.position, Drawpoint.rotation);
        yield return handdetails.AddCard(view);
    }
    private void RefillDeck()
    {
        drawpile.AddRange(Discardpile);
        drawpile.Clear();
    }

    //not working
    public IEnumerator dicardCard(CardDescriptions cardview)
    {
        Debug.Log("working son");
        cardview.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardview.transform.DOMove(Discardpoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardview.gameObject);
    }
    private IEnumerator PlayCardPerformer(Playcard card)
    {
        Hand.Remove(card.card);
        CardDescriptions cardview = handdetails.RemoveCard(card.card);
    }

}
