using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Mystery Event")]
public class MysteryEventData : ScriptableObject
{
    public string eventName;
    [TextArea(3, 6)] public string description;
    public Sprite eventImage;
    public List<EventOption> options;
}

[System.Serializable]
public class EventOption
{
    public string optionText;
    [TextArea(2, 4)] public string resultText;
    public int healthChange;    // + or - (use your PlayerStats API)
    public int goldChange;
    public bool endsEvent = true;
}
