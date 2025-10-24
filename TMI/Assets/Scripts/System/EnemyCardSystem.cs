using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class EnemyCardSystem : Singleton<EnemyCardSystem>
{
    [SerializeField] public EnemyHandView Enumhanddetails;
    [SerializeField] private Transform ENumDrawpoint;
    [SerializeField] private Transform EnumDiscardpoint;
    [SerializeField] private Transform EnemyPlaypoint;
    //list for 
    private readonly List<EnemyCards> Enemydrawpile = new();
    public readonly List<EnemyCards> EnemyDiscardpile = new();
    public readonly List<EnemyCards> EnemyHand = new();
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
    public void Setup(List<EnemyCardData> deckData)
    {
        foreach (var enemycarddata in deckData)
        {
            Debug.Log(enemycarddata);
            EnemyCards card = new(  enemycarddata);
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
        foreach (var Card in EnemyHand)
        {
            EnemyDiscardpile.Add(Card);
            EnemyCardview Cardview = Enumhanddetails.RemoveCard(Card);
            yield return DiscardCard(Cardview);

        }
        EnemyHand.Clear();
    }
    public IEnumerator DrawCards()
    {
        EnemyCards card = Enemydrawpile.Draw();
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
        if (cardview == null)
        {
            Debug.LogWarning("Tried to discard a null card view!");
            yield break;
        }
        cardview.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardview.transform.DOMove(EnumDiscardpoint.position, 0.15f);
        yield return tween.WaitForCompletion();      
        Destroy(cardview.gameObject);
    }
    public IEnumerator EnemyPlayCardPerformer(EnemyPlayCard playCard)
    {
        if (EnemyHand.Contains(playCard.Cards))
        {
            EnemyHand.Remove(playCard.Cards);
            EnemyCardview enemyCardview = Enumhanddetails.RemoveCard(playCard.Cards);
            Debug.Log("Enemy played card: " + playCard.Cards.Title);
            if (enemyCardview != null)
            {
                // Animate to play point
                enemyCardview.transform.DOScale(Vector3.one * 1.2f, 0.2f);
                Tween tween = enemyCardview.transform.DOMove(EnemyPlaypoint.position, 0.3f);
                yield return tween.WaitForCompletion();

                enemyCardview.transform.DOScale(Vector3.zero, 0.15f);
                yield return new WaitForSeconds(0.15f);
                Destroy(enemyCardview.gameObject);
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

        EnemyPlayCard playAction = new EnemyPlayCard(attack.Attack);
        //PlayerHealth.Instance.TakeDamage(attackData.Damage);
        yield return null;
    }

}
