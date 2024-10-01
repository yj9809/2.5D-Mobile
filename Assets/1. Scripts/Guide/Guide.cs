using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Sirenix.OdinInspector;

public class Guide : MonoBehaviour
{
    [SerializeField] private GameObject guidePrefab;
    private GameObject curGuidePrefab;
    private TextMeshProUGUI guideText;
    private TextMeshProUGUI guideTextNum;
    [SerializeField] private Sprite guideClearImage;

    private bool isGuideActive = true;
    [SerializeField] private Button guideButton;
    [SerializeField] private GameObject guideUI;
    [SerializeField] private RectTransform guideLine;

    [Title("Target")]
    [SerializeField] private GameObject[] targets;

    [Title("Object")]
    [SerializeField] private GameObject _OfficeObject;
    [SerializeField] private GameObject[] _ContainerObjects;
    [SerializeField] private GameObject[] _MachineObjects;
    [SerializeField] private GameObject _StoreObject;

    private BaseCost baseCost;
    private BoxPackaging boxPackaging;
    private BoxStorage boxStorage;
    private Truck truck;
    private Player player;

    void Start()
    {
        baseCost = DataManager.Instance.baseCost;
        player = GameManager.Instance.P;

        guideButton.onClick.AddListener(GuideButton);

        boxPackaging = FindObjectOfType<BoxPackaging>();
        boxStorage = GameObject.Find("BoxStorage").GetComponent<BoxStorage>();
        truck = GameObject.Find("Truck").GetComponent<Truck>();

        SetTargetsActive(false);
        CreateGuidePrefab();
        SetWorkPoint();

        if (baseCost.guideStep < 5)
            truck.gameObject.SetActive(false);
    }

    void Update()
    {
        GuideLine();
        GuideStep();

        if (Input.GetKeyDown(KeyCode.F3))
        {
            UpdateGuide("Pass", "0", true);
        }
    }

    private void GuideButton()
    {
        isGuideActive = !isGuideActive;
        guideUI.SetActive(isGuideActive);
    }

    private void GuideLine()
    {
        #region 플레이어 중심 화살표 (주석처리)
        /*if (step < targets.Length)
        {
            Transform target = targets[step].transform;
            Vector3 direction = target.position - player.position;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            arrow.rotation = lookRotation * Quaternion.Euler(90, 0, 0);

            float distance = direction.magnitude;
            float guideScaleFactor = Mathf.Clamp(distance / 10f, 0.3f, 1f);
            canvas.localScale = new Vector3(guideScaleFactor, guideScaleFactor, guideScaleFactor);

            arrow.localPosition = Vector3.zero;
        }*/
        #endregion

        #region 타겟 위치 화살표
        if (baseCost.guideStep < targets.Length)
        {
            Transform target = targets[baseCost.guideStep].transform;
            Vector3 targetPosition = new Vector3(target.position.x, guideLine.position.y, target.position.z);

            guideLine.DOMove(targetPosition, 1f).SetEase(Ease.OutSine);
        }
        #endregion
    }

    private void GuideStep()
    {
        switch (baseCost.guideStep)
        {
            case 0: _Step0(); break;
            case 1: _Step1(); break;
            case 2: _Step2(); break;
            case 3: _Step3(); break;
            case 4: _Step4(); break;
            case 5: _Step5(); break;
            case 6: _Step6(); break;
            case 7: _Step7(); break;
            case 8: _Step8(); break;
            case 9: _Step9(); break;
            case 10: _Step10(); break;
            case 11: _Step11(); break;
            case 12: _Step12(); break;
            case 13: _Step13(); break;
        }
    }

    #region GuideSteps
    private void _Step0()
    {
        SetActiveTarget(0);
        UpdateGuide("재료 보관소로 이동 하자 !", ""
            , player.IngredientStack.Count > 0);
    }

    private void _Step1()
    {
        SetActiveTarget(1);
        UpdateGuide("재료를\n컨베이어 벨트로 옮기자 !", ""
            , player.IngredientStack.Count <= 0);
    }

    private void _Step2()
    {
        SetActiveTarget(2);
        UpdateGuide("완성된 츄룹을\n포장작업대 창고로 옮기자 !", ""
            , player.ChuruStack.Count > 0);
    }

