using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemyview : Combatantviews
{
    [SerializeField] private TMP_Text Attack;

    public int attackPower { get; set; }

    public void setup()
    {
        attackPower = 10;
        UpdatedAttack();
        setupBase(attackPower, null);
    }
    private void UpdatedAttack()
    {
        Attack.text = "ATK" + attackPower;
    }
}
