using UnityEngine;

public class MouseHit2DDebugger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var cam = Camera.main;
            if (!cam) { Debug.LogWarning("No Main Camera"); return; }

            var wp = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 p = new Vector2(wp.x, wp.y);

            var hits = Physics2D.OverlapPointAll(p);
            if (hits.Length == 0)
            {
                Debug.Log("Clicked nothing (2D Physics).");
                return;
            }

            foreach (var h in hits)
            {
                Debug.Log($"Clicked over: {h.name} (layer {LayerMask.LayerToName(h.gameObject.layer)})");
            }
        }
    }
}
