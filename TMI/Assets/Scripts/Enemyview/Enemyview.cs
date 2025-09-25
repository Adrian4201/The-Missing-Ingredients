using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemyview : MonoBehaviour
{
    [SerializeField] private TMP_Text Attack;

    public int attackPower { get; set; }

    public void setup()
    {
        attackPower = 10;
        UpdatedAttack();
        
    }
    private void UpdatedAttack()
    {
        Attack.text = "ATK" + attackPower;
    }
}
