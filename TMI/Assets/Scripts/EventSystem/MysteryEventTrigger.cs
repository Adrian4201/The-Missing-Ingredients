using UnityEngine;

public class MysteryEventTrigger : MonoBehaviour
{
    [SerializeField] private MysteryEventData eventData;

    private void OnMouseDown()
    {
        MysteryEventManager manager = FindObjectOfType<MysteryEventManager>();
        if (manager != null)
        {
            manager.ShowEvent(eventData);
        }
    }
}
