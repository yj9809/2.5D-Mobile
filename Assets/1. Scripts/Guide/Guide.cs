using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Sirenix.OdinInspector;

public class Guide : MonoBehaviour
{
    [Title("Guide")]
    [SerializeField] private GameObject guidePrefab;
    private GameObject curGuidePrefab;
    private TextMeshProUGUI guideTitle;
    private TextMeshProUGUI guideText;
    private TextMeshProUGUI guideTextNum;
    [SerializeField] private Sprite guideClearImage;

    private bool isGuideActive = true;
    [SerializeField] private Button guideButton;
    [SerializeField] private GameObject guideUI;
    [SerializeField] private RectTransform guideLine;
    private bool isGuideLineMoving = false;
    private float guideLineOriginalY;

    [Title("Target")]
    [SerializeField] private GameObject[] targets;

    [Title("Object")]
    [SerializeField] private GameObject _OfficeObject;
    [SerializeField] private GameObject[] _ContainerObjects;
    [SerializeField] private GameObject[] _MachineObjects;
    [SerializeField] private GameObject _StallObject;
    [SerializeField] private GameObject _StoreObject;

    [Title("Employee")]
    [SerializeField] private Button employeeAddButton;

    [Title("Scripts")]
    public bool _Scripts = true;
    [HideIfGroup("_Scripts"), SerializeField] private BoxPackaging boxPackaging;
    [HideIfGroup("_Scripts"), SerializeField] private BoxStorage boxStorage;
    [HideIfGroup("_Scripts"), SerializeField] private Truck truck;
    [HideIfGroup("_Scripts"), SerializeField] private InterstitialAdExample adExample;
    private BaseCost baseCost;
    private Player player;

    private bool _guideDone = false;

    private bool _ShowAd = false;

    private void Awake()
    {
        //UIManager.Instance.SetGuideStep(this);
        baseCost = DataManager.Instance.baseCost;
        player = GameManager.Instance.P;
    }

    void Start()
    {
        if (baseCost.guideStep > 14)
            return;

        guideButton.onClick.AddListener(GuideButton);

        SetTargetsActive(false);
        CreateGuidePrefab();
        SetWorkPoint();

        if (baseCost.guideStep < 5)
            truck.gameObject.SetActive(false);

        if (!_guideDone)
        {
            GuideLine();
        }

        EmployeeActive();
    }

    void Update()
    {
        if (!_guideDone)
        {
            GuideStep();
        }

        // ����
        if (!_ShowAd)
        {
            if (baseCost.guideStep == 6 && truck.BoxStack.Count == 1)
            {
                Ads();
                _ShowAd = true;
            }
            else if (baseCost.guideStep == 10)
            {
                Ads();
                _ShowAd = true;
            }
            else if (_guideDone)
            {
                Ads();
                _ShowAd = true;
            }
        }
    }

    private void Ads()
    {
        adExample.ShowAd();
    }

    private void GuideButton()
    {
        isGuideActive = !isGuideActive;
        guideUI.SetActive(isGuideActive);
    }

