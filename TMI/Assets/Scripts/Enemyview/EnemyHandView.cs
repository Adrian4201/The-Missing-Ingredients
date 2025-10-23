using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyHandView : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SplineContainer splinecon;
    private readonly List<EnemyCardview> cards = new();

    public IEnumerator addCard(EnemyCardview card)
    {
        card.transform.SetParent(transform, worldPositionStays: false);
        cards.Add(card);
        yield return UpdatecardpostionEn(0.15f);
    }
    public EnemyCardview RemoveCard(Cards card)
    {
        EnemyCardview cardView = GetCardview(card);
        if (cardView == null) return null;
        cards.Remove(cardView);
        StartCoroutine(UpdatecardpostionEn(0.15f));
        return cardView;

    }
    private EnemyCardview GetCardview(Cards cardss)
    {
        return cards.Where(Enemyview => Enemyview.Card == cardss).FirstOrDefault();
    }
    
    private IEnumerator UpdatecardpostionEn(float time)
    {
        if (cards.Count == 0) yield break;
        float CardSpacing = 1f / 10f;
        float firstCardpostion = 0.5f -(cards.Count - 1)* CardSpacing / 2;
        Spline spline = splinecon.Spline;
        for (int i = 0; i < cards.Count; i++)
        {
            float p = firstCardpostion + i * CardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(forward - up, Vector3.Cross(-up, forward).normalized);

            cards[i].transform.DOMove(splinePosition + transform.position + 0.01f * i * Vector3.back, time);
            cards[i].transform.DORotate(new Vector3(0, 0, rotation.eulerAngles.z), time);
        }

        yield return new WaitForSeconds(time);
    }
}
