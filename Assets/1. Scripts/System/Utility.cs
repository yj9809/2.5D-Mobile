using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using DG.Tweening;

// ����� ��� Ÿ�� ������ ������ �������ٷ��� ���� enum��
// create�� ��� ������ ��, Drop�� ���� ������ ��, Array�� �ڽ� ���丮���� �ű� ��
// Car�� Ʈ���� �ڽ� �ű� ��, Box�� �� �״�� �ڽ��� �÷��̾� īƮ�� �ű� �� 
// ��� �κ� ������Ʈ �������� �̻��ϴ� ������ ���� Ȯ���ؼ� �ؿ� ������ ó���ϴ� �κп� �ش� enum ��ȣ ã�Ƽ� Ȯ���ϸ� ��
public enum CheckType
{
    Create,
    Drop,
    Array,
    Car,
    Box
}
#region �����̾��� ģ��...
public static class Vibrations
{
#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass AndroidPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject AndroidcurrentActivity = AndroidPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject AndroidVibrator = AndroidcurrentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#endif
    public static void Vibrate()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidVibrator.Call("vibrate");
#endif
    }

    public static void Vibrate(long milliseconds)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
         AndroidVibrator.Call("vibrate", milliseconds);
#endif
    }

    public static void Cancel()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidVibrator.Call("cancel");
#endif
    }

}
#endregion
public static class Utility
{
    public static float ObjRendererCheck(GameObject obj)
    {
        BoxCollider ren = obj.GetComponent<BoxCollider>();

        return ren.size.y;
    }
    // parentPos �̵� ��ų ��, churu ���� ������Ʈ(ó�� ��� ������ִ� �������� ����ϸ� �ɲ� ���Ƽ� �������� �� Null)
    // getChuruStack ������ ����(a���� b�� �ű� �� a�� ����), setChuruStack ���� ����(���������� b�� ����), num Ÿ�� ������ ���� ��Ʈ
    // �� ���� �̻��ϰ� ũ�Գ� ���� ���� ���� ������ �̻��ϰ� ������ �� ��κ� ���⼭ Ȯ���ϸ� ��
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
            newChuru = churu;
            newChuru.name = churu.name;
            newChuru.transform.SetParent(parentPos);
            newChuru.transform.DOLocalMove(new Vector3(0, 0 + (ObjRendererCheck(newChuru) * setChuruStack.Count), 0), 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() => 
                {
                    newChuru.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    newChuru.transform.localScale = Vector3.one;
                } );
        }
        else
        {
            if (num == (int)CheckType.Drop)
            {
                newChuru = getChuruStack.Pop();
                newChuru.transform.DOLocalMove(new Vector3(0, 0 + (ObjRendererCheck(newChuru) * setChuruStack.Count), 0), 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    newChuru.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    newChuru.transform.localScale = Vector3.one;
                });
            }
            else if (num == (int) CheckType.Array)
            {
                newChuru = churu;
                newChuru.transform.DOLocalMove(new Vector3(0, 0 + (ObjRendererCheck(newChuru) * (setChuruStack.Count % 10)), 0), 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    newChuru.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    newChuru.transform.localScale = Vector3.one;
                });
            }
            else if(num == (int) CheckType.Car)
            {
                newChuru = getChuruStack.Pop();
                newChuru.transform.DOMove(parentPos.position, 0.2f).SetEase(Ease.InBack);
            }
            else
            {
                newChuru = getChuruStack.Pop();
                newChuru.transform.DOLocalMove(new Vector3(0, 0 + (ObjRendererCheck(newChuru) * setChuruStack.Count), 0), 0.2f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    newChuru.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    newChuru.transform.localScale = Vector3.one;
                });
            }
        }
        newChuru.transform.SetParent(parentPos);

        if(setChuruStack != null)
            setChuruStack.Push(newChuru);
    }
}
