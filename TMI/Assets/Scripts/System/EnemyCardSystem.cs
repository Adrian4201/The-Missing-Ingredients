using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.XR;

public class EnemyCardSystem : Singleton<EnemyCardSystem>
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
        ActionSystem.Attachperformmer<Playcard>(PlayCardPerformer);
        ActionSystem.Attachperformmer<Dealdamage>(Dealdamageperformer);
        ActionSystem.Attachperformmer<EnemyAttack>(EnemyAttackPerformer);

    }
    private void OnDisable()
    {
        ActionSystem.Dettachperformer<DrawCard>();
        ActionSystem.Dettachperformer<DiscardCardsGa>();
        ActionSystem.Dettachperformer<Playcard>();
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
        EnemyCardview EnemyView = EnemyCardViewsCreator.Instance.CreateEnemyView(card,ENumDrawpoint.position,ENumDrawpoint.rotation);
        yield return EnemyView;
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
    public IEnumerator PlayCardPerformer(Playcard playCard)
    {
        if (!TurnSystem.Instance.canplay)
        {
            Debug.LogWarning("Blocked card play — not player's turn!");
            yield break;
        }

        EnemyHand.Remove(playCard.card);
        EnemyCardview enemyCardview = Enumhanddetails.RemoveCard(playCard.card);
        Debug.Log("Card has been play)");

        // Notify TurnSystem, but don't discard here
        TurnSystem turnSystem = FindObjectOfType<TurnSystem>();
        if (turnSystem != null)
        {
            turnSystem.EnSetPlayedCard(playCard.card,enemyCardview);  // Ensure cardview is set
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
        //PlayerHealth.Instance.TakeDamage(attackData.Damage);
        yield return PlayCardPerformer(playAction);
    }

}
