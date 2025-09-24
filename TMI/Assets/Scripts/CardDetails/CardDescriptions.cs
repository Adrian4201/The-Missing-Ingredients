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

    private Vector3 DragStartPos;

    public Quaternion DragRotation;
    public void Setup(Cards card)
    {
        Card = card;
        Title.text = card.Title;
        Description.text = card.Description;
        mana.text = card.Mana.ToString();
        imageS.sprite = card.Image;
        
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
<<<<<<< HEAD
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
=======
>>>>>>> parent of 23bb525 (addddrag)
}
