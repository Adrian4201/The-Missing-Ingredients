using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    [SerializeField] private EnemyBoardview enemyBoardview;
    private void OnEnable()
    {
        //ActionSystem.Attachperformmer<Enemyturn>(EnremyTurnperformer);
        ActionSystem.Attachperformmer<AttackHero>(AttackHeroPerformer);
    }
    private void OnDisable()
    {
        ActionSystem.Dettachperformer<Enemyturn>();
        ActionSystem.Dettachperformer<AttackHero>();

    }
    public void SetUp(List<EnemyData> enemydatas)
    {
        foreach (var enemydata in enemydatas)
        {
            enemyBoardview.AddEnemy(enemydata);
        }
    }
    public Enemyview GetCurrentEnemy()
    {
        return enemyBoardview.enemyviews[0];
    }
    /*private IEnumerator EnremyTurnperformer(Enemyturn enemyturn)
    {
        DrawCard enemydraw = new DrawCard(1);
        ActionSystem.Instance.Preform(enemydraw);
        yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);

        if (CardSystem.Instance.Hand.Count > 0)
        {
            Cards card = CardSystem.Instance.Hand[0];
            Debug.Log("Enemy picked up card: " + card.Title);

            // Perform enemy attack
            EnemyAttack enemAttack = new EnemyAttack(card);
            ActionSystem.Instance.AddAction(enemAttack);
            ActionSystem.Instance.Preform(enemAttack);
            yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);
        }
        else
        {
            Debug.LogError("No cards for enemy!");
        }

        Debug.Log("Enemy turn completed");
    }
    */
    public IEnumerator AttackHeroPerformer(AttackHero attackhero)
    {
        Enemyview attacker = attackhero.Attacker;
        //wait for move animation
        Tween tween = attacker.transform.DOMoveX(attacker.transform.position.x - 1f, 0.15f);
        yield return tween.WaitForCompletion();

        HealthTracker heroHealth = HeroSystem.Instance.Hero.GetComponent<HealthTracker>();
        if (heroHealth == null)
        {
            Debug.LogError("Hero does not have a HealthTracker component!");
            yield break;
        }
        //procedd with damage
        Dealdamage dealDamage = new(attacker.attackPower,new List<HealthTracker> { heroHealth });
        ActionSystem.Instance.AddAction(dealDamage);
    }

}
