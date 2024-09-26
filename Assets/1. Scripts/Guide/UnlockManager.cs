using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public enum UnlockType
{
    Office,
    Machine,
    Store
}

public class UnlockManager : MonoBehaviour
{
    [EnumToggleButtons, SerializeField] private UnlockType unlockType;
    [SerializeField] private GameObject _Object;
    [SerializeField] private GameObject _Wall;
    [SerializeField] private Image _FillImage;
    [SerializeField] private int amount;
    [ProgressBar(0, 100), SerializeField] private float currentFill;

    private float unlockTime = 3.0f;
    private bool isTrigger = false;
    private bool isUnlocked = false;

    private Player player;
    private BaseCost baseCost;
    private void Awake()
    {
        player = GameManager.Instance.P;
        baseCost = DataManager.Instance.baseCost;

        _Object.SetActive(false);

        DataCheck();
    }

    private void DataCheck()
    {
        if (baseCost.unlockOffice && unlockType == UnlockType.Office)
        {
            _Object.SetActive(true);
            DestoryWall();
            gameObject.SetActive(false);
        }
        else if (baseCost.unlockMachine && unlockType == UnlockType.Machine)
        {
            _Object.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (baseCost.unlockStore && unlockType == UnlockType.Store)
        {
            _Object.SetActive(true);
            DestoryWall();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isUnlocked && player.Gold >= amount)
        {
            isTrigger = true;
            StartCoroutine(UnlockProcess(currentFill));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isUnlocked)
        {
            isTrigger = false;
        }
    }

    private IEnumerator UnlockProcess(float updateProcess)
    {
        currentFill = updateProcess;
        float fillRate = 100f / unlockTime;
        while (currentFill < 100)
        {
            if (isTrigger)
            {
                currentFill += fillRate * Time.deltaTime;
                UpdateUnlockUI(currentFill / 100);
            }
            yield return null;
        }

        // 오브젝트 생성구간
        if (currentFill >= 100)
        {
            if (unlockType == UnlockType.Office)
            {
                SetActiveObject();
                DestoryWall();
            }
            else if (unlockType == UnlockType.Machine)
            {
                SetActiveObject();
            }
            else if (unlockType == UnlockType.Store)
            {
                SetActiveObject();
                DestoryWall();
            }
        }
    }

    private void SetActiveObject()
    {
        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(false);
        }
        _Object.gameObject.SetActive(true);
        _Object.transform.DOScale(Vector3.zero, 0f);
        _Object.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InBounce)
            .OnComplete(() =>
            {
                GameManager.Instance.NowNavMeshBake();
                Vibration.VibratePop();
            }
            );

        isUnlocked = true;

        UIManager.Instance.SpendGold(amount);

        UpdataObject();
    }

    private void UpdataObject()
    {
        if (unlockType == UnlockType.Office)
        {
            baseCost.unlockOffice = true;
            DestoryWall();
        }
        else if (unlockType == UnlockType.Machine)
        {
            baseCost.unlockMachine = true;
        }
        else if (unlockType == UnlockType.Store)
        {
            baseCost.unlockStore = true;
            DestoryWall();
        }
    }

    private void UpdateUnlockUI(float progress)
    {
        if (_FillImage != null)
        {
            _FillImage.fillAmount = progress;
        }
    }

    private void DestoryWall()
    {
        _Wall.SetActive(false);
    }
}
