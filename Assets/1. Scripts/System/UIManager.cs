using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldTxt;

    private int gold = 0;
    private int itemPrice = 10;

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
        gold += itemPrice;
        UpdateUI();
    }
}
