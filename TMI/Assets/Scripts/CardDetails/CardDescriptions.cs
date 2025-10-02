using TMPro;
using UnityEngine;

public class CardDescriptions : MonoBehaviour
{
    [Header("Card Elements")]
    [SerializeField] private TMP_Text Title;

    [SerializeField] private TMP_Text Damage;

    [SerializeField] private TMP_Text Description;

    //[SerializeField] private TMP_Text RarityText;   //  NEW
    [SerializeField] private SpriteRenderer imageS;
    [SerializeField] private SpriteRenderer cardBackground;
    [SerializeField] private GameObject wrapper;

    public Cards Card { get; private set; }

    private Vector3 DragStartPos;
    public Quaternion DragRotation;

    public void Setup(Cards card)
    {
        Card = card;

        // Fill in card content
        Title.text = card.Title;
        Description.text = card.Description;
        Damage.text = card.Damage.ToString();
        imageS.sprite = card.Image;

        //  Apply type color (for damage + background)
        if (CardColorMap.Colors.TryGetValue(card.Color, out var typeColor))
        {
            Damage.color = typeColor;
            cardBackground.color = typeColor;
        }

        //  Apply rarity text
       // RarityText.text = card.Rarity.ToString();

        
    }

    void OnMouseEnter()
    {
        if (!InterationSystem.Instance.CanHover()) return;
        wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2);
        HoverSystem.Instance.Show(Card, pos);
        Debug.Log("Hover activated");
    }

    void OnMouseExit()
    {
        if (!InterationSystem.Instance.CanHover()) return;
        HoverSystem.Instance.Hide();
        wrapper.SetActive(true);
        Debug.Log("Hover ended");
    }

    private void OnMouseDown()
    {
        if (!InterationSystem.Instance.CanInteract()) return;
        InterationSystem.Instance.IsDragging = true;
        wrapper.SetActive(true);
        HoverSystem.Instance.Hide();
        DragStartPos = transform.position;
        DragRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnMouseDrag()
    {
        if (!InterationSystem.Instance.CanInteract()) return;
        // drag logic here
    }

    private void OnMouseUp()
    {
        if (!InterationSystem.Instance.CanInteract()) return;
        // release drag logic
    }
}
