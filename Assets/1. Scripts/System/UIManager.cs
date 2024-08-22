using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Button cameraZoomButton;

    [SerializeField] private TextMeshProUGUI goldTxt;

    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private Button speedUpgradeButton;
    [SerializeField] private Button cartSpeedUpgradeButton;
    [SerializeField] private Button maxObjStackCountUpgradeButton;

    ////������� �������� �׽�Ʈ ������
    //[SerializeField] private Sprite[] onOffSprite;
    //[SerializeField] private Image onOffImage;
    //[SerializeField] private Slider testSilder;
    //[SerializeField] private Sprite[] buttonSprite;
    //[SerializeField] private Image buttonImage;
    ////������� �������� �׽�Ʈ ��������

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
        cameraZoomButton.onClick.AddListener(GameManager.Instance.MainCamera.ZoomScreen);
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
        //�׽�Ʈ��
        AddGold(10);
    }

    public void AddGold(int amount)
    {
        p.gold += amount;
        UpdateUI();
        Debug.Log($"��� ȹ��: {amount}, ���� ���: {p.gold}");
    }

    //��� ��� �Լ�
    public bool SpendGold(int amount)
    {
        if (p.gold >= amount)
        {
            p.gold -= amount;
            UpdateUI();
            Debug.Log($"��� ���: {amount}, ���� ���: {p.gold}");
            return true;
        }
        else
        {
            Debug.Log("��尡 �����մϴ� !");
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
            Debug.Log("��尡 �����Ͽ� ���׷��̵带 ������ �� �����ϴ�.");
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
            Debug.Log("��尡 �����Ͽ� ���׷��̵带 ������ �� �����ϴ�.");
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
            Debug.Log("��尡 �����Ͽ� ���׷��̵带 ������ �� �����ϴ�.");
        }
    }

    ////������� ������ �׽�Ʈ �Լ���
    //public void ButtonEvent()
    //{
    //    testSilder.value = testSilder.value == 1 ? 0 : 1;
    //    onOffImage.sprite = testSilder.value == 1 ? onOffSprite[1] : onOffSprite[0];
    //    buttonImage.sprite = testSilder.value == 1 ? buttonSprite[1] : buttonSprite[0];
    //}
    ////������� ������ �׽�Ʈ �Լ�����
}
