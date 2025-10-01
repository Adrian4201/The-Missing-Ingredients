using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Cards")]
public class CardData : ScriptableObject
{
    [field: SerializeField] public string Title { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public Sprite CardImage { get; private set; }

    // NEW: Card categorization
    [field: SerializeField] public CardRarity Rarity { get; private set; }
    [field: SerializeField] public CardColor Color { get; private set; }
    // NEW: Keywords
    [field: SerializeField] public List<CardKeyword> Keywords { get; private set; }

    // Existing Effects list
    [field: SerializeField] public List<CardEffects> Effects { get; private set; }
}



// ------------------ KEYWORD STRUCT ------------------
[System.Serializable]
public class CardKeyword
{
    public KeywordType Keyword;        // e.g. Poison, Heal, Expose
    public int Value;                  // Used if keyword has variable strength (e.g. Heal 10, Poison 3)
    public TriggerType Trigger;        // When it applies
    public ConditionType Condition;    // Optional condition (e.g. if target < 50% HP)
}
