using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSystem : MonoBehaviour
{
    //[SerializeField] private Handdetails handdetails;
    //[SerializeField] private CardData cardData;
    [SerializeField] private List<CardData> Deckdata;
    // Start is called before the first frame update


    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Cards card = new(cardData);
            CardDescriptions cardview = CardviewsCreator.Instance.CreateCardView(card,transform.position, Quaternion.identity);
            StartCoroutine(handdetails.AddCard(cardview));

        }
    }
    */

    private void Start()
    {

        Debug.Log("Work damint");
        CardSystem.Instance.Setup(Deckdata);


    }
    

}
