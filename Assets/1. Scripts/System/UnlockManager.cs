using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnlockManager : MonoBehaviour
{
    [SerializeField] private GameObject lockPrefab;
    [SerializeField] private float unlockTime = 3.0f;
    [SerializeField] private Image unlockFillImage;
    private float currentFill;

    private bool isUnlocked = false;
    private Coroutine unlockCoroutine;
    private Player p;

    void Start()
    {
        p = GameManager.Instance.P;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isUnlocked)
        {
            unlockCoroutine = StartCoroutine(UnlockRoutine(currentFill));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isUnlocked && unlockCoroutine != null)
        {
            StopCoroutine(unlockCoroutine);
        }
    }

    private IEnumerator UnlockRoutine(float unlockProgress)
    {
        currentFill = unlockProgress;

        while (currentFill < unlockTime)
        {
            currentFill += Time.deltaTime;
            UpdateUnlockUI(currentFill / unlockTime);
            yield return null;
        }

        Instantiate(lockPrefab, transform.position, Quaternion.identity);
        isUnlocked = true;
        ResetUnlockUI();
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
