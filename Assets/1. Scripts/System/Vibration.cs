using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration : MonoBehaviour
{
    // 코드 사용시 TriggerVibration(); 호출
    public void TriggerVibration()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        VibrateForDuration(500); // 500ms = 0.5초
#endif
    }

    // 진동 시간을 설정하는 함수
    private void VibrateForDuration(long milliseconds)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
        vibrator.Call("vibrate", milliseconds);
    }
}
