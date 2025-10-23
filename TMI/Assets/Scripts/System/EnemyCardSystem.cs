using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.XR;

public class EnemyCardSystem : Singleton<CardSystem>
{
    [SerializeField] private EnemyHandView Enumhanddetails;
    [SerializeField] private Transform ENumDrawpoint;
    [SerializeField] private Transform EnumDiscardpoint;
    //list for 
    private readonly List<Cards> Enemydrawpile = new();
    private readonly List<Cards> EnemyDiscardpile = new();
    public readonly List<Cards> EnemyHand = new();
    private void OnEnable()
    {
        ActionSystem.Attachperformmer<DrawCard>(EnemyDrawperformer);
        ActionSystem.Attachperformmer<DiscardCardsGa>(EnemyDiscardperformer);

    }
    private void OnDisable()
    {
        ActionSystem.Dettachperformer<DrawCard>();
        ActionSystem.Dettachperformer<DiscardCardsGa>();
    }
    public void Setup(List<CardData> deckData)
    {
        foreach (var cardData in deckData)
        {
            Debug.Log(cardData);
            Cards card = new(cardData);
            Enemydrawpile.Add(card);
        }
    }
    private IEnumerator EnemyDrawperformer(DrawCard drawperformer)
    {
        int enemyCardamount = Mathf.Min(drawperformer.Amount, Enemydrawpile.Count);
        int notdraewn = drawperformer.Amount - enemyCardamount;
        for (int i = 0; i < enemyCardamount; i++)
        {

            yield return DrawCards();
        }
        if(notdraewn  > 0)
        {
            EnRefill();
            for( int i = 0; i< notdraewn; i++)
            {
                yield return DrawCards();
            }
        }
    }
    private IEnumerator EnemyDiscardperformer(DiscardCardsGa endiscard)
    {
        foreach (var card in EnemyHand)
        {
            EnemyDiscardpile.Add(card);
            EnemyCardview Cardview = Enumhanddetails.RemoveCard(card);
            yield return DiscardCard(Cardview);

        }
        EnemyHand.Clear();
    }
    private IEnumerator DrawCards()
    {
        Cards card = Enemydrawpile.Draw();
        EnemyHand.Add(card);
        EnemyCardview EnemyView = EnemyCardViewsCreator.Instance.CreateEnemyView(card,ENumDrawpoint.position,ENumDrawpoint.rotation);
        yield return EnemyView;
    }
    
    private void EnRefill()
    {
        Enemydrawpile.AddRange(EnemyDiscardpile);
        EnemyDiscardpile.Clear();
    }
    
    private IEnumerator DiscardCard(EnemyCardview cardview)
    {
        cardview.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardview.transform.DOMove(EnumDiscardpoint.position, 0.15f);
        yield return tween.WaitForCompletion();      
        Destroy(cardview.gameObject);
    }

}
