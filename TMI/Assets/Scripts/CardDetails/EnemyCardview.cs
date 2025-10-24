using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCardview : MonoBehaviour
{
    [Header("Card Elements")]
    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text Damage;
    [SerializeField] private TMP_Text Description;

    //[SerializeField] private TMP_Text RarityText;   //  NEW
    [SerializeField] private SpriteRenderer imageS;
    [SerializeField] private SpriteRenderer cardBackground;
    [SerializeField] private GameObject wrapper;
    [SerializeField] private LayerMask DropArea;

    public EnemyCards Card { get; private set; }
    

    public void Setup(EnemyCards card)
    {
        if (card == null)
        {
            Debug.LogError("Card is null!!");
            return;
        }
        Card = card;

        // Fill in card content
        if (Title != null) Title.text = card.Title;
        else Debug.LogError("Title is not assigned in CardDescriptions!");
        if (Description != null) Description.text = card.Description;
        else Debug.LogError("Description is not assigned in CardDescriptions!");
        if (Damage != null) Damage.text = card.Damage.ToString();
        else Debug.LogError("Damage is not assigned in CardDescriptions!");
        if (imageS != null) imageS.sprite = card.Image;
        else Debug.LogError("imageS is not assigned in CardDescriptions!");
    }
}
