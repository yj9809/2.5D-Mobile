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
        //Å×½ºÆ®¿ë
        AddGold(10);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateUI();
        Debug.Log($"°ñµå È¹µæ: {amount}, ÇöÀç °ñµå: {gold}");
    }

    //°ñµå »ç¿ë ÇÔ¼ö
    //public bool SpendGold(int amount)
    //{
    //    if (gold >= amount)
    //    {
    //        gold -= amount;
    //        UpdateUI();
    //        Debug.Log($"°ñµå »ç¿ë: {amount}, ³²Àº °ñµå: {gold}");
    //        return true;
    //    }
    //    else
    //    {
    //        Debug.Log("°ñµå°¡ ºÎÁ·ÇÕ´Ï´Ù !");
    //        return false;
    //    }
    //}
}
