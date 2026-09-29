using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Sirenix.OdinInspector;
using Churub.Core;

public class Guide : MonoBehaviour
{
    private static readonly Color32 GuideCounterColor = new Color32(107, 63, 42, 255);
    private static readonly Color32 GuideCompletedCounterColor = new Color32(217, 121, 95, 255);

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
    private CompactSupplyStation compactSupply;
    private CompactManualStation compactManual;
    private CompactSalesCounter compactSales;
    private bool compactFlow;
    private Button skipButton;
    private Button replayButton;

    private bool _guideDone = false;
    private readonly HashSet<int> invalidTargetWarnings = new HashSet<int>();

    private Button claimButton;

    private void Awake()
    {
        //UIManager.Instance.SetGuideStep(this);
        baseCost = DataManager.Instance.baseCost;
        player = GameManager.Instance.P;
        compactSupply = FindObjectOfType<CompactSupplyStation>();
        compactManual = FindObjectOfType<CompactManualStation>();
        compactSales = FindObjectOfType<CompactSalesCounter>();
        compactFlow = compactSupply != null && compactManual != null && compactSales != null;
        ResolveProductionTargets();
    }

    private void ResolveProductionTargets()
    {
        if (targets == null || targets.Length < 6) return;

        var workPoints = FindObjectsByType<WorkPoint>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        if (compactFlow)
        {
            ResolveCompactTargets(workPoints);
            return;
        }

        for (int index = 0; index < 6; index++)
        {
            GameObject inactiveMatch = null;
            foreach (var workPoint in workPoints)
            {
                if (!MatchesProductionTarget(index, workPoint)) continue;
                if (workPoint.gameObject.activeInHierarchy)
                {
                    targets[index] = workPoint.gameObject;
                    inactiveMatch = null;
                    break;
                }
                if (inactiveMatch == null)
                    inactiveMatch = workPoint.gameObject;
            }
            if (inactiveMatch != null)
                targets[index] = inactiveMatch;
        }
    }

    private void ResolveCompactTargets(WorkPoint[] workPoints)
    {
        var markers = FindObjectsByType<OnboardingTargetMarker>(
            FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var marker in markers)
        {
            int index = CompactTargetIndex(marker.TargetType);
            if (index < 0) continue;
            targets[index] = marker.gameObject;
            if (marker.TargetType == OnboardingTargetType.SalesPallet)
                targets[5] = marker.gameObject;
        }

        foreach (var workPoint in workPoints)
        {
            if (workPoint == null) continue;

            if (workPoint.Action is CompactManualProcessAction process
                && process.Station == compactManual)
            {
                if (targets[2] == null) targets[2] = workPoint.gameObject;
                continue;
            }

            if (!(workPoint.Action is ItemTransfer transfer)) continue;
            if (transfer.Endpoint == compactSupply)
                if (targets[0] == null) targets[0] = workPoint.gameObject;
            else if (transfer.Endpoint == compactManual)
                if (targets[1] == null) targets[1] = workPoint.gameObject;
            else if (transfer.Endpoint is CompactManualOutput output
                && output.Station == compactManual)
                if (targets[3] == null) targets[3] = workPoint.gameObject;
            else if (transfer.Endpoint == compactSales)
            {
                if (targets[4] == null) targets[4] = workPoint.gameObject;
                if (targets[5] == null) targets[5] = workPoint.gameObject;
            }
        }
    }

    private static int CompactTargetIndex(OnboardingTargetType type)
    {
        switch (type)
        {
            case OnboardingTargetType.SupplyPickup: return 0;
            case OnboardingTargetType.ManualInput: return 1;
            case OnboardingTargetType.ManualWork: return 2;
            case OnboardingTargetType.ManualOutput: return 3;
            case OnboardingTargetType.SalesPallet: return 4;
            default: return -1;
        }
    }

    private void OnEnable() => CompactProgressEvents.ActionCompleted += OnCompactActionCompleted;

    private void OnDisable()
    {
        CompactProgressEvents.ActionCompleted -= OnCompactActionCompleted;
        if (guideLine != null) guideLine.DOKill();
    }

    private bool MatchesProductionTarget(int index, WorkPoint workPoint)
    {
        if (workPoint == null) return false;

        if (index == 4)
            return workPoint.Action is PackagingInteraction packaging
                && packaging.Packaging == boxPackaging;

        if (!(workPoint.Action is ItemTransfer transfer)) return false;
        switch (index)
        {
            case 0:
                return transfer.Endpoint is IngredientMaker
                    && !IsUnderAny(workPoint.transform, _ContainerObjects);
            case 1:
                return transfer.Endpoint is ConveyorBelt
                    && !IsUnderAny(workPoint.transform, _MachineObjects);
            case 2:
                return transfer.Endpoint is BoxStorage churuStorage
                    && churuStorage.bsType == BoxStorageType.ChuruStorage
                    && !IsUnderAny(workPoint.transform, _MachineObjects);
            case 3:
                return transfer.Endpoint == boxPackaging;
            case 5:
                return transfer.Endpoint == boxStorage;
            default:
                return false;
        }
    }

