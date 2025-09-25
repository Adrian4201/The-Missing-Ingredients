using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
    public int maxHealth = 100;
    public int CurrentHealth;
    public HealthBar healthBar;
    void Start()
    {
        CurrentHealth = maxHealth;
        healthBar.SetmaxHealth(maxHealth);
    }

    public void takedamage(Dealdamage damage)
    {
        
    }
}
