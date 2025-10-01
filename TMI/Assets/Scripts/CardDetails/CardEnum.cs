// Assets/Scripts/Models/CardEnums.cs
using UnityEngine;


public enum CardRarity { Common, Rare, Epic, Legendary }
public enum CardColor { Red, Orange, Yellow, Green, Blue, Purple }

public enum KeywordType
{
    Surge, Shuffle, Reshuffle, Barrier, Expose, Poise,
    Poison, Drop, Stale, Heated, Heal
}

public enum TriggerType
{
    OnPlay, OnDiscard, OnTurnStart, OnTurnEnd, Conditional
}

public enum ConditionType
{
    None, TargetBelow50PercentHP, PlayerBelow50PercentHP
}
