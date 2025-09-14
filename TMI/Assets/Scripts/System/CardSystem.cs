using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardSystem : MonoBehaviour
{
    [SerializeField] private Cards cardprefab;

    [SerializeField] private Transform spawn;

    [SerializeField] private Transform hand;

    private void OnEnable()
    {
        ActionSystem.Attachperformer<DrawCard>(DrawCardPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCard>();
    }

    private IEnumerator DrawCardPerformer(DrawCard Drawcards)
    {
        Cards card = Instantiate(cardprefab, spawn.position, Quaternion.identity);
        Tween tween = card.transform.DoMove(hand.position, 0.5f);
        yield return tween.WaitForCompletion();
    }
}
