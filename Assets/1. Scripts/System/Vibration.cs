using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration : MonoBehaviour
{
    // �ڵ� ���� TriggerVibration(); ȣ��
    public void TriggerVibration()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        VibrateForDuration(500); // 500ms = 0.5��
#endif
    }

    // ���� �ð��� �����ϴ� �Լ�
    private void VibrateForDuration(long milliseconds)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
        vibrator.Call("vibrate", milliseconds);
    }
}
