using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Store : MonoBehaviour
{
    [SerializeField, Tooltip("초당 생산량")] private int goldPerSecond = 1;
    [SerializeField, Tooltip("생산 주기")] private float timeInterval = 1f;
    private float timer = 0;
    private Player player;

    private void Start()
    {
        player = GameManager.Instance.P;
    }

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
        player.Gold += goldPerSecond;
    }
}
