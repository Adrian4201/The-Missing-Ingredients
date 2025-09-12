using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Handdetails handView;
    [SerializeField] private CardData cardData;
    // Start is called before the first frame update
    
    private void Update()
    {
        if( Input.GetKeyUp(KeyCode.Space))
        {
            Cards card = new(cardData);
            CardDescriptions cardview = CardviewsCreator.Instance.CreateCardView(card, transform.position, Quaternion.identity);
            StartCoroutine(handView.AddCard(cardview));
        }
    }

}
