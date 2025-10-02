using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] private List<EnemyData> enemyDatas;

    [SerializeField] private List<CardData> cardData;
    private void Start()
    {
        CardSystem.Instance.Setup(cardData);
        EnemySystem.Instance.SetUp(enemyDatas);
        DrawCard card = new(2);
        ActionSystem.Instance.Preform(card);
    }
}
