using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class CardviewsCreator : Singleton<CardviewsCreator>
{
    // Start is called before the first frame update
    [SerializeField] private CardDescriptions cardviewprefab;
    public CardDescriptions CreateCardView(Cards cards, Vector3 position, Quaternion rotation)
    {
        CardDescriptions cardview = Instantiate(cardviewprefab, position, rotation);
        cardview.transform.localScale = Vector3.zero;
        cardview.transform.DOScale(Vector3.one, 0.15f);
        cardview.Setup(cards);
        return cardview;


    }
}
