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
        goldTxt.text = "Gold: " + p.Gold.ToString();
    }

    public void SellItem()
    {
        //테스트용
        AddGold(10);
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

    //여기부터 윤제영 테스트 함수임
    private void OnGUI()
    {

        if (GUI.Button(new Rect(10, 50, 80, 20), "PMS Up"))
            p.MaxObjStackCount += 1;
        if (GUI.Button(new Rect(10, 70, 80, 20), "PMS Down"))
            p.MaxObjStackCount -= 1;
        if (GUI.Button(new Rect(10, 90, 80, 20), "PS Up"))
            p.BaseSpeed += 1;
        if (GUI.Button(new Rect(10, 110, 80, 20), "PS Down"))
            p.BaseSpeed -= 1;

        string textAreaString = $"PMS:{p.MaxObjStackCount} PS:{p.BaseSpeed}";
        textAreaString = GUI.TextArea(new Rect(10, 130, 80, 20), textAreaString);

        if (GUI.Button(new Rect(90, 50, 80, 20), "IM Up"))
            im.ObjSpawnTime += 1;
        if (GUI.Button(new Rect(90, 70, 80, 20), "IM Down"))
            im.ObjSpawnTime -= 1;
        if (GUI.Button(new Rect(90, 90, 80, 20), "CB Up"))
            cb.PlaceObjectTime += 1;
        if (GUI.Button(new Rect(90, 110, 80, 20), "CB Down"))
            cb.PlaceObjectTime -= 1;
        string abc = $"IM:{im.ObjSpawnTime} CB:{cb.PlaceObjectTime}";
        abc = GUI.TextArea(new Rect(90, 130, 80, 20), abc);
    }
    public void SetIngredientMaker(IngredientMaker im)
    {
        this.im = im;
    }
    public void SetConveyorBelt(ConveyorBelt cb)
    {
        this.cb = cb;
    }
    //여기까지 윤제영 테스트 함수였음
}
