using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseUtil 
{
    private static Camera  camera = Camera.main;
    // Start is called before the first frame update
    public static Vector3 GetMousePositionInWorldSpace(float zValue = 0f)
    {
        Plane dragePlane = new(camera.transform.forward, new Vector3(0, 0, zValue));
        Ray ray =camera.ScreenPointToRay(Input.mousePosition);
        if(dragePlane.Raycast(ray,out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }

   
}
