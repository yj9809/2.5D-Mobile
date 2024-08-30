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
    private IngredientMaker im;
    private ConveyorBelt cb;
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

    // ��ȭ ���� ����
    private string ChangeNumbet(string number)
    {
        char[] unitAlphabet = new char[3] { 'K', 'M', 'B' };
        int unit = 0;
        while (number.Length > 6)
        {
            unit++;
            number = number.Substring(0, number.Length - 3);
        }
        if (number.Length > 3)
        {
            int newInt = int.Parse(number);
            if (number.Length > 4)
            {
                return (newInt / 1000).ToString() + unitAlphabet[unit];
            }
            else
            {
                return (newInt / 1000f).ToString("0.0") + unitAlphabet[unit];
            }
        }
        else
        {
            int newInt = int.Parse(number);
            return (newInt).ToString();
        }
    }

    private void UpdateUI()
    {
        goldTxt.text = ChangeNumbet(p.Gold.ToString());
    }

    public void SellItem()
    {
        //�׽�Ʈ��
        AddGold(900);
    }

    public void AddGold(int amount)
    {
        p.Gold += amount;
        UpdateUI();
        Debug.Log($"��� ȹ��: {amount}, ���� ���: {p.Gold}");
    }

    //��� ��� �Լ�
    public bool SpendGold(int amount)
    {
        if (p.Gold >= amount)
        {
            p.Gold -= amount;
            UpdateUI();
            Debug.Log($"��� ���: {amount}, ���� ���: {p.Gold}");
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
            p.BaseSpeed += 1;
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
            p.CartSpeed += 1;
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
            p.MaxObjStackCount += 1;
            data.baseCost.baseMaxObjStackCountUpgradeCost *= 2;
        }
        else
        {
            Debug.Log("��尡 �����Ͽ� ���׷��̵带 ������ �� �����ϴ�.");
        }
    }

    //������� ������ �׽�Ʈ �Լ���
    /*
    private void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);

        // ��Ʈ ������ ����
        buttonStyle.fontSize = 50;

        if (GUI.Button(new Rect(50, 250, 300, 100), "PMS Up", buttonStyle))
            p.MaxObjStackCount += 1;
        if (GUI.Button(new Rect(50, 360, 300, 100), "PMS Down", buttonStyle))
            p.MaxObjStackCount -= 1;
        if (GUI.Button(new Rect(50, 470, 300, 100), "PS Up", buttonStyle))
            p.BaseSpeed += 1;
        if (GUI.Button(new Rect(50, 580, 300, 100), "PS Down", buttonStyle))
            p.BaseSpeed -= 1;

        string textAreaString = $"PMS:{p.MaxObjStackCount} PS:{p.BaseSpeed}";
        textAreaString = GUI.TextArea(new Rect(50, 690, 300, 100), textAreaString, buttonStyle);

        if (GUI.Button(new Rect(360, 250, 300, 100), "IM Up", buttonStyle))
            im.ObjSpawnTime += 1;
        if (GUI.Button(new Rect(360, 360, 300, 100), "IM Down", buttonStyle))
            im.ObjSpawnTime -= 1;
        if (GUI.Button(new Rect(360, 470, 300, 100), "CB Up", buttonStyle))
            cb.PlaceObjectTime += 1;
        if (GUI.Button(new Rect(360, 580, 300, 100), "CB Down", buttonStyle))
            cb.PlaceObjectTime -= 1;
        string abc = $"IM:{im.ObjSpawnTime} CB:{cb.PlaceObjectTime}";
        abc = GUI.TextArea(new Rect(350, 690, 300, 100), abc, buttonStyle);

        if (GUI.Button(new Rect(670, 250, 300, 100), "Gold", buttonStyle))
            AddGold(100);
        if (GUI.Button(new Rect(670, 360, 300, 100), "BDP Up", buttonStyle))
            cb.BreakDownProb += 0.1f;
        if (GUI.Button(new Rect(670, 470, 300, 100), "BDP Down", buttonStyle))
            cb.BreakDownProb -= 0.1f;
        string def = $"BDP:{cb.BreakDownProb * 100}%";
        def = GUI.TextArea(new Rect(670, 580, 300, 100), def, buttonStyle);
    }
    public void SetIngredientMaker(IngredientMaker im)
    {
        this.im = im;
    }
    public void SetConveyorBelt(ConveyorBelt cb)
    {
        this.cb = cb;
    }
    */
    //������� ������ �׽�Ʈ �Լ�����
}
