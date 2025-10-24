using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

public class EnemyCardViewsCreator : Singleton<EnemyCardViewsCreator>
{
    [SerializeField] private EnemyCardview enemyCardviewPrefab;
    public EnemyCardview CreateEnemyView(EnemyCards card, Vector3 postition ,Quaternion rotation, Transform parent)
    {
        EnemyCardview enemyview = Instantiate(enemyCardviewPrefab, postition,rotation, parent);
        enemyview.transform.localScale = Vector3.zero;
        enemyview.transform.DOScale(Vector3.one, 0.15f);
        enemyview.Setup(card);
        Debug.Log("hi");
        return enemyview;
    }
}
