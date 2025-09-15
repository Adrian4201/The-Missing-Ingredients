using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.Attachperformmer<Enemyturn>(EnremyTurnperformer);
    }
    private void OnDisable()
    {
        
    }
    private IEnumerator EnremyTurnperformer(Enemyturn enemyturn)
    {
        Debug.Log("enemyturn)");
        yield return new WaitForSeconds(2f);
    }
}
