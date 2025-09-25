using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
    public int maxHealth = 100;
    public int CurrentHealth;
    public HealthBar healthBar;
    [SerializeField] private Combatantviews combatView;
    void Start()
    {
        CurrentHealth = maxHealth;
        healthBar.SetmaxHealth(maxHealth);
        combatView.setupBase(maxHealth, combatView.GetComponent<SpriteRenderer>().sprite);
    }
    public void takedamage(Dealdamage damage)
    {
        CurrentHealth -= damage.Damage;
        if (CurrentHealth < 0) CurrentHealth = 0;

        healthBar.Sethealth(CurrentHealth);
        combatView.updateBase(CurrentHealth); // add this helper in Combatantviews
    }
}