    private void _Step3()
    {
        SetActiveTarget(3);
        UpdateGuide($"츄룹 창고 이동 작업\n", boxPackaging.ChuruStorage.Count.ToString() + " / 5"
            , player.ChuruStack.Count <= 0 && boxPackaging.ChuruStorage.Count >= 5);
    }

    private void _Step4()
    {
        SetActiveTarget(4);
        UpdateGuide("포장작업대에서\n박스포장을 진행하자 !", ""
            , boxStorage.bsType == BoxStorageType.BoxStorage && boxStorage.BoxStack.Count >= 1);
    }

    private void _Step5()
    {
        truck.gameObject.SetActive(true);
        SetActiveTarget(5);
        UpdateGuide("완성한 박스를\n트럭에 싣자 !", ""
            , player.BoxStack.Count > 0);
    }

    private void _Step6()
    {
        SetActiveTarget(6);
        UpdateGuide($"박스 트럭 상차 작업\n", truck.BoxStack.Count.ToString() + " / 5"
            , truck.BoxStack.Count >= 5);
    }
    private void _Step7()
    {
        SetActiveTarget(7);
        UpdateGuide($"지역 해금 : 추가 재료 창고\n", baseCost.playerGold.ToString() + " / 100"
            , _ContainerObjects[0].activeSelf);
    }

    private void _Step8()
    {
        SetActiveTarget(8);
        UpdateGuide($"지역 해금 : 사무실\n", baseCost.playerGold.ToString() + " / 100"
            , _OfficeObject.activeSelf);
    }

    private void _Step9()
    {
        SetActiveTarget(9);
        UpdateGuide($"사무실 : 직원 고용\n", baseCost.playerGold.ToString() + " / 100"
            , baseCost.baseEmployeeAddCount > 0);
    }

    private void _Step10()
    {
        SetActiveTarget(10);
        UpdateGuide($"지역 해금 : 추가 컨베이어 벨트\n", baseCost.playerGold.ToString() + " / 100"
            , _MachineObjects[0].activeSelf);
    }
    private void _Step11()
    {
        SetActiveTarget(11);
        UpdateGuide($"지역 해금 : 추가 재료 창고\n", baseCost.playerGold.ToString() + " / 100"
            , _ContainerObjects[1].activeSelf);
    }
    private void _Step12()
    {
        SetActiveTarget(12);
        UpdateGuide($"지역 해금 : 추가 컨베이어 벨트\n", baseCost.playerGold.ToString() + " / 100"
            , _MachineObjects[1].activeSelf);
    }

    private void _Step13()
    {
        SetActiveTarget(13);
        UpdateGuide($"지역 해금 : 상점\n", baseCost.playerGold.ToString() + " / 100"
            , _StoreObject.activeSelf);
    }
    #endregion

    private void CreateGuidePrefab()
    {
        if (curGuidePrefab != null)
        {
            if (guideClearImage != null)
            {
                curGuidePrefab.GetComponent<Image>().sprite = guideClearImage;
            }
            Destroy(curGuidePrefab, 2f);
        }

        curGuidePrefab = Instantiate(guidePrefab, guideUI.transform);

        guideText = curGuidePrefab.GetComponentInChildren<TextMeshProUGUI>();
        guideTextNum = curGuidePrefab.transform.Find("Guide_Text_Num (TMP)").GetComponent<TextMeshProUGUI>();
    }

    private void ToNextStep()
    {
        baseCost.guideStep++;
    }

    private void UpdateGuide(string text, string numberText, bool isCompleted)
    {
        if (guideText != null && guideTextNum != null)
        {
            guideText.text = text;

            guideTextNum.color = isCompleted ? Color.yellow : Color.black;
            guideTextNum.text = numberText;
        }

        if (isCompleted)
        {
            CreateGuidePrefab();
            ToNextStep();
        }
    }

    private void SetWorkPoint()
    {
        for (int i = 0; i <= baseCost.guideStep; i++)
        {
            if (targets[i].GetComponent<WorkPoint>())
                targets[i].SetActive(true);
        }
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
