using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using Churub.Core;

public enum UnlockType
{
    Office,
    Container1,
    Machine1,
    Container2,
    Machine2,
    Stall,
    Store
}

public class UnlockManager : MonoBehaviour
{
    [EnumToggleButtons, SerializeField] private UnlockType unlockType;
    [SerializeField] private Dictionary<UnlockType, int> unlockAmount;
    [SerializeField] private GameObject _Object;
    [SerializeField] private GameObject _Wall;
    [SerializeField] private GameObject _SideWalk;

    [TitleGroup("UI"), SerializeField] private Image _FillImage;
    [TitleGroup("UI"), ProgressBar(0, 100), SerializeField] private float currentFill;
    private int amount;
    public UnlockType Type => unlockType;
    public bool IsPurchased => isUnlocked ||
        (baseCost != null && BalanceTable.IsFacilityUnlocked(baseCost, unlockType.ToString()));
    private string lockReason;
    private Coroutine unlockRoutine;
    private TMPro.TMP_Text[] priceLabels;
    private UnlockProgressView progressView;
    private int investedAmount;
    private bool investmentChanged;

    private const float unlockTime = 3.0f;
    private const float investmentDelay = 0.4f;
    private bool isTrigger = false;
    private bool isUnlocked = false;

    private Player player;
    private BaseCost baseCost;
    private AudioManager audioManager;

    private void Awake()
    {
        player = GameManager.Instance.P;
        baseCost = DataManager.Instance.baseCost;
        audioManager = AudioManager.Instance;
        if (unlockType == UnlockType.Store)
            UIManager.Instance.storeUpgradeButton.onClick.AddListener(UnlockStore);

        string facilityKey = unlockType.ToString();
        amount = BalanceTable.FacilityCost(facilityKey);
        investedAmount = BalanceTable.FacilityInvestment(baseCost, facilityKey);
        baseCost.SetFacilityInvestment(facilityKey, investedAmount);
        currentFill = amount > 0 ? investedAmount * 100f / amount : 100f;

        _Object.SetActive(false);
        CheckUnlockStatus();

        priceLabels = GetComponentsInChildren<TMPro.TMP_Text>(true);

        if (!isUnlocked && _FillImage != null)
        {
            progressView = gameObject.AddComponent<UnlockProgressView>();
            if (progressView.Initialize(_FillImage))
            {
                progressView.SetProgress(currentFill / 100f, false);
                progressView.SetInvestment(investedAmount, amount, BalanceTable.FacilityLock(baseCost, facilityKey));
            }
        }
    }

    private void Update()
    {
        lockReason = BalanceTable.FacilityLock(baseCost, unlockType.ToString());

        if (!isUnlocked && investedAmount >= amount && lockReason == null)
        {
            CompleteUnlock();
            return;
        }

        foreach (var label in priceLabels)
            label.text = lockReason ?? GetPriceText();

        if (progressView != null)
            progressView.SetInvestment(investedAmount, amount, lockReason);
    }

    private void Start()
    {
        if (unlockType == UnlockType.Store && _Object.activeSelf)
        {
            UIManager.Instance.storeUpgradeButton.gameObject.SetActive(false);
        }
    }

    private void CheckUnlockStatus()
    {
        if (baseCost.IsUnlocked(unlockType.ToString()))
        {
            isUnlocked = true;
            _Object.SetActive(true);
            DisableObjects();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") || unlockType == UnlockType.Store || IsPurchased) return;

        isTrigger = true;
        if (unlockRoutine == null && player.Gold >= 1f && BalanceTable.FacilityLock(baseCost, unlockType.ToString()) == null)
            unlockRoutine = StartCoroutine(UnlockProcess());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isUnlocked)
        {
            isTrigger = false;
            StopUnlockRoutine();
            SaveInvestmentIfNeeded();
        }
    }

    private IEnumerator UnlockProcess()
    {
        float delay = 0f;
        while (delay < investmentDelay && CanContinueInvestment())
        {
            delay += Time.deltaTime;
            yield return null;
        }

        float investmentAccumulator = 0f;
        float investmentPerSecond = amount / unlockTime;

        while (CanContinueInvestment())
        {
            investmentAccumulator += investmentPerSecond * Time.deltaTime;
            int remaining = amount - investedAmount;
            int affordable = Mathf.FloorToInt(player.Gold);
            int spendAmount = Mathf.Min(Mathf.FloorToInt(investmentAccumulator), remaining, affordable);

            if (spendAmount > 0)
            {
                if (!UIManager.Instance.SpendGold(spendAmount)) break;

                investmentAccumulator -= spendAmount;
                investedAmount += spendAmount;
                baseCost.SetFacilityInvestment(unlockType.ToString(), investedAmount);
                currentFill = investedAmount * 100f / amount;
                investmentChanged = true;
                UpdateUnlockUI(currentFill / 100f);

                if (investedAmount >= amount)
                {
                    CompleteUnlock();
                    unlockRoutine = null;
                    yield break;
                }
            }

            if (affordable <= 0) break;
            yield return null;
        }

        SaveInvestmentIfNeeded();
        unlockRoutine = null;
    }

