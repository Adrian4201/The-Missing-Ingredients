public enum CardKeyword
{
    Surge,      // Move again
    Shuffle,    // Shuffle remaining cards
    Reshuffle,  // Shuffle all cards
    Drop,       // On discard effect
    Barrier,    // Temporary HP
    Expose,     // Take more damage
    Poise,      // Deal less damage
    Poison,     // Damage over time
    Stale,      // Next attack weaker
    Heated,     // Next attack stronger
    Heal        // Restore HP
}
public enum TriggerType
{
    OnPlay,       // When the card is played
    OnAttack,     // When the card attacks
    OnHit,        // When this card deals damage
    OnDiscard,    // When the card is discarded
    StartOfTurn,  // At the start of a turn
    EndOfTurn,    // At the end of a turn
    NextTurnEnd   // Special delayed effect
}

[System.Serializable]
public struct KeywordData
{
    public CardKeyword Keyword;   // e.g. Poison, Surge
    public int Value;             // e.g. Heal amount, Poison ticks
    public TriggerType Trigger;   // When it happens
    public ConditionType Condition; // Optional condition
}

public enum ConditionType
{
    None,              // Always applies
    TargetBelow50HP,   // Only applies if target is under half health
    PlayerBelow50HP,   // Example: your own HP check
    HasBarrier,        // Example: only if target has Barrier
    // You can add more conditions as needed
}
