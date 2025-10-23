using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class EnemyCardSystem : Singleton<EnemyCardSystem>
{
    [SerializeField] private EnemyHandView Enumhanddetails;
    [SerializeField] private Transform ENumDrawpoint;
    [SerializeField] private Transform EnumDiscardpoint;
    [SerializeField] private Transform EnemyPlaypoint;
    //list for 
    private readonly List<Cards> Enemydrawpile = new();
    private readonly List<Cards> EnemyDiscardpile = new();
    public readonly List<Cards> EnemyHand = new();
    private void OnEnable()
    {
        ActionSystem.Attachperformmer<DrawCard>(EnemyDrawperformer);
        ActionSystem.Attachperformmer<DiscardCardsGa>(EnemyDiscardperformer);
        ActionSystem.Attachperformmer<EnemyPlayCard>(EnemyPlayCardPerformer);
        ActionSystem.Attachperformmer<Dealdamage>(Dealdamageperformer);
        ActionSystem.Attachperformmer<EnemyAttack>(EnemyAttackPerformer);

    }
    private void OnDisable()
    {
        ActionSystem.Dettachperformer<DrawCard>();
        ActionSystem.Dettachperformer<DiscardCardsGa>();
        ActionSystem.Dettachperformer<EnemyPlayCard>();
        ActionSystem.Dettachperformer<Dealdamage>();
        ActionSystem.Dettachperformer<EnemyAttack>();
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
    public IEnumerator EnemyDrawperformer(DrawCard drawperformer)
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
    public IEnumerator EnemyDiscardperformer(DiscardCardsGa endiscard)
    {
        foreach (var card in EnemyHand)
        {
            EnemyDiscardpile.Add(card);
            EnemyCardview Cardview = Enumhanddetails.RemoveCard(card);
            yield return DiscardCard(Cardview);

        }
        EnemyHand.Clear();
    }
    public IEnumerator DrawCards()
    {
        Cards card = Enemydrawpile.Draw();
        EnemyHand.Add(card);
        EnemyCardview EnemyView = EnemyCardViewsCreator.Instance.CreateEnemyView(card,ENumDrawpoint.position,ENumDrawpoint.rotation, Enumhanddetails.transform);
        yield return Enumhanddetails.addCard(EnemyView);
    }
    
    private void EnRefill()
    {
        Enemydrawpile.AddRange(EnemyDiscardpile);
        EnemyDiscardpile.Clear();
    }
    
    public IEnumerator DiscardCard(EnemyCardview cardview)
    {
        cardview.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardview.transform.DOMove(EnumDiscardpoint.position, 0.15f);
        yield return tween.WaitForCompletion();      
        Destroy(cardview.gameObject);
    }
    public IEnumerator EnemyPlayCardPerformer(EnemyPlayCard playCard)
    {
        if (EnemyHand.Contains(playCard.Card))
        {
            EnemyHand.Remove(playCard.Card);
            EnemyCardview enemyCardview = Enumhanddetails.RemoveCard(playCard.Card);
            Debug.Log("Enemy played card: " + playCard.Card.Title);
            if (enemyCardview != null)
            {
                // Animate to play point
                enemyCardview.transform.DOScale(Vector3.one * 1.2f, 0.2f);
                Tween tween = enemyCardview.transform.DOMove(EnemyPlaypoint.position, 0.3f);
                yield return tween.WaitForCompletion();
                Debug.Log("Enemy card animated to play area.");
            }
            else
            {
                Debug.LogWarning("EnemyCardview is null for played card.");
            }
        }
        else
        {
            Debug.LogWarning("Card not in enemy hand; cannot play.");
        }
        yield break;
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
        //PlayerHealth.Instance.TakeDamage(attackData.Damage);
        yield return null;
    }

}
