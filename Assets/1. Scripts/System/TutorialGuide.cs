using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGuide : MonoBehaviour
{
    private bool doTutorial = false;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private GameObject turorialPanal;

    [SerializeField] private Transform player;
    [SerializeField] private Transform[] targets;
    [SerializeField] private RectTransform canvas;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.P.transform;

        tutorialButton.onClick.AddListener(OnClickTutorialButton);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (doTutorial)
    //    {
            
    //    }
    //}

    private void OnClickTutorialButton()
    {
        doTutorial = !doTutorial;
        if (doTutorial)
        {
            ShowTutorialGuide();
            GuideLine();
        }
        else
        {
            CloseTutorialGuide();
        }
    }

    private void ShowTutorialGuide()
    {
        TutorialGuideStep();
    }
    private void CloseTutorialGuide()
    {

    }

    private void GuideLine()
    {
        int step = DataManager.Instance.baseCost.tutorialStep;
        if (step >= 0)
        {
            Transform target = targets[step];
            Vector3 direction = target.position - player.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            canvas.rotation = lookRotation * Quaternion.Euler(90, 0, 0);
            canvas.localPosition = Vector3.zero;
        }
    }

    private void TutorialGuideStep()
    {
        switch (DataManager.Instance.baseCost.tutorialStep)
        {
            case 0:

                ToNextStep();
                break;
            case 1:

                ToNextStep();
                break;
            case 2:

                ToNextStep();
                break;
            case 3:

                ToNextStep();
                break;
            case 4:

                break;
        }
    }

    private void ToNextStep()
    {
        DataManager.Instance.baseCost.tutorialStep++;
    }
}
