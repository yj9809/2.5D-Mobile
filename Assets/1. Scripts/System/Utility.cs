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
    // parentPos �̵� ��ų ��, churu ���� ������Ʈ(ó�� ��� ������ִ� �������� ����ϸ� �ɲ� ���Ƽ� �������� �� Null)
    // getChuruStack ������ ����(a���� b�� �ű� �� a�� ����), setChuruStack ���� ����(���������� b�� ����), num Ÿ�� ������ ���� ��Ʈ
    /// <summary>
    /// ������Ʈ �̵� ��ų �Լ�.
    /// </summary>
    /// <param name="parentPos">�̵� ��ų ��</param>
    /// <param name="churu">���� ������Ʈ</param>
    /// <param name="getChuruStack">������ ����</param>
    /// <param name="setChuruStack">���� ����</param>
    /// <param name="num">Ÿ���� �����ϴ� ��Ʈ</param>
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
            // �ӽ÷�  Quaternion.Euler(270, 0, 0)) �̷��� �ٲ���� ������ ���� �������� ����� ������  Quaternion.Euler(0, 0, 0)) ���� �ٲ����
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
            // �ӽ÷�  Quaternion.Euler(270, 0, 90)) �̷��� �ٲ���� ������ ���� �������� ����� ������  Quaternion.Euler(0, 0, 0)) ���� �ٲ����
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
