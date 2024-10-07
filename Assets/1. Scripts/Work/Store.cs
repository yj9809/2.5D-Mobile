using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class Store : MonoBehaviour
{
    [SerializeField, Tooltip("초당 생산량")] private float goldPerSecond = 100f;
    [SerializeField, Tooltip("생산 주기")] private float timeInterval = 1f;
    private float timer = 0;
    public float totalGold = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeInterval)
        {
            GetGold();
            timer = 0f;
        }
    }

    private void GetGold()
    {
        totalGold += goldPerSecond;
    }
}
