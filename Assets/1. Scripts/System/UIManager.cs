using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;

[System.Serializable]
public class UpgradeInfo
{
    public System.Func<int> count;
    public System.Func<int> cost;
    public int maxCount; // 최대 업그레이드 횟수

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
    [TabGroup("Upgrade"), SerializeField] private Sprite[] UpgradeStepSprite;
    [TabGroup("Upgrade"), SerializeField] private Image[] UpgradeStepImage;
    [TabGroup("Upgrade"), SerializeField] private TMP_Text[] UpgradeCostText;

    [SerializeField] private Button breakDownButton;

    ////여기부터 윤제영에 테스트 참조임
    private IngredientMaker im;
    private ConveyorBelt cb;
    ////여기까지 윤제영에 테스트 참조였음

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
        breakDownButton.gameObject.SetActive(false);

        cameraZoomButton.onClick.AddListener(ZoomScreen);

        breakDownButton.onClick.AddListener(BreakDownEventButton);
        SetUpgradeInfo();
        UpdateUI();
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
            Employee employee = Instantiate(gm.employee[0], Vector3.zero, Quaternion.identity).GetComponent<Employee>();
        }
    }
   
    private void UpdateUI()
    {
        Debug.Log(baseCost);
        goldTxt.text = ChangeNumbet(p.Gold.ToString());
    }
    private void ZoomScreen()
    {
        gm.MainCamera.ZoomScreen();
        cameraZoomButton.image.sprite =
            cameraZoomButton.image.sprite == cameraZoomButtonImg[0] ? cameraZoomButtonImg[1] : cameraZoomButtonImg[0];
    }

    #region GoldUI
    // 재화 단위 변경
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
        //테스트용
        AddGold(900);
    }

    public void AddGold(int amount)
    {
        p.Gold += amount;
        UpdateUI();
        Debug.Log($"골드 획득: {amount}, 현재 골드: {p.Gold}");
    }

    //골드 사용 함수
    public bool SpendGold(int amount)
    {
        if (p.Gold >= amount)
        {
            p.Gold -= amount;
            UpdateUI();
            Debug.Log($"골드 사용: {amount}, 남은 골드: {p.Gold}");
            return true;
        }
        else
        {
            Debug.Log("골드가 부족합니다 !");
            return false;
        }
    }
    #endregion

    #region UpgradeUI
    // 업그레이드 데이터 항목 넣어주는 함수
    private void SetUpgradeInfo()
    {
        int maxCount = baseCost.baseUpgradeMaxCount;
        upgradeInfos = new List<UpgradeInfo>
        {
            new UpgradeInfo(() => baseCost.baseSpeedUpgradeCount, () => baseCost.baseSpeedUpgradeCost, maxCount ),
            new UpgradeInfo(() => baseCost.baseMaxObjStackCountUpgradeCount, () => baseCost.baseMaxObjStackCountUpgradeCost, maxCount),
            new UpgradeInfo(() => baseCost.baseGoldPerBoxUpgradeCount, () => baseCost.baseGoldPerBoxUpgradeCost, maxCount),
            new UpgradeInfo(() => baseCost.baseEmployeeSpeedUpgradeCount, () => baseCost.baseEmployeeSpeedUpgradeCost, maxCount),
            new UpgradeInfo(() => baseCost.baseEmployeeMaxObjStackCountUpgradeCount, () => baseCost.baseEmployeeMaxObjStackCountUpgradeCost, maxCount),
            new UpgradeInfo(() => baseCost.baseEmployeeAddCount, () => baseCost.baseEmployeeAddCost, maxCount)
        };
    }
    // 업그레이드 표시 항목 업데이트 함수
    private void UpgradeTextUpdate(int num)
    {
        if (upgradeInfos[num].count() == 5)
            UpgradeCostText[num].text = "Max";
        else
            UpgradeCostText[num].text = upgradeInfos[num].cost().ToString();

        UpgradeStepImage[num].sprite = UpgradeStepSprite[upgradeInfos[num].count()];
    }
    // 시작 시 업그레이드 항목 초기화 해주는 함수
    private void StartUpgradeTextUpdate()
    {
        for (int i = 0; i < UpgradeCostText.Length; i++)
        {
            UpgradeTextUpdate(i);
        }
    }
    // 오피스 강화 패널 여는 함수
    public void ShowUpgradeUI()
    {
        upgradePanel.SetActive(true);
        StartUpgradeTextUpdate();
    }
    // 오피스 강화 패널 닫는 함수
    public void CloseUpgradeUI()
    {
        upgradePanel.SetActive(false);
    }
    // 업그레이드 수치 올리기 위한 버튼에 연결한 함수
    public void Upgrade(int num)
    {
        int cost = 0;
        int maxCount = baseCost.baseUpgradeMaxCount;
        switch (num)
        {
            case 0:
                cost = baseCost.baseSpeedUpgradeCost;
                if (baseCost.baseSpeedUpgradeCount < maxCount)
                {
                    if (SpendGold(cost))
                    {
                        p.BaseSpeed += 1;
                        p.CartSpeed += 1;
                        baseCost.baseSpeedUpgradeCount++;
                        baseCost.baseSpeedUpgradeCost *= 2;
                        UpgradeTextUpdate(0);
                    }
                }
                else
                    Debug.Log("최대 업그레이드 입니다.");
                break;

            case 1:
                cost = baseCost.baseMaxObjStackCountUpgradeCost;
                if (baseCost.baseMaxObjStackCountUpgradeCount < maxCount)
                {
                    if (SpendGold(cost))
                    {
                        p.MaxObjStackCount += 1;
                        baseCost.baseMaxObjStackCountUpgradeCount++;
                        baseCost.baseMaxObjStackCountUpgradeCost *= 2;
                        UpgradeTextUpdate(1);
                    }
                }
                else
                    Debug.Log("최대 업그레이드 입니다.");
                break;

            case 2:
                cost = baseCost.baseGoldPerBoxUpgradeCost;
                if (baseCost.baseGoldPerBoxUpgradeCount < maxCount)
                {
                    if (SpendGold(cost))
                    {
                        p.GoldPerBox += 100;
                        baseCost.baseGoldPerBoxUpgradeCount++;
                        baseCost.baseGoldPerBoxUpgradeCost *= 2;
                        UpgradeTextUpdate(2);
                    }
                }
                else
                    Debug.Log("최대 업그레이드 입니다.");
                break;

            case 3:
                cost = baseCost.baseEmployeeSpeedUpgradeCost;
                if (baseCost.baseEmployeeSpeedUpgradeCount < maxCount)
                {
                    if (SpendGold(cost))
                    {
                        baseCost.employeeBaseSpeed += 0.5f;
                        baseCost.employeeBaseCartSpeed += 0.5f;
                        baseCost.baseEmployeeSpeedUpgradeCost *= 2;
                        baseCost.baseEmployeeSpeedUpgradeCount++;
                        UpgradeTextUpdate(3);
                    }
                }
                else
                    Debug.Log("최대 업그레이드 입니다.");
                break;

            case 4:
                cost = baseCost.baseEmployeeMaxObjStackCountUpgradeCost;
                if (baseCost.baseEmployeeMaxObjStackCountUpgradeCount < maxCount)
                {
                    if (SpendGold(cost))
                    {
                        baseCost.employeeBaseMaxObjStackCount += 1;
                        baseCost.baseEmployeeMaxObjStackCountUpgradeCost *= 2;
                        baseCost.baseEmployeeMaxObjStackCountUpgradeCount++;
                        UpgradeTextUpdate(4);
                    }
                }
                else
                    Debug.Log("최대 업그레이드 입니다.");
                break;

            case 5:
                cost = baseCost.baseEmployeeAddCost;
                if (baseCost.baseEmployeeAddCount < maxCount)
                {
                    if (SpendGold(cost))
                    {
                        int random = Random.Range(0, gm.employee.Count);
                        Employee employee;
                        if (baseCost.baseEmployeeAddCount == 4)
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
                        gm.employee.RemoveAt(random);
                        p.employee.Add(employee);


                        baseCost.baseEmployeeAddCost *= 2;
                        baseCost.baseEmployeeAddCount++;
                        UpgradeTextUpdate(5);
                    }
                }
                else
                    Debug.Log("종업원이 최대입니다.");
                break;

        }
    }
    #endregion

    #region BreakDownEventUI
    public void ShowBreakDownEventUI()
    {
        breakDownButton.gameObject.SetActive(true);
    }
    public void HideBreakDownEventUI()
    {
        breakDownButton.gameObject.SetActive(false);
    }
    private void BreakDownEventButton()
    {
        GameManager.Instance.P.PlayerAutoMove(cb.transform.GetChild(0), cb.BreakDownSolution);
    }
    public void SetConveyorBelt(ConveyorBelt cb)
    {
        this.cb = cb;
    }
    #endregion

    //여기부터 윤제영 테스트 함수임
//#if !UNITY_EDITOR
        private void OnGUI()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);

            // 폰트 사이즈 조정
            buttonStyle.fontSize = 25;

            if (GUI.Button(new Rect(430, 250, 200, 100), "돈", buttonStyle))
                AddGold(900);
            if (GUI.Button(new Rect(430, 360, 200, 100), "고장확률 높게", buttonStyle))
                cb.BreakDownProb += 0.01f;
            if (GUI.Button(new Rect(430, 470, 200, 100), "고장확률 낮게", buttonStyle))
                cb.BreakDownProb -= 0.01f;
            string def = $"고장확률:{cb.BreakDownProb * 100}%";
            def = GUI.TextArea(new Rect(430, 580, 200, 100), def, buttonStyle);
        }
        public void SetIngredientMaker(IngredientMaker im)
        {
            this.im = im;
        }
//#endif
    //여기까지 윤제영 테스트 함수였음

}