    private bool CanContinueInvestment()
    {
        return isTrigger && !IsPurchased && investedAmount < amount && player.Gold >= 1f &&
            BalanceTable.FacilityLock(baseCost, unlockType.ToString()) == null;
    }

    private void CompleteUnlock()
    {
        if (IsPurchased || investedAmount < amount || BalanceTable.FacilityLock(baseCost, unlockType.ToString()) != null) return;

        investedAmount = amount;
        baseCost.SetFacilityInvestment(unlockType.ToString(), investedAmount);
        currentFill = 100f;
        UpdateUnlockUI(1f);

        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(false);
        }
        audioManager.PlayEffect(EffectType.UnLock);
        player.PT = PlayerType.None;
        _Object.SetActive(true);
        AnimateObject();

        isUnlocked = true;
        UpdateProgress();
        investmentChanged = false;
        DataManager.Instance.GameDataUpdate();
    }

    private void AnimateObject()
    {
        _Object.transform.DOScale(Vector3.zero, 0f);
        _Object.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InBounce)
            .OnComplete(() =>
            {
                GameManager.Instance.NowNavMeshBake();
                Vibration.VibratePop();
                player.PT = PlayerType.Joystick;
            });
    }

    private void UpdateProgress()
    {
        baseCost.SetUnlocked(unlockType.ToString(), true);
        switch (unlockType)
        {
            case UnlockType.Office:
                baseCost.SetUnlocked(GameDataSchema.Progress.Office, true);
                DisableObjects();
                break;
            case UnlockType.Container1:
            case UnlockType.Container2:

                DisableWall();
                break;
            case UnlockType.Machine1:
            case UnlockType.Machine2:

                break;
            case UnlockType.Stall:
                baseCost.SetUnlocked(GameDataSchema.Progress.Stall, true);
                break;
            case UnlockType.Store:
                baseCost.SetUnlocked(GameDataSchema.Progress.Stall, false);
                baseCost.SetUnlocked(GameDataSchema.Progress.Store, true);
                DisableObjects();
                break;
        }
    }

    private void UpdateUnlockUI(float progress)
    {
        if (progressView != null)
        {
            progressView.SetProgress(progress);
            progressView.SetInvestment(investedAmount, amount, lockReason);
        }
        else if (_FillImage != null)
            _FillImage.fillAmount = progress;
    }

    private void UnlockStore()
    {
        if (unlockType != UnlockType.Store || IsPurchased || BalanceTable.FacilityLock(baseCost, unlockType.ToString()) != null) return;

        int remaining = amount - investedAmount;
        if (remaining > 0 && !UIManager.Instance.SpendGold(remaining)) return;

        investedAmount = amount;
        baseCost.SetFacilityInvestment(unlockType.ToString(), investedAmount);
        CompleteUnlock();
    }

    private string GetPriceText()
    {
        return unlockType == UnlockType.Store
            ? amount.ToString("N0")
            : $"{investedAmount:N0} / {amount:N0}";
    }

    private void StopUnlockRoutine()
    {
        if (unlockRoutine == null) return;
        StopCoroutine(unlockRoutine);
        unlockRoutine = null;
    }

    private void SaveInvestmentIfNeeded()
    {
        if (!investmentChanged) return;
        investmentChanged = false;
        DataManager.Instance.GameDataUpdate();
    }

    private void OnDisable()
    {
        isTrigger = false;
        StopUnlockRoutine();
        SaveInvestmentIfNeeded();
    }

    private void OnDestroy()
    {
        var ui = FindObjectOfType<UIManager>();
        if (unlockType == UnlockType.Store && ui != null && ui.storeUpgradeButton != null)
            ui.storeUpgradeButton.onClick.RemoveListener(UnlockStore);
    }

    private void DisableWall()
    {
        if (_Wall != null)
        {
            _Wall.SetActive(false);
        }
    }
    private void DisableSideWalk()
    {
        if (_SideWalk != null)
        {
            _SideWalk.SetActive(false);
        }
    }
    private void DisableObjects()
    {
        DisableWall();
        DisableSideWalk();
    }
}
