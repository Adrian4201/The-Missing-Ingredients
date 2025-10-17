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
        if(spriteRenderer == null)
        {
            Debug.Log("sprite render is not assigned");
            return;
        }
        Maxhealth = CurrentHealth + health; 
        spriteRenderer.sprite = image;
        updateBase(health);
    }

    public void updateBase(int health)
    {
        if (healhText == null)
        {
            Debug.Log("Health isnt assigned in comview");
            return;
        }
        CurrentHealth = health;
        healhText.value = CurrentHealth;
    }
}
