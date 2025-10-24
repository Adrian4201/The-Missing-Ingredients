
using UnityEngine;

public class EnemyCards 
{
    public string Title => data.name;
    public string Description => data.Description;
    public Sprite Image => data.Image;
    public int Damage { get; private set; }

    public CardColor Color => data.Color;

    public readonly EnemyCardData data;
    public EnemyCards (EnemyCardData card)
    {
        data = card;
        Damage = card.Damage;
    }
}
