using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Combatantviews : MonoBehaviour
{
    [SerializeField] private Slider healhText;

    [SerializeField] private SpriteRenderer spriteRenderer;

    public int Maxhealth {  get; private set; }

    public int CurrentHealth { get; private set; }
    public void setupBase(int health, Sprite image)
    {
        Maxhealth = CurrentHealth + health; 
        spriteRenderer.sprite = image;
        updateBase(health);
    }

    public void updateBase(int health)
    {
        CurrentHealth = health;
        healhText.value = CurrentHealth;
    }
}
