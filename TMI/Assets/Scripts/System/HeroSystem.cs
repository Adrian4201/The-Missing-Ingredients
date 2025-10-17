using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSystem : Singleton<HeroSystem>
{
    [field: SerializeField] public Playerviews Hero {  get; private set; }
    public void Setup(PlayerData playerData)
    {
        Hero.setup(playerData);
    }
}
