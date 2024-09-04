using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Sirenix.OdinInspector;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Button cameraZoomButton;

    [SerializeField] private TextMeshProUGUI goldTxt;

    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private Button speedUpgradeButton;
    [SerializeField] private Button cartSpeedUpgradeButton;
    [SerializeField] private Button maxObjStackCountUpgradeButton;

    [SerializeField] private Button breakDownButton;

    ////여기부터 윤제영에 테스트 참조임
    //[SerializeField] private Sprite[] onOffSprite;
    //[SerializeField] private Image onOffImage;
    //[SerializeField] private Slider testSilder;
    //[SerializeField] private Sprite[] buttonSprite;
    //[SerializeField] private Image buttonImage;
    private IngredientMaker im;
    private ConveyorBelt cb;
    ////여기까지 윤제영에 테스트 참조였음

    private Player p;
    private DataManager data;

    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.P;
        data = DataManager.Instance;

        upgradePanel.SetActive(false);
        breakDownButton.gameObject.SetActive(false);

        UpdateUI();
        speedUpgradeButton.onClick.AddListener(UpgradePlayerSpeed);
        cartSpeedUpgradeButton.onClick.AddListener(UpgradePlayerCartSpeed);
        maxObjStackCountUpgradeButton.onClick.AddListener(UpgradePlayerMaxStack);
        cameraZoomButton.onClick.AddListener(GameManager.Instance.MainCamera.ZoomScreen);

        breakDownButton.onClick.AddListener(BreakDownEventButton);
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
        goldTxt.text = ChangeNumbet(p.Gold.ToString());
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
            Debug.Log("골드가 부족하여 업그레이드를 진행할 수 없습니다.");
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
            Debug.Log("골드가 부족하여 업그레이드를 진행할 수 없습니다.");
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
            Debug.Log("골드가 부족하여 업그레이드를 진행할 수 없습니다.");
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
    /*
    private void OnGUI()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);

        // 폰트 사이즈 조정
        buttonStyle.fontSize = 25;

        if (GUI.Button(new Rect(10, 250, 200, 100), "소지갯수 올리기", buttonStyle))
            p.MaxObjStackCount += 1;
        if (GUI.Button(new Rect(10, 360, 200, 100), "소지갯수 내리기", buttonStyle))
            p.MaxObjStackCount -= 1;
        if (GUI.Button(new Rect(10, 470, 200, 100), "이동속도 올리기", buttonStyle))
            p.BaseSpeed += 1;
        if (GUI.Button(new Rect(10, 580, 200, 100), "이동속도 내리기", buttonStyle))
            p.BaseSpeed -= 1;

        string textAreaString = $"소지갯수:{p.MaxObjStackCount}\n이동속도:{p.BaseSpeed}";
        textAreaString = GUI.TextArea(new Rect(10, 690, 200, 100), textAreaString, buttonStyle);

        if (GUI.Button(new Rect(220, 250, 200, 100), "재료생산 느리게", buttonStyle))
            im.ObjSpawnTime += 1;
        if (GUI.Button(new Rect(220, 360, 200, 100), "재료생산 빠르게", buttonStyle))
            im.ObjSpawnTime -= 1;
        if (GUI.Button(new Rect(220, 470, 200, 100), "재료변환 느리게", buttonStyle))
            cb.PlaceObjectTime += 1;
        if (GUI.Button(new Rect(220, 580, 200, 100), "재료변환 빠르게", buttonStyle))
            cb.PlaceObjectTime -= 1;
        string abc = $"재료생산:{im.ObjSpawnTime}초\n재료변환:{cb.PlaceObjectTime}초";
        abc = GUI.TextArea(new Rect(220, 690, 200, 100), abc, buttonStyle);

        if (GUI.Button(new Rect(430, 250, 200, 100), "돈", buttonStyle))
            AddGold(900);
        if (GUI.Button(new Rect(430, 360, 200, 100), "고장확률 높게", buttonStyle))
            cb.BreakDownProb += 0.1f;
        if (GUI.Button(new Rect(430, 470, 200, 100), "고장확률 낮게", buttonStyle))
            cb.BreakDownProb -= 0.1f;
        string def = $"고장확률:{cb.BreakDownProb * 100}%";
        def = GUI.TextArea(new Rect(430, 580, 200, 100), def, buttonStyle);
    }
    public void SetIngredientMaker(IngredientMaker im)
    {
        this.im = im;
    }
    
    //여기까지 윤제영 테스트 함수였음
    */
}
