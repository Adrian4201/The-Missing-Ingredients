using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public Dealdamage damage;
    int maxHealth = 100;
    int health = 0;
    public void Update()
    {
        
    }
}
