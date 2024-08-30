using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UnlockManager : MonoBehaviour
{
    [SerializeField] private GameObject lockObject;
    [SerializeField] private float unlockTime = 3.0f;
    [SerializeField] private Image unlockFillImage;
    private float currentFill;

    private bool isTrigger = false;
    private bool isUnlocked = false;
    private int amount;

    private Player p;

    void Start()
    {
        p = GameManager.Instance.P;
        amount = 1000;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isUnlocked && p.Gold > amount)
        {
            isTrigger = true;
            UIManager.Instance.SpendGold(amount);
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

        while (currentFill < unlockTime)
        {
            if (isTrigger)
            {
                currentFill += Time.deltaTime;
                UpdateUnlockUI(currentFill / unlockTime);
            }
            yield return null;
        }

        // 오브젝트 생성구간
        if (currentFill >= unlockTime)
        {
            //Instantiate(lockPrefab, transform.position, Quaternion.identity);
            lockObject.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InBounce);
            isUnlocked = true;
            ResetUnlockUI();
        }
    }

    private void UpdateUnlockUI(float progress)
    {
        if (unlockFillImage != null)
        {
            unlockFillImage.fillAmount = progress;
        }
    }

    private void ResetUnlockUI()
    {
        if (unlockFillImage != null)
        {
            unlockFillImage.fillAmount = 0f;
        }
    }
}
