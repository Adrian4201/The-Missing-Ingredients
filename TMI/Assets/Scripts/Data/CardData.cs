using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Card")]
public class CardData : MonoBehaviour
{
    [field: SerializeField] public string  Description { get; private set; }
    [field: SerializeField] public int Mana { get; private set; }
    [field: SerializeField]public Sprite Image { get; private set; }
}
