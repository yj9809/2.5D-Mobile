using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI goldTxt;

    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private Button speedUpgradeButton;
    [SerializeField] private Button cartSpeedUpgradeButton;
    [SerializeField] private Button maxObjStackCountUpgradeButton;

    private Player p;
    private DataManager data;

    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.P;
        data = DataManager.Instance;

        upgradePanel.SetActive(false);

        UpdateUI();
        speedUpgradeButton.onClick.AddListener(UpgradePlayerSpeed);
        cartSpeedUpgradeButton.onClick.AddListener(UpgradePlayerCartSpeed);
        maxObjStackCountUpgradeButton.onClick.AddListener(UpgradePlayerMaxStack);
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
        goldTxt.text = "Gold: " + p.gold.ToString();
    }

    public void SellItem()
    {
        //테스트용
        AddGold(10);
    }

    public void AddGold(int amount)
    {
        p.gold += amount;
        UpdateUI();
        Debug.Log($"골드 획득: {amount}, 현재 골드: {p.gold}");
    }

    //골드 사용 함수
    public bool SpendGold(int amount)
    {
        if (p.gold >= amount)
        {
            p.gold -= amount;
            UpdateUI();
            Debug.Log($"골드 사용: {amount}, 남은 골드: {p.gold}");
            return true;
        }
        else
        {
            Debug.Log("골드가 부족합니다 !");
            return false;
        }
    }

    public void ShowUpgradeUI()
    {
        upgradePanel.SetActive(true);
    }
    public void CloseUpgradeUI()
    {
        upgradePanel.SetActive(false);
    }
    
    public void UpgradePlayerSpeed()
    {
        int cost = data.baseCost.baseSpeedUpgradeCost;
        if (SpendGold(cost))
        {
            p.baseSpeed += 1;
            data.baseCost.baseSpeedUpgradeCost *= 2;
        }
        else
        {
            Debug.Log("골드가 부족하여 업그레이드를 진행할 수 없습니다.");
        }
    }
    public void UpgradePlayerCartSpeed()
    {
        int cost = data.baseCost.baseCartSpeedUpgradeCost;
        if (SpendGold(cost))
        {
            p.cartSpeed += 1;
            data.baseCost.baseCartSpeedUpgradeCost *= 2;
        }
        else
        {
            Debug.Log("골드가 부족하여 업그레이드를 진행할 수 없습니다.");
        }
    }
    public void UpgradePlayerMaxStack()
    {
        int cost = data.baseCost.baseMaxObjStackCountUpgradeCost;
        if (SpendGold(cost))
        {
            p.maxObjStackCount += 1;
            data.baseCost.baseMaxObjStackCountUpgradeCost *= 2;
        }
        else
        {
            Debug.Log("골드가 부족하여 업그레이드를 진행할 수 없습니다.");
        }
    }
}
