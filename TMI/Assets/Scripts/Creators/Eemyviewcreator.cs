using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eemyviewcreator : Singleton<Eemyviewcreator>
{
    [SerializeField] private Enemyview eneyprefab;

    public Enemyview Createview(EnemyData enemyData, Vector3 position, Quaternion rotation)
    {
        Enemyview enemyview = Instantiate(eneyprefab, position, rotation);
        enemyview.setup(enemyData);
        return enemyview;
    }
}
