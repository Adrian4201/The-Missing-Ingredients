using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy")]

public class EnemyData : ScriptableObject
{
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public int Health {  get; private set; }
    [field : SerializeField] public int Attackpower {  get; private set; }

    [field: SerializeField] public CardData Card { get; private set; }

}
