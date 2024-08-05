using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static float ObjRendererCheck(GameObject obj)
    {
        Renderer ren = obj.GetComponent<Renderer>();

        return ren.bounds.size.y;
    }
}
