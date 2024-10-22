using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAdExample : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;

    void Awake()
    {
        // ���� �÷����� ���� ���� ���� ID ��������
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;

        // �ʱ� ���� �ε�
        LoadAd();
    }

    void Start()
    {
       
        // �� ���� �� ���� ��� ǥ��
        ShowAd();
        // 5��(300��)���� ���� ǥ�� ����
        InvokeRepeating("ShowAd", 300f, 300f);
    }

    // ���� ������ ������ �ε�:
    public void LoadAd()
    {
        // �߿�! �ʱ�ȭ ���Ŀ��� �������� �ε��ϼ��� (�� ���������� �ʱ�ȭ�� �ٸ� ��ũ��Ʈ���� ó����).
        Debug.Log("���� �ε� ��: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // ���� �������� �ε�� ������ ǥ��:
    public void ShowAd()
    {
        // ���� �������� ������ �ε���� ���� ��� �� �޼���� �����մϴ�.
        Debug.Log("���� ǥ�� ��: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    // Load Listener �� Show Listener �������̽� �޼��� ����:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // ���� ������ �������� ���������� �ε����� �� ������ �ڵ带 ���������� �ۼ��� �� �ֽ��ϴ�.
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"���� ���� �ε� ����: {_adUnitId} - {error.ToString()} - {message}");
        // ���� ������ �ε� ������ ��� ���������� �ٽ� �õ��ϴ� ���� �ڵ带 ������ �� �ֽ��ϴ�.
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"���� ���� ǥ�� ���� {_adUnitId}: {error.ToString()} - {message}");
        // ���� ������ ǥ�� ������ ��� ���������� �ٸ� ���� �ε��ϴ� ���� �ڵ带 ������ �� �ֽ��ϴ�.
    }

    public void OnUnityAdsShowStart(string _adUnitId) { }
    public void OnUnityAdsShowClick(string _adUnitId) { }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        LoadAd();
    }
}