    private void GuideLine()
    {
        #region �÷��̾� �߽� ȭ��ǥ (�ּ�ó��)
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

        #region Ÿ�� ��ġ ȭ��ǥ
        if (baseCost.guideStep < targets.Length)
        {
            Transform target = targets[baseCost.guideStep].transform;
            Vector3 targetPosition = new Vector3(target.position.x, guideLine.position.y, target.position.z);

            guideLine.DOMove(targetPosition, 1f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                if (!isGuideLineMoving)
                {
                    guideLineOriginalY = guideLine.localPosition.y;
                    isGuideLineMoving = true;
                    GuideLineMoveMent();
                }
            });
        }
        #endregion
    }

    private void GuideLineMoveMent()
    {
        guideLine.DOLocalMoveY(guideLineOriginalY + 0.35f, 0.5f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                guideLine.DOLocalMoveY(guideLineOriginalY - 0.35f, 0.5f)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() =>
                    {
                        isGuideLineMoving = false;
                        GuideLineMoveMent();
                    });
            });
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
            case 14: _Step14(); break;
            case 15: _GuideDone(); break;
        }
    }

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

        guideTitle = curGuidePrefab.transform.Find("Guide_Text_Title (TMP)").GetComponent<TextMeshProUGUI>();
        guideText = curGuidePrefab.transform.Find("Guide_Text (TMP)").GetComponent<TextMeshProUGUI>();
        guideTextNum = curGuidePrefab.transform.Find("Guide_Text_Num (TMP)").GetComponent<TextMeshProUGUI>();
    }

    public void ToNextStep()
    {
        baseCost.guideStep++;
        GuideLine();
        _ShowAd = false;
    }

    private void UpdateGuide(string title, string text, string numberText, bool isCompleted)
    {
        if (guideTitle != null && guideText != null && guideTextNum != null)
        {
            guideTitle.text = title;
            guideText.text = text;
            guideTextNum.color = isCompleted ? Color.yellow : Color.black;
            guideTextNum.text = numberText;
        }

        if (isCompleted)
        {
            CreateGuidePrefab();
            GiveReward(baseCost.guideStep);
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

    private void GiveReward(int step)
    {
        int reward = 0;

        switch (step)
        {
            case 0: reward = 100; break;
            case 1: reward = 200; break;
            case 2: reward = 200; break;
            case 3: reward = 300; break;
            case 4: reward = 300; break;
            case 5: reward = 500; break;
            case 6: reward = 500; break;
            case 7: reward = 1000; break;
            case 8: reward = 1000; break;
            case 9: reward = 1000; break;
            case 10: reward = 1000; break;
            case 11: reward = 1000; break;
            case 12: reward = 1000; break;
            case 13: reward = 1000; break;
            case 14: reward = 1000; break;
        }

        if (reward > 0)
        {
            player.Gold += reward;
            UIManager.Instance.UpdateGoldUI();
        }
    }

    #region GuideSteps
    private void _Step0()
    {
        SetActiveTarget(0);
        UpdateGuide("������� ù���� 1", "����� â��� �̵�", ""
            , player.IngredientStack.Count > 0);
    }
    private void _Step1()
    {
        SetActiveTarget(1);
        UpdateGuide("������� ù���� 2", "����Ḧ �����̾� ��Ʈ�� �ű��", ""
            , player.IngredientStack.Count <= 0);
    }
    private void _Step2()
    {
        SetActiveTarget(2);
        UpdateGuide("������� ù���� 3", "�ϼ��� ���� �ڽ� ������ �ű��", ""
            , player.ChuruStack.Count > 0);
    }
    private void _Step3()
    {
        SetActiveTarget(3);
        UpdateGuide("������� ù���� 4", "��� â�� �̵� �۾�", boxPackaging.ChuruStorage.Count.ToString() + " / 5"
            , player.ChuruStack.Count <= 0 && boxPackaging.ChuruStorage.Count >= 5);
    }
    private void _Step4()
    {
        SetActiveTarget(4);
        UpdateGuide("������� ù���� 5", "�ڽ� ����뿡�� �ڽ� �����ϱ�", ""
            , boxStorage.bsType == BoxStorageType.BoxStorage && boxStorage.BoxStack.Count >= 1);
    }
    private void _Step5()
    {
        truck.gameObject.SetActive(true);
        SetActiveTarget(5);
        UpdateGuide("������� ù���� 6", "�򸨹ڽ��� Ʈ���� �Ʊ�", ""
            , player.BoxStack.Count > 0);
    }
    private void _Step6()
    {
        SetActiveTarget(6);
        UpdateGuide("������� ù���� fin", "�򸨹ڽ� 5���� Ʈ���� �Ǿ� �Ǹ��ϱ�", truck.BoxStack.Count.ToString() + " / 5"
            , truck.BoxStack.Count >= 5);
    }
    private void _Step7()
    {
        SetActiveTarget(7);
        UpdateGuide("���� Ȯ�� 1", "�����̾� ��Ʈ �߰� �Ǽ��ϱ�", baseCost.playerData["gold"].ToString() + " / 5000"
            , _MachineObjects[0].activeSelf); 
    }
    private void _Step8()
    {
        SetActiveTarget(8);
        UpdateGuide("���忣 �繫���� �ʿ�����", "�繫�� �Ǽ��ϱ�", baseCost.playerData["gold"].ToString() + " / 2500"
            , _OfficeObject.activeSelf);
    }
    private void _Step9()
    {
        SetActiveTarget(9);
        UpdateGuide("���� ȥ���ϱ� �����", "�繫�ǿ��� ���� ����ϱ�", baseCost.playerData["gold"].ToString() + " / 5000"
            , baseCost.upgradeCosts["baseEmployeeAddCount"] > 0);
        employeeAddButton.interactable = true;
    }
    private void _Step10()
    {
        SetActiveTarget(10);
        UpdateGuide("���� Ȯ�� 2", "����� ���״Ͼ� �߰� �Ǽ��ϱ�", baseCost.playerData["gold"].ToString() + " / 1000"
            , _ContainerObjects[0].activeSelf);

        EmployeeActive();
    }
    private void _Step11()
    {
        SetActiveTarget(11);
        UpdateGuide("���� Ȯ�� 3", "�����̾� ��Ʈ �߰� �Ǽ��ϱ�", baseCost.playerData["gold"].ToString() + " / 10000"
            , _MachineObjects[1].activeSelf);
    }
    private void _Step12()
    {
        SetActiveTarget(12);
        UpdateGuide("���� Ȯ�� 4", "����� ���״Ͼ� �߰� �Ǽ��ϱ�", baseCost.playerData["gold"].ToString() + " / 5000"
            , _ContainerObjects[1].activeSelf);
    }
    private void _Step13()
    {
        SetActiveTarget(13);
        UpdateGuide("�� �ø����� ���� !", "���� ��ġ�ϱ�", baseCost.playerData["gold"].ToString() + " / 10000"
            , _StallObject.activeSelf);
    }
    private void _Step14()
    {
        SetActiveTarget(14);
        UpdateGuide("�� ����� ���� !", "���� ���׷��̵� �ϱ�", baseCost.playerData["gold"].ToString() + " / 20000"
            , _StoreObject.activeSelf);
    }
    private void _GuideDone()
    {
        _guideDone = true;
        gameObject.SetActive(false);
        guideButton.gameObject.SetActive(false);
        guideUI.gameObject.SetActive(false);
    }
    #endregion

    private void EmployeeActive()
    {
        employeeAddButton.interactable = _ContainerObjects[0].activeSelf && _MachineObjects[0].activeSelf;
    }
}
