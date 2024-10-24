using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;
using BackEnd;

// 이것도 권오석 작품 맞을껄?
// 이거 보면 양심적으로 주석처리 해주겠지 싶음
// Ui에 문제 생겼다 하면 바로 권오석한테 문의

// 근데 지금 자세히 보니까 이 밑에 있는건 내꺼 같음
// 그래서 주석 처리 남기겠음
// 이 부부에서 UpgradeInfo 클래스는 그냥 단순하게 텍스트 배열로 받아서 처리하려고 했는데
// 새로운 방법에 도전해보고 싶어 만들었던 클래스임
// system.func<자료형> 이렇게 선언해둔 변수는 액션이랑 살짝 비슷하게 활용할 수 있는거 같음
// 값이 바뀌면 알아서 업데이트 된달까? 그런 느낌으로 활용한거임 
// 그래서 만약 값 추가하고 싶고 업데이트도 필요하다 싶으면 똑같이 처리해서 써보셈
[System.Serializable]
public class UpgradeInfo
{
    public System.Func<int> count;
    public System.Func<int> cost;
    public int maxCount; // 최대 업그레이드 횟수

    /// <summary>
    /// 이게 텍스트 업데이트 해주는 부분
    /// </summary>
    /// <param name="count"> 업그레이드 한 횟수</param>
    /// <param name="cost"> 업그레이드 가격 2배씩 올라가게 설정해뒀을껄? </param>
    /// <param name="maxCount"> 최대 가능횟수 (솔직히 전부 5로 통일되어 있어서 필요한가 싶어) </param>
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

    [Title("Debug"), SerializeField] private TextMeshProUGUI logText;

    ////여기부터 윤제영에 테스트 참조임
    private IngredientMaker im;
    private ConveyorBelt cb;
    private Guide guide;
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
        storePanel.SetActive(false);

        cameraZoomButton.onClick.AddListener(ZoomScreen);
        storeGetGoldButton.onClick.AddListener(GetGold);

        SetUpgradeInfo();
        UpdateUI();
        StoreUI();
    }

    private void LogMessage(string message)
    {
        logText.text = "";
        if (logText != null)
        {
            logText.text += message + "\n";
            StartCoroutine(MessageClear(3f));
        }
    }

    private IEnumerator MessageClear(float delay)
    {
        yield return new WaitForSeconds(delay);
        logText.text = "";
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

    public void DeleteData()
    {
        DataManager.Instance.DeleteData();
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
    }

    //골드 사용 함수
    public bool SpendGold(int amount)
    {
        if (p.Gold >= amount)
        {
            p.Gold -= amount;
            UpdateUI();
            AudioManager.Instance.PlayEffect(EffectType.Upgrade);
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region UpgradeUI
    // 업그레이드 데이터 항목 넣어주는 함수
    private void SetUpgradeInfo()
    {
        int maxCount = baseCost.upgradeCosts["baseUpgradeMaxCount"] ;
        // 이게 새로운 방식으로 적용한 텍스트 업데이트임 
        // 이렇게 새로운 클래스 리스트에 미리 데이터에 정보를 입력할 수 있게 미리 넣어둠
        // 만약 새로운 텍스트 정보 값 필요하면 밑쪽 보고 포멧 똑같이 해서 넣으면 됨
        // 데이터 딕셔너리 키 값 잘 입력하고 문제 생기면 이쪽도 확인 필요
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
    // 업그레이드 표시 항목 업데이트 함수
    private void UpgradeTextUpdate(int num)
    {
        if (upgradeInfos[num].count() == 5)
            upgradeCostText[num].text = "Max";
        else
            upgradeCostText[num].text = upgradeInfos[num].cost().ToString();

        upgradeStepImage[num].sprite = upgradeStepSprite[upgradeInfos[num].count()];
    }
    // 시작 시 업그레이드 항목 초기화 해주는 함수
    private void StartUpgradeTextUpdate()
    {
        for (int i = 0; i < upgradeCostText.Length; i++)
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
        int maxCount = baseCost.upgradeCosts["baseUpgradeMaxCount"] ;
        // 이건 Switch 문 말고 다른걸로 해보고 싶었는데 방법이 잘 생각안났음
        // 추후 새로운 방법 떠오르면 이쪽 바꾸면 될꺼임
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
                    LogMessage("최대 업그레이드 입니다.");
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
                    LogMessage("최대 업그레이드 입니다.");
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
                    LogMessage("최대 업그레이드 입니다.");
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
                    LogMessage("최대 업그레이드 입니다.");
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
                    LogMessage("최대 업그레이드 입니다.");
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
                        try
                        {
                            employee.name = gm.employee[random].name;
                        }
                        catch (System.Exception err)
                        {
                            Debug.Log(err);
                        }
                        gm.employee.RemoveAt(random);
                        p.employee.Add(employee);


                        baseCost.upgradeCosts["baseEmployeeAddCost"] *= 2;
                        baseCost.upgradeCosts["baseEmployeeAddCount"]++;
                        UpgradeTextUpdate(5);
                    }
                }
                else
                    LogMessage("종업원이 최대입니다.");
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

        store.totalGold = 0;
        StoreUI();
    }
    #endregion

    #region GameTest UI

    private void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);

        // 폰트 사이즈 조정
        buttonStyle.fontSize = 25;

        if (GUI.Button(new Rect(200, 250, 200, 100), "가이드 넘기기", buttonStyle))
            guide.ToNextStep();
        if (GUI.Button(new Rect(430, 250, 200, 100), "돈", buttonStyle))
            AddGold(900);
    }
    public void SetGuideStep(Guide guide)
    {
        this.guide = guide;
    }
    #endregion
}
