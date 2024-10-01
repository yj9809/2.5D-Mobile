using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class BreakPointEvent : MonoBehaviour
{
    [SerializeField] private ConveyorBelt conveyorBelt;
    [TabGroup("BreakEvent"), ProgressBar(0, 100), SerializeField] private float currentFill;
    [TabGroup("BreakEvent"), SerializeField] private Image eventGauge;

    private bool isTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = true;
            conveyorBelt.BreakDownSolution();
            StartCoroutine(SolutionInProgress(currentFill));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }
    private IEnumerator SolutionInProgress(float updateProcess)
    {
        currentFill = updateProcess;
        float fillRate = 100f / 3;
        while (currentFill < 100)
        {
            if (isTrigger)
            {
                currentFill += fillRate * Time.deltaTime;
                UpdateUnlockUI(currentFill / 100);
            }
            yield return null;
        }

        if (currentFill >= 100)
        {
            eventGauge.fillAmount = 0;
            currentFill = 0;
            conveyorBelt.BreakDownSolutionClear();
            this.gameObject.SetActive(false);
        }
    }
    private void UpdateUnlockUI(float progress)
    {
        if (eventGauge != null)
        {
            eventGauge.fillAmount = progress;
        }
    }
}
