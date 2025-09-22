using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterationSystem : Singleton<InterationSystem>
{
    public bool IsDragging { get;  set; } = false;

    public bool CanInteract()
    {
        if (!ActionSystem.Instance.Isperforming) return true;
        else return false;

    }

    public bool CanHover()
    {
        if(IsDragging) return false;
        return true;
    } 




}
