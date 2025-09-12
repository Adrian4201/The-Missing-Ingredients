using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardDescriptions : MonoBehaviour
{
    [SerializeField] private TMP_Text Title;

    [SerializeField] private TMP_Text mana;

    [SerializeField] private TMP_Text Description;

    [SerializeField] private SpriteRenderer imageS;

    [SerializeField] private GameObject wrapper;
    public Cards Card {  get; private set; }
    public void Setup(Cards card)
    {
        Card = card;
        Title.text = card.Title;
        Description.text = card.Description;
        mana.text = card.Mana.ToString();
        imageS.sprite = card.Image;
    }
}
