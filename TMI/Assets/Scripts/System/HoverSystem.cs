using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoverSystem : Singleton<HoverSystem>
{
    [SerializeField] private CardDescriptions cardView;
    public void Show(Cards card, Vector3 postition)
    {
        cardView.gameObject.SetActive(true);
        cardView.Setup(card);
        cardView.transform.position = postition;

    }
    public void Hide()
    {
        cardView.gameObject.SetActive(false);
    }
}



