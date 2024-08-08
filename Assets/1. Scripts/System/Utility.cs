using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum CheckType
{
    Create,
    Drop,
    Array
}

public static class Utility
{
    public static float ObjRendererCheck(GameObject obj)
    {
        Renderer ren = obj.GetComponent<Renderer>();

        return ren.bounds.size.y;
    }

    public static void ObjectDrop(Transform parentPos, GameObject churu, Stack<GameObject> getChuruStack, Stack<GameObject> setChuruStack, int num)
    {
        GameObject newChuru;

        if (num == (int)CheckType.Create)
        {
            newChuru = PoolingManager.Instance.GetObj(churu);
            newChuru.transform.SetParent(parentPos);
            newChuru.transform.localPosition = new Vector3(0, (ObjRendererCheck(newChuru) * setChuruStack.Count), 0);
        }
        else 
        {
            if (num == (int)CheckType.Drop)
            {
                newChuru = getChuruStack.Pop();
                newChuru.transform.DOLocalMove(new Vector3(0, 0 + (ObjRendererCheck(newChuru) * setChuruStack.Count), 0), 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() => newChuru.transform.localRotation = Quaternion.Euler(0, 0, 0));
            }
            else
            {
                newChuru = churu;
                newChuru.transform.DOLocalMove(new Vector3(0, 0 + (ObjRendererCheck(newChuru) * (setChuruStack.Count % 10)), 0), 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() => newChuru.transform.localRotation = Quaternion.Euler(0, 0, 0));
            }

            
        }
        newChuru.transform.SetParent(parentPos);
        setChuruStack.Push(newChuru);
    }
}
