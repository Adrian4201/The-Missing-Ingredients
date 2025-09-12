using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : Singleton<ActionSystem>
{
    private List<GameAction> reaction = null;

    public bool Isperforming { get; private set; } = false;
    private static Dictionary<Type, List<Action<GameAction>>> preSubs = new();

    private static Dictionary<Type, List<Action<GameAction>>> postSubs = new();

    private static Dictionary<Type, Func<GameAction, IEnumerator>> Preformers = new();

    public void Preform(GameAction action, System.Action OnPreformFinished = null);


}
