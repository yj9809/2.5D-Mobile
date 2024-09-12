using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;

[System.Serializable]
public class UpgradeInfo
{
    public string description;
    public System.Func<string> valueGetter;
    public System.Func<string> count; // 현재 업그레이드 횟수
    public int maxCount; // 최대 업그레이드 횟수

    public UpgradeInfo(string description, System.Func<string> valueGetter, System.Func<string> count, int maxCount)
    {
        this.description = description;
        this.valueGetter = valueGetter;
        this.count = count;
        this.maxCount = maxCount;
    }
}

public class UIManager : Singleton<UIManager>
{
    [TabGroup("Camera Zoom"), SerializeField] private Button cameraZoomButton;
    [TabGroup("Camera Zoom"), SerializeField] private Sprite[] cameraZoomButtonImg;

    [SerializeField] private TextMeshProUGUI goldTxt;

    [TabGroup("Upgrade"), SerializeField] private GameObject upgradePanel;
    [TabGroup("Upgrade"), SerializeField] private Button speedUpgradeButton;
    [TabGroup("Upgrade"), SerializeField] private Button cartSpeedUpgradeButton;
    [TabGroup("Upgrade"), SerializeField] private Button maxObjStackCountUpgradeButton;
    [TabGroup("Upgrade"), SerializeField] private TMP_Text[] upgradeTxt;

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

        UpdateUI();
        cameraZoomButton.onClick.AddListener(ZoomScreen);

