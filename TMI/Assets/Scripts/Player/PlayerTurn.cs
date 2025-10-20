using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerTurn : GameAction
{
    private CardSystem cardSystem;
    private CardDescriptions Cardview;

    public IEnumerator EXcutePlayerturn()
    {
        
        DrawCard draw = new(2);
        ActionSystem.Instance.Preform(draw);
        yield return new WaitUntil(() => !ActionSystem.Instance.Isperforming);
        

    }
}
