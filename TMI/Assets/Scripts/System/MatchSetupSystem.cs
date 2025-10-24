using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] private List<EnemyData> enemyDatas;

    [SerializeField] private PlayerData playerDatas;
    [SerializeField] private EnemyData enemyData;
    
    private void Start()
    {
        HeroSystem.Instance.Setup(playerDatas);
        
        EnemySystem.Instance.SetUp(enemyDatas);
        
        CardSystem.Instance.Setup(playerDatas.Deck);

        EnemyCardSystem.Instance.Setup(enemyData.Deck);  
        Debug.Log("EnemyCardSystem set up with shared deck (" + enemyData.Deck.Count + " cards, same as player).");

        DrawCard card = new(2);
        ActionSystem.Instance.Preform(card);

        DrawCard enemyDraw = new DrawCard(1);
        StartCoroutine(EnemyCardSystem.Instance.EnemyDrawperformer(enemyDraw));
        Debug.Log("Player drew 2 cards; Enemy drew 1 card from shared deck.");
    }
}
