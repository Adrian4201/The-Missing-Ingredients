using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndturnUI : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClick()
    {
        Debug.Log("Shit");
        Enemyturn enemy = new();
        ActionSystem.Instance.Preform(enemy);
    }
}
