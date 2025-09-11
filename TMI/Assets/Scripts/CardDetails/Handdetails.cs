using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class Handdetails : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SplineContainer splines;

    private readonly List<CardDescriptions> cards = new();

    public IEnumerator AddCard(CardDescriptions cardView)
    {
        cards.Add(cardView);
        yield return UpdateCardPosition(0.15f);
    }

    private IEnumerator UpdateCardPosition(float duration) 
    {
        if(cards.Count== 0) yield break;
        float Cardspacing = 1f / 10f;
        float firstCardpostion = 0.5f - (cards.Count - 1) * Cardspacing/2;
        Spline spline = splines.Spline;
        for (int i = 0; i < cards.Count; i++)
        {
            float p = firstCardpostion + i * Cardspacing;
            Vector3 splineposition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(forward - up, Vector3.Cross(-up, forward).normalized);
            cards[i].transform.DOMove(splineposition + transform.position + 0.01f * i * Vector3.back, duration);
            cards[i].transform.DORotate(rotation.eulerAngles,duration);

        }
        yield return new WaitForSeconds(duration);

    }
}
