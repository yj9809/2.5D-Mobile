using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public void OnClick1()
    {
        Vibration.Vibrate((long)5000);
    }
}
