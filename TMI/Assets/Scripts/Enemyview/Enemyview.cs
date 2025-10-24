using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemyview : Combatantviews
{
    //[SerializeField] private TMP_Text Attack;

    public int attackPower { get; set; }

    public void setup(EnemyData enemyData)
    {
        Debug.Log($"EnemyData Image: {enemyData.Image}");
        attackPower = 10;
        //UpdatedAttack();
        setupBase(enemyData.Health, enemyData.Image);
        Debug.Log(enemyData.Image);
    }
    /*private void UpdatedAttack()
    {
        Attack.text = "ATK" + attackPower;
    }
    */
}
