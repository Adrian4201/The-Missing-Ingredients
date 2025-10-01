using UnityEngine;

public class Cards
{
    public string Title => data.name;
    public string Description => data.Description;
    public Sprite Image => data.Image;
    public int Damage { get; private set; }

    public CardRarity Rarity => data.Rarity;
    public CardColor Color => data.Color;

    private readonly CardData data;

    public Cards(CardData card)
    {
        data = card;
        Damage = card.Damage;
    }
}
