using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum CheckType
{
    Create,
    Drop,
    Array,
    Car,
    Box
}

public static class Utility
{
    public static Bounds ObjRendererCheck(GameObject obj)
    {
        Renderer ren = obj.GetComponent<Renderer>();

        return ren.bounds;
    }
    // parentPos 이동 시킬 곳, churu 만들 오브젝트(처음 재료 만들어주는 곳에서만 사용하면 될꺼 같아서 나머지는 다 Null)
    // getChuruStack 가져올 스택(a에서 b로 옮길 때 a를 말함), setChuruStack 받을 스택(마찬가지로 b를 말함), num 타입 구분을 위한 인트
    /// <summary>
    /// 오브젝트 이동 시킬 함수.
    /// </summary>
    /// <param name="parentPos">이동 시킬 곳</param>
    /// <param name="churu">만들 오브젝트</param>
    /// <param name="getChuruStack">가져올 스택</param>
    /// <param name="setChuruStack">받을 스택</param>
    /// <param name="num">타입을 구분하는 인트</param>
    public static void ObjectDrop(Transform parentPos, GameObject churu, Stack<GameObject> getChuruStack, Stack<GameObject> setChuruStack, int num)
    {
        GameObject newChuru;

        if (num == (int)CheckType.Create)
        {
            newChuru = PoolingManager.Instance.GetObj(churu);
            newChuru.name = churu.name;
            newChuru.transform.SetParent(parentPos);
            newChuru.transform.localPosition = new Vector3(0, (ObjRendererCheck(newChuru).size.y * setChuruStack.Count), 0);
        }
        else
        {
            if (num == (int)CheckType.Drop)
            {
                newChuru = getChuruStack.Pop();
                newChuru.transform.DOLocalMove(new Vector3(0, 0 + (ObjRendererCheck(newChuru).size.y * setChuruStack.Count), 0), 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() => newChuru.transform.localRotation = Quaternion.Euler(0, 0, 0));
            }
            // 임시로  Quaternion.Euler(270, 0, 0)) 이렇게 바꿔놨기 때문에 추후 프리팹이 제대로 나오면  Quaternion.Euler(0, 0, 0)) 으로 바꿔야함
            else if (num == (int) CheckType.Array)
            {
                newChuru = churu;
                Debug.Log(setChuruStack.Count % 10);
                newChuru.transform.DOLocalMove(new Vector3(0, 0 + (ObjRendererCheck(newChuru).size.y * (setChuruStack.Count % 10)), 0), 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() => newChuru.transform.localRotation = Quaternion.Euler(0,0,0));
            }
            else if(num == (int) CheckType.Car)
            {
                newChuru = getChuruStack.Pop();
                newChuru.transform.DOMove(parentPos.position, 0.2f).SetEase(Ease.InBack);
            }
            // 임시로  Quaternion.Euler(270, 0, 90)) 이렇게 바꿔놨기 때문에 추후 프리팹이 제대로 나오면  Quaternion.Euler(0, 0, 0)) 으로 바꿔야함
            else
            {
                newChuru = getChuruStack.Pop();
                newChuru.transform.DOLocalMove(new Vector3(0, 0 + (ObjRendererCheck(newChuru).size.x * setChuruStack.Count), 0), 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() => newChuru.transform.localRotation = Quaternion.Euler(270, 0, 90));
            }
        }
        newChuru.transform.SetParent(parentPos);

        if(setChuruStack != null)
            setChuruStack.Push(newChuru);
    }
}
