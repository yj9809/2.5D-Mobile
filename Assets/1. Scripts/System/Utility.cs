using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class Utility
{
    public static float ObjRendererCheck(GameObject obj)
    {
        Renderer ren = obj.GetComponent<Renderer>();

        return ren.bounds.size.y;
    }
    public static void ObjectDrop(Transform parentPos, GameObject churu, Stack<GameObject> getChuruStack, Stack<GameObject> setChuruStack, bool isCreate)
    {
        GameObject newChuru;
        if (isCreate)
            newChuru = GameObject.Instantiate(churu, parentPos);
        else
            newChuru = getChuruStack.Pop();

        newChuru.transform.SetParent(parentPos);
        newChuru.transform.DOLocalMove(new Vector3(0, 0 + (ObjRendererCheck(newChuru) * setChuruStack.Count), 0), 1f)
            .SetEase(Ease.OutQuint);
        setChuruStack.Push(newChuru);

    }
}
