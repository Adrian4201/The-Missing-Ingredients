using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    // Start is called before the first frame update
    [SerializeField] private Handdetails handdetails;
    [SerializeField] private Transform Drawpoint;
    [SerializeField] private Transform Discardpoint;
    private readonly List<Cards> drawpile = new();
    private readonly List<Cards> Discardpile = new();
    private readonly List<Cards> Hand = new();
    private void OnEnable()
    {
        ActionSystem.Attachperformmer<DrawCard>(DrawcardPerformer);
        ActionSystem.Attachperformmer<DiscardCardsGa>(Discardpreformer);
        ActionSystem.SubscribeReaction<Enemyturn>(EnemyturnReact, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<Enemyturn>(EnemyturnReact, ReactionTiming.POST);

    }
    private void OnDisable()
    {
        ActionSystem.Dettachperformer<DrawCard>();
        ActionSystem.Dettachperformer<DiscardCardsGa>();
        ActionSystem.UnSubscribeReaction<Enemyturn>(EnemyturnReact, ReactionTiming.PRE);
        ActionSystem.UnSubscribeReaction<Enemyturn>(EnemyturnReact, ReactionTiming.POST);
    }
    //setups
    public void Setup(List<CardData> Deckdata)
    {
        foreach (var cardData in Deckdata)
        {
            Cards card = new(cardData);
            drawpile.Add(card);
        }
    }
    //performers

    private IEnumerator DrawcardPerformer(DrawCard drawperformer)
    {
      int CardAmount = Mathf.Min(drawperformer.drawAmount, drawpile.Count);
        int notDrawn = drawperformer.drawAmount- CardAmount;
        for(int i = 0; i < CardAmount; i++)
        {
            yield return DrawCards();
        }
        if(notDrawn > 0)
        {
            RefillDeck();
            for(int i = 0; i < notDrawn; i++)
            {
                yield return DrawCards();
            }
        }
    }
    private IEnumerator Discardpreformer(DiscardCardsGa Discardperformer)
    {
        foreach(var card in Hand)
        {
            Discardpile.Add(card);
            CardDescriptions cardview =handdetails.RemoveCard(card);
            yield return dicardCard(cardview);
        }
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
    private IEnumerator DrawCards()
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
    private IEnumerator dicardCard(CardDescriptions cardview)
    {
        cardview.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardview.transform.DOMove(Discardpoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardview.gameObject);
    }

}
