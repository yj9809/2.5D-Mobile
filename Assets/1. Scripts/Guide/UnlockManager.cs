using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public enum UnlockType
{
    Office,
    Container,
    Machine,
    Store
}

public class UnlockManager : MonoBehaviour
{
    [EnumToggleButtons, SerializeField] private UnlockType unlockType;
    [SerializeField] private GameObject _Object;
    [SerializeField] private GameObject _Wall;
    [SerializeField] private GameObject _SideWalk;
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
        if (baseCost.gameProgressBool["unlockOffice"] && unlockType == UnlockType.Office)
        {
            _Object.SetActive(true);
            ActiveFalseObject();
            gameObject.SetActive(false);
        }
        else if (unlockType == UnlockType.Container)
        {
            if (baseCost.gameProgressBool["unlockContainer_1"])
            {
                _Object.SetActive(true);
                gameObject.SetActive(false);
                ActiveFalseWall();
            }
            else if (baseCost.gameProgressBool["unlockContainer_2"])
            {
                _Object.SetActive(true);
                gameObject.SetActive(false);
                ActiveFalseWall();
            }
        }
        else if (unlockType == UnlockType.Machine)
        {
            if (baseCost.gameProgressBool["unlockMachine_1"])
            {
                _Object.SetActive(true);
                gameObject.SetActive(false);
            }
            else if (baseCost.gameProgressBool["unlockMachine_2"])
            {
                _Object.SetActive(true);
                gameObject.SetActive(false);
            }
        }
        else if (baseCost.gameProgressBool["unlockStore"] && unlockType == UnlockType.Store)
        {
            _Object.SetActive(true);
            ActiveFalseObject();
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
                ActiveFalseObject();
            }
            else if (unlockType == UnlockType.Container)
            {
                SetActiveObject();
                ActiveFalseWall();
            }
            else if (unlockType == UnlockType.Machine)
            {
                SetActiveObject();
            }
            else if (unlockType == UnlockType.Store)
            {
                SetActiveObject();
                ActiveFalseObject();
            }
        }

        if (unlockType != UnlockType.Store)
        {
            player.transform.position = new Vector3(transform.position.x - 3f, transform.position.y, transform.position.z);
        }
        else
        {
            player.transform.position = new Vector3(transform.position.x + 3f, transform.position.y, transform.position.z);
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
            baseCost.gameProgressBool["unlockOffice"] = true;
            ActiveFalseObject();
        }
        else if (unlockType == UnlockType.Container)
        {
            if (!baseCost.gameProgressBool["unlockContainer_1"] && !baseCost.gameProgressBool["unlockContainer_2"])
            {
                baseCost.gameProgressBool["unlockContainer_1"] = true;
            }
            else if (baseCost.gameProgressBool["unlockContainer_1"] && !baseCost.gameProgressBool["unlockContainer_2"])
            {
                baseCost.gameProgressBool["unlockContainer_2"] = true;
            }
            ActiveFalseWall();
        }
        else if (unlockType == UnlockType.Machine)
        {
            if (!baseCost.gameProgressBool["unlockMachine_1"] && !baseCost.gameProgressBool["unlockMachine_2"])
            {
                baseCost.gameProgressBool["unlockMachine_1"] = true;
            }
            else if (baseCost.gameProgressBool["unlockMachine_1"] && !baseCost.gameProgressBool["unlockMachine_2"])
            {
                baseCost.gameProgressBool["unlockMachine_2"] = true;
            }
        }
        else if (unlockType == UnlockType.Store)
        {
            baseCost.gameProgressBool["unlockStore"] = true;
            ActiveFalseObject();
        }
    }

    private void UpdateUnlockUI(float progress)
    {
        if (_FillImage != null)
        {
            _FillImage.fillAmount = progress;
        }
    }

    private void ActiveFalseWall()
    {
        _Wall.SetActive(false);
    }
    private void ActiveFalseSideWlk()
    {
        _SideWalk.SetActive(false);
    }
    private void ActiveFalseObject()
    {
        ActiveFalseWall();
        ActiveFalseSideWlk();
    }
}