    private static bool IsUnderAny(Transform child, GameObject[] roots)
    {
        if (roots == null) return false;
        foreach (var root in roots)
            if (root != null && child.IsChildOf(root.transform))
                return true;
        return false;
    }

    void Start()
    {
        BalanceTable.Synchronize(baseCost);

        guideButton.onClick.AddListener(GuideButton);

        SetTargetsActive(false);
        CreateGuidePrefab();
        SetWorkPoint();

        if (!compactFlow && baseCost.guideStep < 5 && truck != null)
            truck.gameObject.SetActive(false);

        if (!_guideDone)
        {
            GuideLine();
        }

        CreateClaimButton();
        if (compactFlow) CreateOnboardingControls();
    }

    void Update()
    {
        if (!_guideDone)
        {
            if (compactFlow && !baseCost.onboardingCompleted) CompactGuideStep();
            else if (!compactFlow && baseCost.guideStep <= 6) GuideStep();
            else ShowExpansionGoals();
        }
    }

    private void CreateClaimButton()
    {
        var go = new GameObject("First Employee Reward", typeof(RectTransform), typeof(Image), typeof(Button));
        go.transform.SetParent(guideUI.transform, false);
        var rect = (RectTransform)go.transform;
        rect.anchorMin = rect.anchorMax = new Vector2(.5f, 0f);
        rect.pivot = new Vector2(.5f, 1f);
        rect.anchoredPosition = new Vector2(0, -12);
        rect.sizeDelta = new Vector2(360, 64);
        go.GetComponent<Image>().color = new Color(.2f, .45f, .2f);
        claimButton = go.GetComponent<Button>();
        var label = new GameObject("Label", typeof(RectTransform), typeof(TextMeshProUGUI));
        label.transform.SetParent(go.transform, false);
        var text = label.GetComponent<TextMeshProUGUI>();
        text.font = guideText.font;
        text.text = "첫 직원 맞이하기 (무료)";
        text.fontSize = 24;
        text.alignment = TextAlignmentOptions.Center;
        text.raycastTarget = false;
        var labelRect = (RectTransform)label.transform;
        labelRect.anchorMin = Vector2.zero;
        labelRect.anchorMax = Vector2.one;
        labelRect.offsetMin = labelRect.offsetMax = Vector2.zero;
        claimButton.onClick.AddListener(() => {
            if (UIManager.Instance.ClaimFirstEmployee()) ShowExpansionGoals();
        });
        go.SetActive(false);
    }

    private void CreateOnboardingControls()
    {
        skipButton = CreateGuideControl("Skip Onboarding", "건너뛰기", new Vector2(115, -84));
        replayButton = CreateGuideControl("Replay Onboarding", "처음 안내 다시 보기", new Vector2(0, -84));
        skipButton.onClick.AddListener(SkipOnboarding);
        replayButton.onClick.AddListener(RestartOnboarding);
        RefreshOnboardingControls();
    }

