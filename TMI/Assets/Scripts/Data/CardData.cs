using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Cards")]
public class CardData : ScriptableObject
{
    [field: SerializeField] public string  Description { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField]public Sprite Image { get; private set; }
    [field: SerializeField] public Sprite CardImage { get; private set; }
}
