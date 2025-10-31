using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Combatantviews combatView;
    [SerializeField] private Sprite sprite;

    private int currentHealth;

    private void Awake()
    {
        if (combatView == null)
            combatView = GetComponentInChildren<Combatantviews>();

        if (healthBar == null)
            healthBar = GetComponentInChildren<HealthBar>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetmaxHealth(maxHealth);
        combatView.setupBase(maxHealth, sprite);
    }
    public void takedamage(Dealdamage damage)
    {
        currentHealth -= damage.Damage;
        if (currentHealth < 0) currentHealth = 0;

        healthBar.Sethealth(currentHealth);
        combatView.updateBase(currentHealth);
        Debug.Log($"{name} took {damage.Damage} damage. Current health: {currentHealth}");
    }

}
