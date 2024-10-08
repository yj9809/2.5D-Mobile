using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;
using BackEnd;

[System.Serializable]
public class UpgradeInfo
{
    public System.Func<int> count;
    public System.Func<int> cost;
    public int maxCount; // �ִ� ���׷��̵� Ƚ��

    public UpgradeInfo(System.Func<int> count, System.Func<int> cost, int maxCount)
    {
        this.count = count;
        this.cost = cost;
        this.maxCount = maxCount;
    }
}

public class UIManager : Singleton<UIManager>
{
    [TabGroup("Camera Zoom"), SerializeField] private Button cameraZoomButton;
    [TabGroup("Camera Zoom"), SerializeField] private Sprite[] cameraZoomButtonImg;

    [SerializeField] private TextMeshProUGUI goldTxt;

    [TabGroup("Upgrade"), SerializeField] private GameObject upgradePanel;
    [TabGroup("Upgrade"), SerializeField] private Sprite[] upgradeStepSprite;
    [TabGroup("Upgrade"), SerializeField] private Image[] upgradeStepImage;
    [TabGroup("Upgrade"), SerializeField] private TMP_Text[] upgradeCostText;

    [TabGroup("Store"), SerializeField] private Store store;
    [TabGroup("Store"), SerializeField] private GameObject storePanel;
    [TabGroup("Store"), SerializeField] private TextMeshProUGUI storeGoldTxt;
    [TabGroup("Store"), SerializeField] private Button storeGetGoldButton;
    [TabGroup("Store")] public Button storeUpgradeButton;

    [SerializeField] private Button breakDownButton;

    ////������� �������� �׽�Ʈ ������
    private IngredientMaker im;
    private ConveyorBelt cb;
    private Guide guide;
    ////������� �������� �׽�Ʈ ��������

    private Player p;
    private BaseCost baseCost;
    private GameManager gm;

    private List<UpgradeInfo> upgradeInfos;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.P;
        baseCost = DataManager.Instance.baseCost;
        gm = GameManager.Instance;
        upgradePanel.SetActive(false);
        storePanel.SetActive(false);
        breakDownButton.gameObject.SetActive(false);

        cameraZoomButton.onClick.AddListener(ZoomScreen);
        storeGetGoldButton.onClick.AddListener(GetGold);

