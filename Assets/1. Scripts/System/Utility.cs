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
    public static float ObjRendererCheck(GameObject obj, string value)
    {
        Renderer ren = obj.GetComponent<Renderer>();

        if(value == "x")
            return ren.bounds.size.x;
        else
            return ren.bounds.size.y;

        //�ӽ÷� x ���� �޾ƾ��ϱ⿡ return ren.bounds.size.x �� ���� ���� ��Ű���� ����
        /*
        Renderer ren = obj.GetComponent<Renderer>();

        return ren.bounds.size.y;
         */
        //�� �ڵ�� �ٲ���� string value ���ڰ��� ������
    }
    // parentPos �̵� ��ų ��, churu ���� ������Ʈ(ó�� ��� ������ִ� �������� ����ϸ� �ɲ� ���Ƽ� �������� �� Null)
    // getChuruStack ������ ����(a���� b�� �ű� �� a�� ����), setChuruStack ���� ����(���������� b�� ����), num Ÿ�� ������ ���� ��Ʈ
    public static void ObjectDrop(Transform parentPos, GameObject churu, Stack<GameObject> getChuruStack, Stack<GameObject> setChuruStack, int num)
    {
        GameObject newChuru;

        if (num == (int)CheckType.Create)
        {
            newChuru = PoolingManager.Instance.GetObj(churu);
            newChuru.name = churu.name;
            newChuru.transform.SetParent(parentPos);
            newChuru.transform.localPosition = new Vector3(0, (ObjRendererCheck(newChuru, "y") * setChuruStack.Count), 0);
        }
        else
        {
            if (num == (int)CheckType.Drop)
            {
                newChuru = getChuruStack.Pop();
                newChuru.transform.DOLocalMove(new Vector3(0, 0 + (ObjRendererCheck(newChuru, "y") * setChuruStack.Count), 0), 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() => newChuru.transform.localRotation = Quaternion.Euler(0, 0, 0));
            }
            // �ӽ÷�  Quaternion.Euler(270, 0, 0)) �̷��� �ٲ���� ������ ���� �������� ����� ������  Quaternion.Euler(0, 0, 0)) ���� �ٲ����
            else if (num == (int) CheckType.Array)
            {
                newChuru = churu;
                string name = newChuru.name == "Churub_Stick_Defalt" ? "x" : "y";
                Debug.Log(ObjRendererCheck(newChuru, name) * (setChuruStack.Count % 10));
                newChuru.transform.DOLocalMove(new Vector3(0, 0 + (ObjRendererCheck(newChuru, name) * (setChuruStack.Count % 10)), 0), 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() => newChuru.transform.localRotation =  name == "x" ? Quaternion.Euler(270, 0, 0) : Quaternion.Euler(0,0,0));
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
                newChuru.transform.DOLocalMove(new Vector3(0, 0 + (ObjRendererCheck(newChuru, "x") * setChuruStack.Count), 0), 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() => newChuru.transform.localRotation = Quaternion.Euler(270, 0, 90));
            }
        }
        newChuru.transform.SetParent(parentPos);

        if(setChuruStack != null)
            setChuruStack.Push(newChuru);
    }
}
