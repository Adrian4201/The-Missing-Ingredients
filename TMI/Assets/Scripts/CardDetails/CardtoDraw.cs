using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardtoDraw : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseDown()
    {
        if (ActionSystem.Instance.Isperforming) return;
        DrawCard draw = new();
        ActionSystem.Instance.Preform(draw);
        Destroy(gameObject);
    }
}
