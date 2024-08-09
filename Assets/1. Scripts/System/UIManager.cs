using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI goldTxt;

    private int gold = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SellItem();
        }
    }

    private void UpdateUI()
    {
        goldTxt.text = "Gold: " + gold.ToString();
    }

    public void SellItem()
    {
        //�׽�Ʈ��
        AddGold(10);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateUI();
        Debug.Log($"��� ȹ��: {amount}, ���� ���: {gold}");
    }

    //��� ��� �Լ�
    //public bool SpendGold(int amount)
    //{
    //    if (gold >= amount)
    //    {
    //        gold -= amount;
    //        UpdateUI();
    //        Debug.Log($"��� ���: {amount}, ���� ���: {gold}");
    //        return true;
    //    }
    //    else
    //    {
    //        Debug.Log("��尡 �����մϴ� !");
    //        return false;
    //    }
    //}
}
