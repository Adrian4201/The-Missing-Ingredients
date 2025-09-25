using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardDescriptions : MonoBehaviour
{
    [SerializeField] private TMP_Text Title;

    [SerializeField] private TMP_Text Damage;

    [SerializeField] private TMP_Text Description;

    [SerializeField] private SpriteRenderer imageS;

    [SerializeField] private TMP_Text rarityText;


    [field: SerializeField] public CardType Type { get; private set; } // new

    [SerializeField] private GameObject wrapper;
   
    public Cards Card {  get; private set; }

    private Vector3 DragStartPos;

    public Quaternion DragRotation;
    public enum CardType
    {
        Red, Orange, Yellow, Green, Blue, Purple
    }

    public void Setup(Cards card)
    {
        Card = card;
        Title.text = card.Title;
        Description.text = card.Description;
        Damage.text = card.Damage.ToString();
        imageS.sprite = card.Image;

        // Color the damage text based on CardType
        Damage.color = TypeColorMap.Colors[card.Type];

        rarityText.text = RarityLabelMap.Labels[card.Rarity];

    }
    public static class TypeColorMap
    {
        public static readonly Dictionary<CardType, Color> Colors = new()
    {
        { CardType.Red, Color.red },
        { CardType.Orange, new Color(1f, 0.5f, 0f) },   // orange
        { CardType.Yellow, Color.yellow },
        { CardType.Blue, Color.blue },
        { CardType.Purple, new Color(0.5f, 0f, 0.5f) }  // purple
    };
    }

    void OnMouseEnter()
    {
        if (!InterationSystem.Instance.CanHover()) return;
        wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, - 2);
        HoverSystem.Instance.Show(Card, pos);  
        Debug.Log("I work");

        
    }
    void OnMouseExit()
    {
        if (!InterationSystem.Instance.CanHover()) return;
        HoverSystem.Instance.Hide();
        wrapper.SetActive(true);
        Debug.Log("shit myself again");
    }

    private void OnMouseDown()
    {
       if (!InterationSystem.Instance.CanInteract()) return;
        InterationSystem.Instance.IsDragging = true;
        wrapper.SetActive(true);
        HoverSystem.Instance.Hide();
        DragStartPos = transform.position;
        DragRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(0,0,0);
        //transform.position = MouseUtil.GetMousePositionInWorldSpace();

    }
    private void OnMouseDrag()
    {
        if (!InterationSystem.Instance.CanInteract()) return;
    }
    private void OnMouseUp()
    {
        if (!InterationSystem.Instance.CanInteract()) return;
    }

}
