using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGuide : MonoBehaviour
{
    private bool doTutorial = false;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private GameObject turorialPanel;

    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] targets;
    [SerializeField] private RectTransform arrow;

    private int step;
    private BoxPackaging boxPackaging;
    private BoxStorage boxStorage;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.P.transform;

        tutorialButton.onClick.AddListener(OnClickTutorialButton);
        arrow.gameObject.SetActive(false);

        step = DataManager.Instance.baseCost.tutorialStep;

        boxPackaging = FindObjectOfType<BoxPackaging>();
        boxStorage = GameObject.Find("BoxStorage").GetComponent<BoxStorage>();

        foreach (var item in targets)
        {
            item.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
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

    private void OnClickTutorialButton()
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
        if (step >= 0)
        {
            Transform target = targets[step].transform;
            Vector3 direction = target.position - player.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            arrow.rotation = lookRotation * Quaternion.Euler(90, 0, 0);
            arrow.localPosition = Vector3.zero;
        }
    }

    private void TutorialGuideStep()
    {
        
        switch (step)
        {
            case 0: // 재료 받기
                Debug.Log("Step 1 !");
                targets[0].SetActive(true);
                if (GameManager.Instance.P.IngredientStack.Count > 0)
                {
                    ToNextStep();
                }
                break;
            case 1: // 컨베이어벨트에 넘기기
                Debug.Log("Step 2 !");
                targets[1].SetActive(true);
                if (GameManager.Instance.P.IngredientStack.Count <= 0)
                {
                    ToNextStep();
                }
                break;
            case 2: // 츄르 받기
                Debug.Log("Step 3 !");
                targets[2].SetActive(true);
                if (GameManager.Instance.P.ChuruStack.Count > 0)
                {
                    ToNextStep();
                }
                break;
            case 3: // 창고로 옮기기
                Debug.Log("Step 4 !");
                targets[3].SetActive(true);
                if (GameManager.Instance.P.ChuruStack.Count <= 0 && boxPackaging.ChuruStorage.Count >= 5)
                {
                    ToNextStep();
                }
                else if (GameManager.Instance.P.ChuruStack.Count == 0 && boxPackaging.ChuruStorage.Count < 5)
                {
                    step = 0;
                }
                break;
            case 4: // 박스 만들기
                Debug.Log("Step 5 !");
                targets[4].SetActive(true);
                if (boxStorage.bsType == BoxStorageType.BoxStorage && boxStorage.BoxStack.Count >= 1)
                {
                    ToNextStep();
                }
                //else
                //{
                //    step = 0;
                //}
                break;
            case 5: // 박스 옮기기
                targets[5].SetActive(true);
                Debug.Log("Step 6 !");
                
                //ToNextStep();
                break;
            case 6: // 배달 쌓기
                targets[6].SetActive(true);
                Debug.Log("Step 7 !");
                ToNextStep();
                break;
            case 7: // 벌어들인 골드로 강화
                Debug.Log("Step 8 !");

                break;
        }
    }

    private void ToNextStep()
    {
        step++;
    }
}
