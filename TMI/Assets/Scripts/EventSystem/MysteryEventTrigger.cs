using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MysteryEventTrigger : MonoBehaviour
{
    [SerializeField] private MysteryEventData eventData;
    [SerializeField] private bool oneTimeUse = true;

    private bool triggered;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var cam = Camera.main;
            if (!cam) return;

            var wp = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 p = new Vector2(wp.x, wp.y);

            // Check only THIS object’s collider first (fast & precise)
            var col = GetComponent<Collider2D>();
            if (col != null && col.OverlapPoint(p))
            {
                Trigger();
                return;
            }

            // If your collider might be on a child, use this instead:
            // var hit = Physics2D.OverlapPoint(p);
            // if (hit && (hit.transform == transform || hit.transform.IsChildOf(transform)))
            //     Trigger();
        }
    }

    private void Trigger()
    {
        if (oneTimeUse && triggered) return;
        triggered = true;

        var manager = FindObjectOfType<MysteryEventManager>();
        if (manager != null)
        {
            manager.ShowEvent(eventData);
            Debug.Log($"Mystery event triggered: {(eventData ? eventData.name : "NULL")}");
        }
        else
        {
            Debug.LogWarning("MysteryEventManager not found in scene.");
        }
    }
}
