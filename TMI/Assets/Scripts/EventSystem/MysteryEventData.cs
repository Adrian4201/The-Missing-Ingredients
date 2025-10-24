using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Mystery Event")]
public class MysteryEventData : ScriptableObject
{
    [Header("Event Info")]
    public string eventName;
    [TextArea(3, 6)] public string description;
    public Sprite eventImage;

    [Header("Event Options")]
    public List<EventOption> options;
}

[System.Serializable]
public class EventOption
{
    [Header("Option Display")]
    public string optionText;           // What the player sees as the button text
    [TextArea(2, 4)] public string resultText; // What happens when they click it

    [Header("Effects")]
    public int healthChange;
    public int goldChange;

    [Header("Flow Control")]
    public bool endsEvent = true; // If false, show result text and wait for player to continue
}
