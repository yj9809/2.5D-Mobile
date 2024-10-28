using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _speedBuffButton;
    [SerializeField] Button _goldBuffButton;
    [SerializeField] Button _machineSpeedBuffButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null;

    void Awake()
    {
        //#if UNITY_IOS
        //        _adUnitId = _iOSAdUnitId;
        //#elif UNITY_ANDROID
        //        _adUnitId = _androidAdUnitId;
        //#endif

        _speedBuffButton.interactable = false;
        _goldBuffButton.interactable = false;
        _machineSpeedBuffButton.interactable = false;
        _adUnitId = _androidAdUnitId;
    }
    private void Start()
    {
        LoadAd();
    }

    public void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId.Equals(_adUnitId))
        {
            _speedBuffButton.onClick.AddListener(() => ShowAdAndApplyBuff("speed"));
            _goldBuffButton.onClick.AddListener(() => ShowAdAndApplyBuff("gold"));
            _machineSpeedBuffButton.onClick.AddListener(() => ShowAdAndApplyBuff("machineSpeed"));
            _speedBuffButton.interactable = true;
            _goldBuffButton.interactable = true;
            _machineSpeedBuffButton.interactable = true;
        }
    }

    public void ShowAdAndApplyBuff(string buffType)
    {
        switch (buffType)
        {
            case "speed":
                _speedBuffButton.interactable = false;
                break;
            case "gold":
                _goldBuffButton.interactable = false;
                break;
            case "machineSpeed":
                _machineSpeedBuffButton.interactable = false;
                break;
        }
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            if (_speedBuffButton.interactable == false)
            {
                // 테스트 중이니까 테스트 끝난 후 5 -> 30초로 바꾸기
                StartCoroutine(ApplyBuff("speed", 5));
            }
            else if (_goldBuffButton.interactable == false)
            {
                StartCoroutine(ApplyBuff("gold", 30));
            }
            else if (_machineSpeedBuffButton.interactable == false)
            {
                StartCoroutine(ApplyBuff("machineSpeed", 30));
            }
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        LoadAd();
    }

    private IEnumerator ApplyBuff(string buffType, float duration)
    {
        float originalStat = 0;
        float cartSpeed = 0;
        float buffedStat = 0;
        float buffedStat2 = 0;

        switch (buffType)
        {
            case "speed":
                originalStat = GetPlayerSpeed(out cartSpeed);
                buffedStat = originalStat + 1f;
                buffedStat2 = cartSpeed + 1f;
                SetPlayerSpeed(buffedStat, buffedStat2);
                _speedBuffButton.interactable = false;
                break;

            case "gold":
                originalStat = GetPlayerGold();
                buffedStat = originalStat * 1.5f;
                SetPlayerGold(buffedStat);
                _goldBuffButton.interactable = false;
                break;

            case "machineSpeed":
                originalStat = GetMachineSpeed();
                buffedStat = originalStat - 1.5f;
                SetMachineSpeed(buffedStat);
                _machineSpeedBuffButton.interactable = false;
                break;
        }

        yield return new WaitForSeconds(duration);
        switch (buffType)
        {
            case "speed":
                SetPlayerSpeed(originalStat, cartSpeed);
                _speedBuffButton.interactable = true;
                break;
            case "gold":
                SetPlayerGold(originalStat);
                _goldBuffButton.interactable = true;
                break;
            case "machineSpeed":
                SetMachineSpeed(originalStat);
                _machineSpeedBuffButton.interactable = true;
                break;
        }
        LoadAd();
        Clear();
    }

    private float GetPlayerSpeed(out float cartSpeed)
    {
        cartSpeed = GameManager.Instance.P.CartSpeed;
        return GameManager.Instance.P.BaseSpeed;
    }

    private float GetPlayerGold()
    {
        return GameManager.Instance.P.GoldPerBox;
    }

    private float GetMachineSpeed()
    {
        return GameManager.Instance.cbTrans[0].parent.GetChild(3).GetComponent<ConveyorBelt>().PlaceObjectTime;
    }

    private void SetPlayerSpeed(float stat, float stat2)
    {
        GameManager.Instance.P.BaseSpeed = stat;
        GameManager.Instance.P.CartSpeed = stat2;
    }

    private void SetPlayerGold(float stat)
    {
        GameManager.Instance.P.GoldPerBox = stat;
    }

    private void SetMachineSpeed(float stat)
    {
        foreach (var item in GameManager.Instance.cbTrans)
        {
            item.parent.GetChild(3).GetComponent<ConveyorBelt>().PlaceObjectTime = stat;
        }
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string adUnitId) { }

    public void OnUnityAdsShowClick(string adUnitId) { }

    void Clear()
    {
        _speedBuffButton.onClick.RemoveAllListeners();
        _goldBuffButton.onClick.RemoveAllListeners();
        _machineSpeedBuffButton.onClick.RemoveAllListeners();
    }
}
