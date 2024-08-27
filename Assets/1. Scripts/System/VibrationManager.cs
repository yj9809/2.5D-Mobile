using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    private void Start()
    {
        Vibration.Init();
    }
    public void OnClick1()
    {
        Vibration.VibratePop();
    }
}
