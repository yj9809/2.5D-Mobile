using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGuide : MonoBehaviour
{
    private bool doTutorial = false;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private GameObject tutorialPanel;

    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] targets;
    [SerializeField] private RectTransform arrow;
    [SerializeField] private RectTransform canvas;

    private int step;
    private BoxPackaging boxPackaging;
    private BoxStorage boxStorage;
    private Truck truck;

    void Start()
    {
        player = GameManager.Instance.P.transform;

        tutorialButton.onClick.AddListener(ToggleTutorial);
        arrow.gameObject.SetActive(false);

        step = DataManager.Instance.baseCost.tutorialStep;

        boxPackaging = FindObjectOfType<BoxPackaging>();
        boxStorage = GameObject.Find("BoxStorage").GetComponent<BoxStorage>();
        truck = GameObject.Find("Truck").GetComponent<Truck>();

        SetTargetsActive(false);
    }

    void Update()
    {
        if (doTutorial)
        {
            ShowTutorialGuide();
            GuideLine();
            TutorialGuideStep();
        }
        else
        {
            CloseTutorialGuide();
        }
    }

    private void ToggleTutorial()
    {
        doTutorial = !doTutorial;
        Debug.Log("doTutorial: " + doTutorial);
    }

    private void ShowTutorialGuide()
    {
        arrow.gameObject.SetActive(true);
    }

    private void CloseTutorialGuide()
    {
        arrow.gameObject.SetActive(false);
    }

    private void GuideLine()
    {
        if (step < targets.Length)
        {
            Transform target = targets[step].transform;
            Vector3 direction = target.position - player.position;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            arrow.rotation = lookRotation * Quaternion.Euler(90, 0, 0);

            float distance = direction.magnitude;
            float guideScaleFactor = Mathf.Clamp(distance / 10f, 0.3f, 1f);
            canvas.localScale = new Vector3(guideScaleFactor, guideScaleFactor, guideScaleFactor);

            arrow.localPosition = Vector3.zero;
        }
    }

    private void TutorialGuideStep()
    {
        switch (step)
        {
            case 0: _Step0(); break;
            case 1: _Step1(); break;
            case 2: _Step2(); break;
            case 3: _Step3(); break;
            case 4: _Step4(); break;
            case 5: _Step5(); break;
            case 6: _Step6(); break;
            case 7: _Step7(); break;
        }
    }

    private void _Step0()
    {
        Debug.Log("Step 1 !");
        SetActiveTarget(0);

        if (GameManager.Instance.P.IngredientStack.Count > 0)
        {
            ToNextStep();
        }
        else
        {
            StepChange();
        }
    }

    private void _Step1()
    {
        Debug.Log("Step 2 !");
        SetActiveTarget(1);

        if (GameManager.Instance.P.IngredientStack.Count <= 0)
        {
            ToNextStep();
        }
    }

    private void _Step2()
    {
        Debug.Log("Step 3 !");
        SetActiveTarget(2);

        if (GameManager.Instance.P.ChuruStack.Count > 0)
        {
            ToNextStep();
        }
    }

    private void _Step3()
    {
        Debug.Log("Step 4 !");
        SetActiveTarget(3);

        if (GameManager.Instance.P.ChuruStack.Count <= 0 && boxPackaging.ChuruStorage.Count >= 5)
        {
            ToNextStep();
        }
        else if (GameManager.Instance.P.ChuruStack.Count == 0 && boxPackaging.ChuruStorage.Count < 5)
        {
            step = 0;
        }
    }

    private void _Step4()
    {
        Debug.Log("Step 5 !");
        SetActiveTarget(4);

        if (boxStorage.bsType == BoxStorageType.BoxStorage && boxStorage.BoxStack.Count >= 1)
        {
            ToNextStep();
        }
        else if (boxStorage.bsType == BoxStorageType.BoxStorage && boxStorage.BoxStack.Count < 1)
        {
            step = 0;
        }
    }

    private void _Step5()
    {
        Debug.Log("Step 6 !");
        SetActiveTarget(5);

        if (GameManager.Instance.P.BoxStack.Count > 0)
        {
            ToNextStep();
        }
    }

    private void _Step6()
    {
        Debug.Log("Step 7 !");
        SetActiveTarget(6);

        if (GameManager.Instance.P.BoxStack.Count <= 0 && truck.BoxStack.Count >= 5)
        {
            ToNextStep();
        }
        else if (truck.BoxStack.Count < 5)
        {
            step = 0;
        }
    }

    private void _Step7()
    {
        Debug.Log("Step 8 !");
        // 강화 관련 작업
    }

    private void ToNextStep()
    {
        step++;
    }

    private void StepChange()
    {
        if (boxPackaging.ChuruStorage.Count >= 5)
        {
            GoToStep(4);
        }
        else if (boxStorage.BoxStack.Count >= 1)
        {
            GoToStep(5);
        }
        else if (truck.BoxStack.Count >= 5)
        {
            GoToStep(7);
        }
    }

    private void GoToStep(int newStep)
    {
        step = newStep;
    }

    private void SetTargetsActive(bool isActive)
    {
        foreach (var target in targets)
        {
            target.SetActive(isActive);
        }
    }

    private void SetActiveTarget(int index)
    {
        if (index >= 0 && index < targets.Length)
        {
            targets[index].SetActive(true);
        }
    }
}
