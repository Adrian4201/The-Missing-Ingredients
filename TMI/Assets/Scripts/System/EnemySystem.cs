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
        Debug.Log("enemyturn");
        yield return new WaitForSeconds(2f);
        Debug.Log("turn done");
    }
}
