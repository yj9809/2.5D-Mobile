using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class Store : MonoBehaviour
{
    [SerializeField] private GameObject stall;
    [SerializeField] private GameObject store;
    [SerializeField] private GameObject commonGameObjects;
    [SerializeField] private TextMeshProUGUI goldTxt;

    private float goldPerSecond; // �ʴ� ���귮
    private float timeInterval = 1f; // ���� �ֱ�
    private float timer = 0;
    [HideInInspector]public float totalGold = 0f;

    void Update()
    {
        if (IsObjectActive())
        {
            timer += Time.deltaTime;
            if (timer >= timeInterval)
            {
                GetGold();
                timer = 0f;
            }
        }
    }

    private bool IsObjectActive()
    {
        bool isActive = false;

        if (stall.activeSelf)
        {
            isActive = true;
            goldPerSecond = 5f;
            store.SetActive(false);
        }
        else if (store.activeSelf)
        {
            isActive = true;
            goldPerSecond = 10f;
            stall.SetActive(false);
            UIManager.Instance.storeUpgradeButton.gameObject.SetActive(false);
        }
        
        if (!isActive)
        {
            stall.SetActive(false);
            store.SetActive(false);
        }

        commonGameObjects.SetActive(isActive);
        return isActive;
    }

    private void GetGold()
    {
        totalGold += goldPerSecond;
        UIManager.Instance.StoreUI();
        UIManager.Instance.StoreUI(goldTxt);
    }
}