        SetUpgradeInfo();
        UpdateUI();
        StoreUI();
        //UpgradeTxtUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SellItem();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Upgrade(5);
        }
    }
   
    private void UpdateUI()
    {
        goldTxt.text = ChangeNumbet(p.Gold.ToString());
    }

    private void ZoomScreen()
    {
        gm.MainCamera.ZoomScreen();
        cameraZoomButton.image.sprite =
            cameraZoomButton.image.sprite == cameraZoomButtonImg[0] ? cameraZoomButtonImg[1] : cameraZoomButtonImg[0];
    }

    #region GoldUI
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
    #endregion

    #region UpgradeUI
    // ���׷��̵� ������ �׸� �־��ִ� �Լ�
    private void SetUpgradeInfo()
    {
        int maxCount = baseCost.upgradeCosts["baseUpgradeMaxCount"] ;
        upgradeInfos = new List<UpgradeInfo>
        {
            new UpgradeInfo(() => baseCost.upgradeCosts["baseSpeedUpgradeCount"] , () => baseCost.upgradeCosts["baseSpeedUpgradeCost"] , maxCount ),
            new UpgradeInfo(() => baseCost.upgradeCosts["baseMaxObjStackCountUpgradeCount"] , () => baseCost.upgradeCosts["baseMaxObjStackCountUpgradeCost"] , maxCount),
            new UpgradeInfo(() => baseCost.upgradeCosts["baseGoldPerBoxUpgradeCount"] , () => baseCost.upgradeCosts["baseGoldPerBoxUpgradeCost"] , maxCount),
            new UpgradeInfo(() => baseCost.upgradeCosts["baseEmployeeSpeedUpgradeCount"] , () => baseCost.upgradeCosts["baseEmployeeSpeedUpgradeCost"] , maxCount),
            new UpgradeInfo(() => baseCost.upgradeCosts["baseEmployeeMaxObjStackCountUpgradeCount"] , () => baseCost.upgradeCosts["baseEmployeeMaxObjStackCountUpgradeCost"] , maxCount),
            new UpgradeInfo(() => baseCost.upgradeCosts["baseEmployeeAddCount"] , () => baseCost.upgradeCosts["baseEmployeeAddCost"] , maxCount)
        };
    }
    // ���׷��̵� ǥ�� �׸� ������Ʈ �Լ�
    private void UpgradeTextUpdate(int num)
    {
        if (upgradeInfos[num].count() == 5)
            upgradeCostText[num].text = "Max";
        else
            upgradeCostText[num].text = upgradeInfos[num].cost().ToString();

        upgradeStepImage[num].sprite = upgradeStepSprite[upgradeInfos[num].count()];
    }
    // ���� �� ���׷��̵� �׸� �ʱ�ȭ ���ִ� �Լ�
    private void StartUpgradeTextUpdate()
    {
        for (int i = 0; i < upgradeCostText.Length; i++)
        {
            UpgradeTextUpdate(i);
        }
    }
    // ���ǽ� ��ȭ �г� ���� �Լ�
    public void ShowUpgradeUI()
    {
        upgradePanel.SetActive(true);
        StartUpgradeTextUpdate();
    }
    // ���ǽ� ��ȭ �г� �ݴ� �Լ�
    public void CloseUpgradeUI()
    {
        upgradePanel.SetActive(false);
    }
    // ���׷��̵� ��ġ �ø��� ���� ��ư�� ������ �Լ�
    public void Upgrade(int num)
    {
        int cost = 0;
        int maxCount = baseCost.upgradeCosts["baseUpgradeMaxCount"] ;
        switch (num)
        {
            case 0:
                cost = baseCost.upgradeCosts["baseSpeedUpgradeCost"] ;
                if (baseCost.upgradeCosts["baseSpeedUpgradeCount"]  < maxCount)
                {
                    if (SpendGold(cost))
                    {
                        p.BaseSpeed += 1;
                        p.CartSpeed += 1;
                        baseCost.upgradeCosts["baseSpeedUpgradeCount"] ++;
                        baseCost.upgradeCosts["baseSpeedUpgradeCost"]  *= 2;
                        UpgradeTextUpdate(0);
                    }
                }
                else
                    Debug.Log("�ִ� ���׷��̵� �Դϴ�.");
                break;

            case 1:
                cost = baseCost.upgradeCosts["baseMaxObjStackCountUpgradeCost"] ;
                if (baseCost.upgradeCosts["baseMaxObjStackCountUpgradeCount"]  < maxCount)
                {
                    if (SpendGold(cost))
                    {
                        p.MaxObjStackCount += 1;
                        baseCost.upgradeCosts["baseMaxObjStackCountUpgradeCount"] ++;
                        baseCost.upgradeCosts["baseMaxObjStackCountUpgradeCost"]  *= 2;
                        UpgradeTextUpdate(1);
                    }
                }
                else
                    Debug.Log("�ִ� ���׷��̵� �Դϴ�.");
                break;

            case 2:
                cost = baseCost.upgradeCosts["baseGoldPerBoxUpgradeCost"] ;
                if (baseCost.upgradeCosts["baseGoldPerBoxUpgradeCount"]  < maxCount)
                {
                    if (SpendGold(cost))
                    {
                        p.GoldPerBox += 100;
                        baseCost.upgradeCosts["baseGoldPerBoxUpgradeCount"] ++;
                        baseCost.upgradeCosts["baseGoldPerBoxUpgradeCost"]  *= 2;
                        UpgradeTextUpdate(2);
                    }
                }
                else
                    Debug.Log("�ִ� ���׷��̵� �Դϴ�.");
                break;

            case 3:
                cost = baseCost.upgradeCosts["baseEmployeeSpeedUpgradeCost"] ;
                if (baseCost.upgradeCosts["baseEmployeeSpeedUpgradeCount"]  < maxCount)
                {
                    if (SpendGold(cost))
                    {
                        baseCost.employeeData["employeeSpeed"]  += 0.5f;
                        baseCost.employeeData["employeeCartSpeed"]  += 0.5f;
                        baseCost.upgradeCosts["baseEmployeeSpeedUpgradeCost"]  *= 2;
                        baseCost.upgradeCosts["baseEmployeeSpeedUpgradeCount"] ++;
                        UpgradeTextUpdate(3);
                    }
                }
                else
                    Debug.Log("�ִ� ���׷��̵� �Դϴ�.");
                break;

            case 4:
                cost = baseCost.upgradeCosts["baseEmployeeMaxObjStackCountUpgradeCost"] ;
                if (baseCost.upgradeCosts["baseEmployeeMaxObjStackCountUpgradeCount"]  < maxCount)
                {
                    if (SpendGold(cost))
                    {
                        baseCost.employeeData["employeeMaxObjStackCount"] += 1;
                        baseCost.upgradeCosts["baseEmployeeMaxObjStackCountUpgradeCost"]  *= 2;
                        baseCost.upgradeCosts["baseEmployeeMaxObjStackCountUpgradeCount"] ++;
                        UpgradeTextUpdate(4);
                    }
                }
                else
                    Debug.Log("�ִ� ���׷��̵� �Դϴ�.");
                break;

            case 5:
                cost = baseCost.upgradeCosts["baseEmployeeAddCost"];
                if (baseCost.upgradeCosts["baseEmployeeAddCount"] < maxCount)
                {
                    if (SpendGold(cost))
                    {
                        int random = Random.Range(0, gm.employee.Count);
                        Employee employee;
                        if (baseCost.upgradeCosts["baseEmployeeAddCount"] == 4)
                        {
                            Vector3 pos = FindObjectOfType<BoxPackaging>().transform.GetChild(1).transform.position;
                            employee = Instantiate(gm.employee[random]).GetComponent<Employee>();
                            Destroy(employee.GetComponent<NavMeshAgent>());
                            employee.transform.position = pos;
                            employee.PackaingEmployee();
                        }
                        else
                        {
                            employee = Instantiate(gm.employee[random], Vector3.zero, Quaternion.identity).GetComponent<Employee>();
                        }
                        employee.name = gm.employee[random].name;
                        gm.employee.RemoveAt(random);
                        p.employee.Add(employee);


                        baseCost.upgradeCosts["baseEmployeeAddCost"] *= 2;
                        baseCost.upgradeCosts["baseEmployeeAddCount"]++;
                        UpgradeTextUpdate(5);
                    }
                }
                else
                    Debug.Log("�������� �ִ��Դϴ�.");
                break;

        }
    }
    #endregion

    #region StoreUI
    public void ShowStoreUI()
    {
        storePanel.SetActive(true);
    }
    public void CloseStoreUI()
    {
        storePanel.SetActive(false);
    }
    public void StoreUI()
    {
        storeGoldTxt.text = ChangeNumbet(store.totalGold.ToString());
    }
    public void StoreUI(TextMeshProUGUI text)
    {
        text.text = ChangeNumbet(store.totalGold.ToString());
    }

    private void GetGold()
    {
        p.Gold += store.totalGold;
        UpdateUI();
        Debug.Log($"��� ȹ��: {store.totalGold}, ���� ���: {p.Gold}");

        store.totalGold = 0;
        StoreUI();
    }
    #endregion

    #region GameTest UI

    private void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);

        // ��Ʈ ������ ����
        buttonStyle.fontSize = 25;

        if (GUI.Button(new Rect(200, 250, 200, 100), "���̵� �ѱ��", buttonStyle))
            guide.ToNextStep();
        if (GUI.Button(new Rect(430, 250, 200, 100), "��", buttonStyle))
            AddGold(900);

        if (GUI.Button(new Rect(650, 250, 200, 100), "���̺�", buttonStyle))
            DataManager.Instance.GameDataUpdate();
        if (GUI.Button(new Rect(650, 360, 200, 100), "������ Ŭ����", buttonStyle))
        {
            Backend.BMember.DeleteGuestInfo();
        }
            
    }
    public void SetIngredientMaker(IngredientMaker im)
    {
        this.im = im;
    }
    public void SetGuideStep(Guide guide)
    {
        this.guide = guide;
    }
    #endregion
}
