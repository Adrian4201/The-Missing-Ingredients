using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoardview : MonoBehaviour
{
    [SerializeField] private List<Transform> Slots;

    public List<Enemyview> enemyviews { get; private set; } = new();

    public void AddEnemy(EnemyData enemyData)
    {
        Transform slot = Slots[enemyviews.Count];
        Enemyview enemyview = Eemyviewcreator.Instance.Createview(enemyData,slot.position, slot.rotation);
        enemyview.transform.parent = slot;
        enemyviews.Add(enemyview);
    }
}
