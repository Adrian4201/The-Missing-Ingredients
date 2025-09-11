using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Handdetails handView;
    // Start is called before the first frame update
    
    private void Update()
    {
        if( Input.GetKeyUp(KeyCode.Space))
        {
            CardDescriptions cardview = CardviewsCreator.Instance.CreateCardView(transform.position, Quaternion.identity);
            StartCoroutine(handView.AddCard(cardview));
        }
    }

}
