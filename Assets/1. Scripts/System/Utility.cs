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
        {
            newChuru = GameObject.Instantiate(churu, parentPos);
            newChuru.transform.localPosition = new Vector3(0, (ObjRendererCheck(newChuru) * setChuruStack.Count), 0);
        }
        else
        {
            newChuru = getChuruStack.Pop();
            newChuru.transform.DOLocalMove(new Vector3(0, 0 + (ObjRendererCheck(newChuru) * setChuruStack.Count), 0), 0.7f)
            .SetEase(Ease.InBack);
        }

        newChuru.transform.SetParent(parentPos);
        setChuruStack.Push(newChuru);
    }
}