        breakDownButton.onClick.AddListener(BreakDownEventButton);
        SetUpgradeInfo();
        //UpgradeTxtUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SellItem();
        }
    }

    private void SetUpgradeInfo()
    {
        int maxCount = baseCost.baseUpgradeMaxCount;
        upgradeInfos = new List<UpgradeInfo>
    {
        new UpgradeInfo("속도", () => $"{p.BaseSpeed}/{p.CartSpeed}", () => $"{baseCost.baseSpeedUpgradeCount}", maxCount),
        new UpgradeInfo("운반 가능 갯수", () => $"{p.MaxObjStackCount}", () => $"{baseCost.baseMaxObjStackCountUpgradeCount}", maxCount),
        new UpgradeInfo("수익", () => $"박스 당 {p.GoldPerBox}", () => $"{baseCost.baseGoldPerBoxUpgradeCount}", maxCount),
        new UpgradeInfo("종업원 속도", () => $"{baseCost.employeeBaseSpeed}/{baseCost.employeeBaseCartSpeed}", () => $"{baseCost.baseEmployeeSpeedUpgradeCount}", maxCount),
        new UpgradeInfo("종업원 운반 가능 갯수", () => $"{baseCost.employeeBaseMaxObjStackCount}", () => $"{baseCost.baseEmployeeMaxObjStackCountUpgradeCount}", maxCount),
        new UpgradeInfo("종업원 수", () => $"{baseCost.baseEmployeeAddCount}", () => $"{baseCost.baseEmployeeAddCount}", maxCount)
    };

        for (int i = 0; i < upgradeInfos.Count; i++)
        {
            upgradeTxt[i].text = $"{upgradeInfos[i].count()}/{upgradeInfos[i].maxCount} \n {upgradeInfos[i].description} : {upgradeInfos[i].valueGetter()}";
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
    public void ShowUpgradeUI()
    {
        upgradePanel.SetActive(true);
    }
    public void CloseUpgradeUI()
    {
        upgradePanel.SetActive(false);
    }
    
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
                    if(SpendGold(cost))
                    {
                        p.BaseSpeed += 1;
                        p.CartSpeed += 1;
                        baseCost.baseSpeedUpgradeCount++;
                        baseCost.baseSpeedUpgradeCost *= 2;
                    }
                }
                else
                    Debug.Log("최대 업그레이드 입니다.");
                UpgradeTxtUpdate(0);
                break;

            case 1:
                cost = baseCost.baseMaxObjStackCountUpgradeCost;
                if(baseCost.baseMaxObjStackCountUpgradeCount < maxCount)
                {
                    if (SpendGold(cost))
                    {
                        p.MaxObjStackCount += 1;
                        baseCost.baseMaxObjStackCountUpgradeCount++;
                        baseCost.baseMaxObjStackCountUpgradeCost *= 2;
                    }
                }
                else
                    Debug.Log("최대 업그레이드 입니다.");
                UpgradeTxtUpdate(1);
                break;

            case 2:
                cost = baseCost.baseGoldPerBoxUpgradeCost;
                if(baseCost.baseGoldPerBoxUpgradeCount < maxCount)
                {
                    if(SpendGold(cost))
                    {
                        p.GoldPerBox += 10;
                        baseCost.baseGoldPerBoxUpgradeCount++;
                        baseCost.baseGoldPerBoxUpgradeCost *= 2;
                    }
                }
                else
                    Debug.Log("최대 업그레이드 입니다.");
                UpgradeTxtUpdate(2);
                break;

            case 3:
                cost = baseCost.baseEmployeeSpeedUpgradeCost;
                if(baseCost.baseEmployeeSpeedUpgradeCount < maxCount)
                {
                    if(SpendGold(cost))
                    {
                        baseCost.employeeBaseSpeed += 0.5f;
                        baseCost.employeeBaseCartSpeed += 0.5f;
                        baseCost.baseEmployeeSpeedUpgradeCost *= 2;
                        baseCost.baseEmployeeSpeedUpgradeCount++;
                    }
                }
                else
                    Debug.Log("최대 업그레이드 입니다.");
                UpgradeTxtUpdate(3);
                break;

            case 4:
                cost = baseCost.baseEmployeeMaxObjStackCountUpgradeCost;
                if(baseCost.baseEmployeeMaxObjStackCountUpgradeCount < maxCount)
                {
                    if(SpendGold(cost))
                    {
                        baseCost.employeeBaseMaxObjStackCount += 1;
                        baseCost.baseEmployeeMaxObjStackCountUpgradeCost *= 2;
                        baseCost.baseEmployeeMaxObjStackCountUpgradeCount++;
                    }
                }
                else
                    Debug.Log("최대 업그레이드 입니다.");
                UpgradeTxtUpdate(4);
                break;

            case 5:
                cost = baseCost.baseEmployeeAddCost;
                if (baseCost.baseEmployeeAddCount < maxCount)
                {
                    if (SpendGold(cost))
                    {
                        int random = Random.Range(0, gm.employee.Count);
                        Debug.Log(random);
                        Employee employee = Instantiate(gm.employee[random], Vector3.zero, Quaternion.identity).GetComponent<Employee>();
                        gm.employee.RemoveAt(random);
                        p.employee.Add(employee);
                        baseCost.baseEmployeeAddCost *= 2;
                        baseCost.baseEmployeeAddCount++;
                    }
                }
                else
                    Debug.Log("종업원이 최대입니다.");
                UpgradeTxtUpdate(5);
                break;

        }
    }
    private void UpgradeTxtUpdate(int num)
    {
        upgradeTxt[num].text = $"{upgradeInfos[num].count()}/{upgradeInfos[num].maxCount} \n {upgradeInfos[num].description} : {upgradeInfos[num].valueGetter()}";
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
//    private void OnGUI()
//    {
//        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);

//        // 폰트 사이즈 조정
//        buttonStyle.fontSize = 25;

//        if (GUI.Button(new Rect(10, 250, 200, 100), "소지갯수 올리기", buttonStyle))
//            p.MaxObjStackCount += 1;
//        if (GUI.Button(new Rect(10, 360, 200, 100), "소지갯수 내리기", buttonStyle))
//            p.MaxObjStackCount -= 1;
//        if (GUI.Button(new Rect(10, 470, 200, 100), "이동속도 올리기", buttonStyle))
//            p.BaseSpeed += 1;
//        if (GUI.Button(new Rect(10, 580, 200, 100), "이동속도 내리기", buttonStyle))
//            p.BaseSpeed -= 1;

//        string textAreaString = $"소지갯수:{p.MaxObjStackCount}\n이동속도:{p.BaseSpeed}";
//        textAreaString = GUI.TextArea(new Rect(10, 690, 200, 100), textAreaString, buttonStyle);

//        if (GUI.Button(new Rect(220, 250, 200, 100), "재료생산 느리게", buttonStyle))
//            im.ObjSpawnTime += 1;
//        if (GUI.Button(new Rect(220, 360, 200, 100), "재료생산 빠르게", buttonStyle))
//            im.ObjSpawnTime -= 1;
//        if (GUI.Button(new Rect(220, 470, 200, 100), "재료변환 느리게", buttonStyle))
//            cb.PlaceObjectTime += 1;
//        if (GUI.Button(new Rect(220, 580, 200, 100), "재료변환 빠르게", buttonStyle))
//            cb.PlaceObjectTime -= 1;
//        string abc = $"재료생산:{im.ObjSpawnTime}초\n재료변환:{cb.PlaceObjectTime}초";
//        abc = GUI.TextArea(new Rect(220, 690, 200, 100), abc, buttonStyle);

//        if (GUI.Button(new Rect(430, 250, 200, 100), "돈", buttonStyle))
//            AddGold(900);
//        if (GUI.Button(new Rect(430, 360, 200, 100), "고장확률 높게", buttonStyle))
//            cb.BreakDownProb += 0.1f;
//        if (GUI.Button(new Rect(430, 470, 200, 100), "고장확률 낮게", buttonStyle))
//            cb.BreakDownProb -= 0.1f;
//        string def = $"고장확률:{cb.BreakDownProb * 100}%";
//        def = GUI.TextArea(new Rect(430, 580, 200, 100), def, buttonStyle);
//    }
//    public void SetIngredientMaker(IngredientMaker im)
//    {
//        this.im = im;
//    }
//#endif
    //여기까지 윤제영 테스트 함수였음
    
}
