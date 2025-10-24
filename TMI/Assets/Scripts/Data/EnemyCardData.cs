using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyCards")]
public class EnemyCardData : ScriptableObject
{
    [field: SerializeField] public string Title { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public Sprite CardImage { get; private set; }

    //color and rarity
    [field: SerializeField] public CardColor Color { get; private set; }


}
