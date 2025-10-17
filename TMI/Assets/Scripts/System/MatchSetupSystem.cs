using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] private List<EnemyData> enemyDatas;

    [SerializeField] private PlayerData playerDatas;
    private void Start()
    {
        HeroSystem.Instance.Setup(playerDatas);
        
        EnemySystem.Instance.SetUp(enemyDatas);
        
        CardSystem.Instance.Setup(playerDatas.Deck);

        DrawCard card = new(2);

        ActionSystem.Instance.Preform(card);
    }
}