    private Button CreateGuideControl(string name, string labelText, Vector2 position)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button));
        go.transform.SetParent(guideUI.transform, false);
        var rect = (RectTransform)go.transform;
        rect.anchorMin = rect.anchorMax = new Vector2(.5f, 0f);
        rect.pivot = new Vector2(.5f, 1f);
        rect.anchoredPosition = position;
        rect.sizeDelta = new Vector2(220, 54);
        go.GetComponent<Image>().color = new Color(.16f, .16f, .16f, .88f);
        var label = new GameObject("Label", typeof(RectTransform), typeof(TextMeshProUGUI));
        label.transform.SetParent(go.transform, false);
        var text = label.GetComponent<TextMeshProUGUI>();
        text.font = guideText.font;
        text.text = labelText;
        text.fontSize = 18;
        text.alignment = TextAlignmentOptions.Center;
        text.raycastTarget = false;
        var labelRect = (RectTransform)label.transform;
        labelRect.anchorMin = Vector2.zero;
        labelRect.anchorMax = Vector2.one;
        labelRect.offsetMin = labelRect.offsetMax = Vector2.zero;
        return go.GetComponent<Button>();
    }

    public void SkipOnboarding()
    {
        if (!compactFlow || baseCost.onboardingCompleted) return;
        IncrementalProgress.CompleteOnboarding(baseCost);
        baseCost.guideStep = Mathf.Max(baseCost.guideStep, 7);
        SaveProgress();
        RefreshOnboardingControls();
        ShowExpansionGoals();
    }

    public void RestartOnboarding()
    {
        if (!compactFlow) return;
        IncrementalProgress.RestartOnboarding(baseCost);
        _guideDone = false;
        guideUI.SetActive(true);
        isGuideActive = true;
        RefreshOnboardingControls();
        CompactGuideStep();
        GuideLine();
        SaveProgress();
    }

    private void RefreshOnboardingControls()
    {
        if (skipButton != null) skipButton.gameObject.SetActive(!baseCost.onboardingCompleted);
        if (replayButton != null) replayButton.gameObject.SetActive(baseCost.onboardingCompleted);
    }

    private void OnCompactActionCompleted(CompactProgressAction action)
    {
        if (!compactFlow || baseCost == null || baseCost.onboardingCompleted) return;
        CompactProgressAction expected;
        switch (baseCost.onboardingStep)
        {
            case 0: expected = CompactProgressAction.IngredientCollected; break;
            case 1: expected = CompactProgressAction.IngredientPlaced; break;
            case 2: expected = CompactProgressAction.ProductCompleted; break;
            case 3: expected = CompactProgressAction.ProductCollected; break;
            case 4: expected = CompactProgressAction.ProductStocked; break;
            case 5: expected = CompactProgressAction.ProductSold; break;
            default: return;
        }
        if (action != expected) return;

        GiveReward(baseCost.onboardingStep);
        baseCost.onboardingStep++;
        if (baseCost.onboardingStep >= IncrementalProgress.OnboardingStepCount)
        {
            IncrementalProgress.CompleteOnboarding(baseCost);
            baseCost.guideStep = Mathf.Max(baseCost.guideStep, 7);
            GiveReward(6);
        }
        SaveProgress();
        CreateGuidePrefab();
        RefreshOnboardingControls();
        if (baseCost.onboardingCompleted) ShowExpansionGoals();
        else
        {
            CompactGuideStep();
            GuideLine();
        }
    }

    private static void SaveProgress()
    {
        if (DataManager.Instance != null) DataManager.Instance.GameDataUpdate();
    }

    private void ShowExpansionGoals()
    {
        bool claim = BalanceTable.CanClaimEmployee(baseCost);
        if (claimButton != null) claimButton.gameObject.SetActive(claim);
        if (claim)
        {
            RefreshExpansionTargets(null);
            UpdateGuide("첫 직원이 도착했어요", "무료 직원을 받고 원재료 운반을 맡겨보세요.", "", false);
            guideLine.gameObject.SetActive(false);
            return;
        }

        string nextFacility = BalanceTable.NextFacility(baseCost);
        RefreshExpansionTargets(nextFacility);
        guideLine.gameObject.SetActive(false);
        if (nextFacility == null)
        {
            if (compactFlow)
            {
                UpdateGuide("다음 목표", "원하는 병목을 골라 공장을 확장하세요.", "", false);
                RefreshOnboardingControls();
            }
            else _GuideDone();
            return;
        }

        string facilityName = FacilityName(nextFacility);
        UpdateGuide("다음 목표", facilityName + " 건설하기",
            BalanceTable.FacilityCost(nextFacility) + " 골드", false);
    }

    private void RefreshExpansionTargets(string nextFacility)
    {
        for (int i = 7; i < targets.Length; i++)
        {
            if (!TryGetTarget(i, out var target)) continue;

            UnlockManager unlock = target.GetComponent<UnlockManager>();
            if (unlock != null)
            {
                bool isNext = unlock.Type.ToString() == nextFacility;
                target.SetActive(isNext && !unlock.IsPurchased);
                continue;
            }

            // The office interaction point is not a facility unlock pad.
            target.SetActive(baseCost.IsUnlocked(GameDataSchema.Progress.Office));
        }
    }

    private static string FacilityName(string key)
    {
        switch (key)
        {
            case "Office": return "사무실";
            case "Container1": return "추가 컨테이너 1";
            case "Machine1": return "추가 컨베이어 벨트 1";
            case "Container2": return "추가 컨테이너 2";
            case "Machine2": return "추가 컨베이어 벨트 2";
            case "Stall": return "노점";
            case "Store": return "상점";
            default: return key;
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
        int currentStep = compactFlow ? baseCost.onboardingStep : baseCost.guideStep;
        if (currentStep < targets.Length)
        {
            if (!TryGetTarget(currentStep, out var targetObject)) return;
            Transform target = targetObject.transform;
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
        if (compactFlow) return;
        baseCost.guideStep++;
        GuideLine();

    }

    private void UpdateGuide(string title, string text, string numberText, bool isCompleted)
    {
        if (guideTitle != null && guideText != null && guideTextNum != null)
        {
            guideTitle.text = title;
            guideText.text = text;
            guideTextNum.color = isCompleted ? GuideCompletedCounterColor : GuideCounterColor;
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
        int currentStep = compactFlow ? baseCost.onboardingStep : baseCost.guideStep;
        for (int i = 0; i <= currentStep && i < targets.Length; i++)
        {
            if (!TryGetTarget(i, out var target)) continue;
            if (target.GetComponent<WorkPoint>())
                target.SetActive(true);
        }
    }

    private void SetTargetsActive(bool isActive)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (!TryGetTarget(i, out var target)) continue;
            target.SetActive(isActive);
        }
    }

    private void SetActiveTarget(int index)
    {
        if (!TryGetTarget(index, out var target)) return;
        var unlock = target.GetComponent<UnlockManager>();
        if (unlock == null || !unlock.IsPurchased) target.SetActive(true);
    }

    private bool TryGetTarget(int index, out GameObject target)
    {
        target = null;
        if (targets != null && index >= 0 && index < targets.Length)
            target = targets[index];
        if (target != null) return true;

        if (invalidTargetWarnings.Add(index))
            Debug.LogError($"Guide target at index {index} is missing or was destroyed. Check the Game scene reference.", this);
        return false;
    }

    private void GiveReward(int step)
    {
        int reward = BalanceTable.TutorialReward(step);

        if (reward > 0)
        {
            CompactTelemetryEvents.Record(CompactTelemetryMetric.GoldEarned, reward);
            player.Gold += reward;
            UIManager.Instance.UpdateGoldUI();
        }
    }

    #region GuideSteps
    private void CompactGuideStep()
    {
        switch (baseCost.onboardingStep)
        {
            case 0:
                SetActiveTarget(0);
                UpdateGuide("첫 생산 1", "컨테이너 앞에서 연어를 가져오세요", "", false);
                break;
            case 1:
                SetActiveTarget(1);
                UpdateGuide("첫 생산 2", "연어를 작업대 입력판에 놓으세요", "", false);
                break;
            case 2:
                SetActiveTarget(2);
                UpdateGuide("첫 생산 3", "가운데 발판에서 가공을 완료하세요", "", false);
                break;
            case 3:
                SetActiveTarget(3);
                UpdateGuide("첫 생산 4", "완성된 츄릅을 가져오세요", "", false);
                break;
            case 4:
                SetActiveTarget(4);
                UpdateGuide("첫 판매 1", "츄릅을 판매 파레트에 진열하세요", "", false);
                break;
            case 5:
                SetActiveTarget(5);
                UpdateGuide("첫 판매 완료", "손님이 구매해 첫 수익을 낼 때까지 기다리세요", "", false);
                break;
        }
    }

    private void _Step0()
    {
        SetActiveTarget(0);
        UpdateGuide("공장냥의 첫걸음 1", "원재료 창고로 이동", ""
            , player.Inventory.ContainsType(ItemType.Ingredient));
    }
    private void _Step1()
    {
        SetActiveTarget(1);
        UpdateGuide("공장냥의 첫걸음 2", "원재료를 컨베이어 벨트로 옮기기", ""
            , !player.Inventory.ContainsType(ItemType.Ingredient));
    }
    private void _Step2()
    {
        SetActiveTarget(2);
        UpdateGuide("공장냥의 첫걸음 3", "완성된 츄릅을 박스 포장대로 옮기기", ""
            , player.Inventory.ContainsType(ItemType.Churu));
    }
    private void _Step3()
    {
        SetActiveTarget(3);
        UpdateGuide("공장냥의 첫걸음 4", "츄룹 창고 이동 작업", boxPackaging.WaitingCount.ToString() + " / 5"
            , !player.Inventory.ContainsType(ItemType.Churu) && boxPackaging.WaitingCount >= 5);
    }
    private void _Step4()
    {
        SetActiveTarget(4);
        UpdateGuide("공장냥의 첫걸음 5", "박스 포장대에서 박스 포장하기", ""
            , boxStorage.bsType == BoxStorageType.BoxStorage && boxStorage.Count >= 1);
    }
    private void _Step5()
    {
        truck.gameObject.SetActive(true);
        SetActiveTarget(5);
        UpdateGuide("공장냥의 첫걸음 6", "츄릅박스를 트럭에 싣기", ""
            , player.Inventory.ContainsType(ItemType.Box));
    }
    private void _Step6()
    {
        SetActiveTarget(6);
        UpdateGuide("공장냥의 첫걸음 fin", "츄릅박스 5개를 트럭에 실어 판매하기", truck.LoadedCount.ToString() + " / 5"
            , baseCost.IsUnlocked(BalanceTable.FirstSaleKey));
    }
    private void _GuideDone()
    {
        _guideDone = true;
        gameObject.SetActive(false);
        guideButton.gameObject.SetActive(false);
        guideUI.gameObject.SetActive(false);
    }
    #endregion

}
