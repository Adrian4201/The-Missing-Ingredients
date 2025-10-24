using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eemyviewcreator : Singleton<Eemyviewcreator>
{
    [SerializeField] private List<Transform> slots;

    [SerializeField] private Enemyview enemyViewPrefab;

    [SerializeField] Sprite Sprite;
    public List<Enemyview> Enemyviews { get; private set; } = new List<Enemyview>();
    public Enemyview Createview(EnemyData enemyData, Vector3 position, Quaternion rotation)
    {
        if (Enemyviews.Count >= slots.Count)  // Safety check: Don't go out of bounds
        {
            Debug.LogError("No more slots available!");
            return null;
        }
         
        Enemyview enemyview = Instantiate(enemyViewPrefab, position, rotation);
        enemyview.setup(enemyData);  
       // enemyview.transform.parent = slots[Enemyviews.Count];  
        //enemyview.transform.localPosition = Vector3.zero;  
       // Enemyviews.Add(enemyview);  
        return enemyview;
    }
}
