using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActionSystem : Singleton<ActionSystem>
{
    private List<GameAction> reaction = null;

    public bool Isperforming { get; private set; } = false;
    private static Dictionary<Type, List<Action<GameAction>>> preSubs = new();

    private static Dictionary<Type, List<Action<GameAction>>> postSubs = new();

    private static Dictionary<Type, Func<GameAction, IEnumerator>> Preformers = new();

    public void Preform(GameAction action, System.Action OnPreformFinished = null)
    {
        if (Isperforming) return;
        Isperforming = true;
        StartCoroutine(Flow(action, () =>
        {
            Isperforming = false;
            OnPreformFinished?.Invoke();


        }));
    }
    public void AddAction(GameAction action)
    {
        reaction?.Add(action);
    }
    private IEnumerator Flow(GameAction action, Action OnFlowFininshed = null) 
    {
        reaction = action.Prereaction;
        
    }

    

}
