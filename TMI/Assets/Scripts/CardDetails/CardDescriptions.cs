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
    [SerializeField] private LayerMask DropArea;

    public Cards Card { get; private set; }
    private Vector3 DragStartPos;
    public Quaternion DragRotation;
    
    public void Setup(Cards card)
    {
        if(card == null)
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
        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
        // drag logic here
    }

    private void OnMouseUp()
    {
        if (!InterationSystem.Instance.CanInteract()) return;
        if(Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, DropArea))
        {
            Playcard playcard = new(Card);
            ActionSystem.Instance.Preform(playcard);
        }
        else
        {
            transform.position = DragStartPos;
            transform.rotation = DragRotation;
        }
        // release drag logic
    }
}
