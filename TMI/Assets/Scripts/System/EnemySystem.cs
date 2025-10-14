using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    [SerializeField] private EnemyBoardview enemyBoardview;
    private void OnEnable()
    {
        ActionSystem.Attachperformmer<Enemyturn>(EnremyTurnperformer);
    }
    private void OnDisable()
    {
        ActionSystem.Dettachperformer<Enemyturn>();
    }
    public void SetUp(List<EnemyData> enemydatas)
    {
        foreach (var enemydata in enemydatas)
        {
            enemyBoardview.AddEnemy(enemydata);
        }
    }
    private IEnumerator EnremyTurnperformer(Enemyturn enemyturn)
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
}
