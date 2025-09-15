using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

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
        PerformSubscribers(action, preSubs);
        yield return action;

        reaction = action.PerformReaction;
        yield return PerformPerformer(action);
        yield return PerformReaction();

        reaction = action.PostReaction;
        PerformSubscribers(action, postSubs);
        yield return PerformReaction();

        OnFlowFininshed?.Invoke();
    }
    private IEnumerator PerformPerformer(GameAction action)
    {
        Type type = action.GetType();
        if (Preformers.ContainsKey(type))
        {
            yield return Preformers[type] (action);
        }

    }
    public IEnumerator PerformReaction()
    {
        foreach(var reaction in reaction)
        {
            yield return Flow(reaction);
        }
    }
    public static void Attachperformmer<T>(Func<T, IEnumerator> Preformer) where T : GameAction
    {
        Type type = typeof(T);
        IEnumerator wrappedperformer(GameAction action) => Preformer((T)action);
        if( Preformers.ContainsKey(type)) Preformers[type] = wrappedperformer;
        else Preformers.Add(type, wrappedperformer);
    }
    public static void Dettachperformer<T>() where T : GameAction
    {
        Type type =typeof(T);
        if (Preformers.ContainsKey(type)) Preformers.Remove(type);
    }
    public static void SubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {

    }

    public static void UnSubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {

    }
    private void PerformSubscribers(GameAction action, Dictionary<Type, List<Action<GameAction>>> Subs)
    {
        Type type = action.GetType();
        if (Subs.ContainsKey(type))
        {
            foreach (var sub in Subs[type])
            {
                sub(action);
            }
        }
    }
    

}
