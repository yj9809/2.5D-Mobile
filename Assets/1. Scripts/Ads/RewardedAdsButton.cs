using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button _speedBuffButton;
    [SerializeField] private Button _maxObjStackCountBuffButton;
    [SerializeField] private Button _goldBuffButton;
    [SerializeField] private Image[] buffOffImage;
    [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] private string _iOSAdUnitId = "Rewarded_iOS";

    private GameManager gm;
    private string _adUnitId = null;

    void Awake()
    {
        //#if UNITY_IOS
        //        _adUnitId = _iOSAdUnitId;
        //#elif UNITY_ANDROID
        //        _adUnitId = _androidAdUnitId;
        //#endif

        _speedBuffButton.interactable = false;
        _maxObjStackCountBuffButton.interactable = false;
        _goldBuffButton.interactable = false;
        _adUnitId = _androidAdUnitId;
        gm = GameManager.Instance;
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
            _speedBuffButton.onClick.AddListener(() => ShowAdAndApplyBuff("Speed"));
            _maxObjStackCountBuffButton.onClick.AddListener(() => ShowAdAndApplyBuff("MaxObjStackCount"));
            _goldBuffButton.onClick.AddListener(() => ShowAdAndApplyBuff("Gold"));
            _speedBuffButton.interactable = true;
            _goldBuffButton.interactable = true;
            _maxObjStackCountBuffButton.interactable = true;
        }
    }

    public void ShowAdAndApplyBuff(string buffType)
    {
        switch (buffType)
        {
            case "Speed":
                _speedBuffButton.interactable = false;
                buffOffImage[0].gameObject.SetActive(true);
                break;
            case "MaxObjStackCount":
                _maxObjStackCountBuffButton.interactable = false;
                buffOffImage[1].gameObject.SetActive(true);
                break;
            case "Gold":
                _goldBuffButton.interactable = false;
                buffOffImage[2].gameObject.SetActive(true);
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
                StartCoroutine(ApplyBuff("Speed", 5));
            }
            if (_maxObjStackCountBuffButton.interactable == false)
            {
                StartCoroutine(ApplyBuff("MaxObjStackCount", 30));
            }
            if (_goldBuffButton.interactable == false)
            {
                StartCoroutine(ApplyBuff("Gold", 30));
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
        float buffedStat = 0;

        switch (buffType)
        {
            case "Speed":
                originalStat = GetPlayerSpeed();
                buffedStat = originalStat + 10f;
                SetPlayerSpeed(buffedStat);
                _speedBuffButton.interactable = false;
                buffOffImage[0].gameObject.SetActive(false);
                break;
            case "MaxObjStackCount":
                originalStat = GetMaxObjCount();
                buffedStat = originalStat + 3f;
                SetMaxObjCountSpeed(buffedStat);
                _maxObjStackCountBuffButton.interactable = false;
                buffOffImage[1].gameObject.SetActive(false);
                Debug.Log($"오브젝트 버프");
                break;
            case "Gold":
                originalStat = GetPlayerGold();
                buffedStat = originalStat + 1.5f;
                SetPlayerGold(buffedStat);
                Debug.Log("골드 버프");
                _goldBuffButton.interactable = false;
                buffOffImage[2].gameObject.SetActive(false);
                break;
        }

        yield return new WaitForSeconds(duration);
        switch (buffType)
        {
            case "Speed":
                SetPlayerSpeed(originalStat);
                _speedBuffButton.interactable = true;
                break;
            case "MaxObjStackCount":
                SetMaxObjCountSpeed(originalStat);
                _maxObjStackCountBuffButton.interactable = true;
                break;
            case "Gold":
                SetPlayerGold(originalStat);
                _goldBuffButton.interactable = true;
                break;
        }
        LoadAd();
        Clear();
    }

    private float GetPlayerSpeed()
    {
        return gm.P.buffSpeed;
    }

    private float GetPlayerGold()
    {
        return gm.P.buffGold;
    }

    private float GetMaxObjCount()
    {
        return gm.P.buffMaxObjStackCount;
    }

    private void SetPlayerSpeed(float stat)
    {
        gm.P.buffSpeed = stat;
    }

    private void SetPlayerGold(float stat)
    {
        gm.P.buffGold = stat;
    }

    private void SetMaxObjCountSpeed(float stat)
    {
        gm.P.buffMaxObjStackCount = stat;
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string adUnitId) { }

    public void OnUnityAdsShowClick(string adUnitId) { }

    void Clear()
    {
        _speedBuffButton.onClick.RemoveAllListeners();
        _maxObjStackCountBuffButton.onClick.RemoveAllListeners();
        _goldBuffButton.onClick.RemoveAllListeners();
    }
}
