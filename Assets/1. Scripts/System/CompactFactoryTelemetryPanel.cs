using System.Collections.Generic;
using System.Linq;
using System.Text;
using Churub.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public sealed class CompactFactoryTelemetryPanel : MonoBehaviour
{
    private const double WindowSeconds = 60d;
    private const double BottleneckRateRatio = .75d;

    private sealed class EmployeeActivity
    {
        public readonly TimedMetricWindow Moving = new TimedMetricWindow(WindowSeconds);
        public readonly TimedMetricWindow Waiting = new TimedMetricWindow(WindowSeconds);
    }

    private readonly TimedMetricWindow produced = new TimedMetricWindow(WindowSeconds);
    private readonly TimedMetricWindow processed = new TimedMetricWindow(WindowSeconds);
    private readonly TimedMetricWindow sold = new TimedMetricWindow(WindowSeconds);
    private readonly TimedMetricWindow gold = new TimedMetricWindow(WindowSeconds);
    private readonly TimedMetricWindow supplyFull = new TimedMetricWindow(WindowSeconds);
    private readonly TimedMetricWindow processStarved = new TimedMetricWindow(WindowSeconds);
    private readonly TimedMetricWindow salesEmpty = new TimedMetricWindow(WindowSeconds);
    private readonly Dictionary<int, EmployeeActivity> employeeActivity =
        new Dictionary<int, EmployeeActivity>();

    private CompactSupplyStation supply;
    private CompactManualStation station;
    private CompactSalesCounter sales;
    private Employee[] employees = System.Array.Empty<Employee>();
    private GameObject panel;
    private TMP_Text body;
    private float refreshAt;
    private float discoverEmployeesAt;
    private double startedAt;

    private void Awake()
    {
        supply = FindFirstObjectByType<CompactSupplyStation>();
        station = FindFirstObjectByType<CompactManualStation>();
        sales = FindFirstObjectByType<CompactSalesCounter>();
        startedAt = Time.unscaledTimeAsDouble;
    }

    private void OnEnable() => CompactTelemetryEvents.MetricRecorded += OnMetricRecorded;

    private void OnDisable() => CompactTelemetryEvents.MetricRecorded -= OnMetricRecorded;

    private void Start()
    {
        if (supply == null || station == null || sales == null)
        {
            Debug.LogWarning("Compact telemetry dependencies are incomplete.", this);
            enabled = false;
            return;
        }
        BuildUi();
        DiscoverEmployees();
        RefreshText(Time.unscaledTimeAsDouble);
    }

    private void Update()
    {
        double now = Time.unscaledTimeAsDouble;
        double delta = Time.unscaledDeltaTime;
        if (supply.StockCount >= supply.StockCapacity) supplyFull.Add(now, delta);
        if (station.InputCount <= 0) processStarved.Add(now, delta);
        if (sales.StockCount <= 0) salesEmpty.Add(now, delta);

        if (Time.unscaledTime >= discoverEmployeesAt)
        {
            DiscoverEmployees();
            discoverEmployeesAt = Time.unscaledTime + 1f;
        }
        foreach (Employee employee in employees)
        {
            if (employee == null) continue;
            int id = employee.GetInstanceID();
            if (!employeeActivity.TryGetValue(id, out EmployeeActivity activity))
            {
                activity = new EmployeeActivity();
                employeeActivity.Add(id, activity);
            }
            (employee.IsMovingForTelemetry ? activity.Moving : activity.Waiting).Add(now, delta);
        }

        if (Time.unscaledTime >= refreshAt)
        {
            RefreshText(now);
            refreshAt = Time.unscaledTime + .25f;
        }
    }

    private void OnMetricRecorded(CompactTelemetryMetric metric, double amount)
    {
        double now = Time.unscaledTimeAsDouble;
        switch (metric)
        {
            case CompactTelemetryMetric.IngredientProduced: produced.Add(now, amount); break;
            case CompactTelemetryMetric.ProductProcessed: processed.Add(now, amount); break;
            case CompactTelemetryMetric.ProductSold: sold.Add(now, amount); break;
            case CompactTelemetryMetric.GoldEarned: gold.Add(now, amount); break;
        }
    }

    private void DiscoverEmployees()
    {
        employees = FindObjectsByType<Employee>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
            .OrderBy(employee => employee.TransportRole).ThenBy(employee => employee.name).ToArray();
        var live = new HashSet<int>(employees.Where(employee => employee != null)
            .Select(employee => employee.GetInstanceID()));
        foreach (int id in employeeActivity.Keys.Where(id => !live.Contains(id)).ToArray())
            employeeActivity.Remove(id);
    }

    private void RefreshText(double now)
    {
        if (body == null) return;
        double observed = System.Math.Max(1d, System.Math.Min(WindowSeconds, now - startedAt));
        var text = new StringBuilder(640);
        text.AppendLine($"<b>최근 60초 처리량</b>  <color=#8FA0B5>관측 {observed:0}s</color>");
        AppendRate(text, "재료 생산", produced.Sum(now), observed);
        AppendRate(text, "가공 완료", processed.Sum(now), observed);
        AppendRate(text, "판매", sold.Sum(now), observed);
        text.AppendLine($"골드 획득  <b>{NumberAbbreviator.Format(gold.Sum(now))}</b>  ·  예상 <b>{NumberAbbreviator.Format(gold.Sum(now) / observed * 60d)}/분</b>");
        text.AppendLine();
        text.AppendLine("<b>현재 재고</b>");
        text.AppendLine($"공급 {supply.StockCount}/{supply.StockCapacity}  ·  가공 입력 {station.InputCount}/{station.InputCapacity}");
        text.AppendLine($"가공 출력 {station.OutputCount}/{station.OutputCapacity}  ·  판매대 {sales.StockCount}/{sales.StockCapacity}");
        text.AppendLine();
        text.AppendLine("<b>최근 정지 시간</b>");
        text.AppendLine($"공급 포화 {supplyFull.Sum(now):0.0}s  ·  가공 재료 부족 {processStarved.Sum(now):0.0}s");
        text.AppendLine($"판매 재고 부족 {salesEmpty.Sum(now):0.0}s");
        text.AppendLine();
        text.AppendLine("<b>직원 이동 / 대기</b>");
        if (employees.Length == 0) text.AppendLine("직원 없음");
        foreach (Employee employee in employees)
        {
            if (employee == null || !employeeActivity.TryGetValue(employee.GetInstanceID(), out EmployeeActivity activity))
                continue;
            double moving = activity.Moving.Sum(now);
            double waiting = activity.Waiting.Sum(now);
            double total = System.Math.Max(.001d, moving + waiting);
            text.AppendLine($"{employee.RoleLabel}  <b>{moving / total * 100d:0}%</b> / {waiting / total * 100d:0}%");
        }
        text.AppendLine();
        text.Append("<b>현재 경고</b>  ").Append(CurrentWarning(now, observed));
        body.text = text.ToString();
    }

    private static void AppendRate(StringBuilder text, string label, double amount, double observed)
    {
        text.AppendLine($"{label}  <b>{amount:0}</b>  ·  {amount / observed:0.00}/초");
    }

    private string CurrentWarning(double now, double observed)
    {
        if (station.OutputCount >= station.OutputCapacity) return "<color=#FFB45C>가공 출력 포화</color>";
        if (sales.StockCount <= 0 && sold.Sum(now) > 0d) return "<color=#FFB45C>판매 재고 부족</color>";
        if (station.InputCount <= 0 && produced.Sum(now) > 0d) return "<color=#FFB45C>가공 재료 부족</color>";
        if (supply.StockCount >= supply.StockCapacity) return "<color=#FFB45C>공급 수령판 포화</color>";
        if (observed >= 10d)
        {
            double productionRate = produced.Sum(now) / observed;
            double processRate = processed.Sum(now) / observed;
            double salesRate = sold.Sum(now) / observed;
            if (productionRate > 0d && processRate < productionRate * BottleneckRateRatio)
                return "<color=#FFB45C>가공 처리량 확인</color>";
            if (processRate > 0d && salesRate < processRate * BottleneckRateRatio)
                return "<color=#FFB45C>판매 처리량 확인</color>";
        }
        return "<color=#77D6B0>뚜렷한 정체 없음</color>";
    }

    private void BuildUi()
    {
        Button toggle = CreateButton("Telemetry Toggle", transform, "계측");
        RectTransform toggleRect = toggle.GetComponent<RectTransform>();
        toggleRect.anchorMin = toggleRect.anchorMax = new Vector2(0f, 1f);
        toggleRect.pivot = new Vector2(0f, 1f);
        toggleRect.anchoredPosition = new Vector2(18f, -18f);
        toggleRect.sizeDelta = new Vector2(116f, 54f);

        panel = UiObject("Compact Factory Telemetry", transform, typeof(Image));
        panel.transform.SetAsLastSibling();
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.anchorMin = panelRect.anchorMax = new Vector2(0f, 1f);
        panelRect.pivot = new Vector2(0f, 1f);
        panelRect.anchoredPosition = new Vector2(18f, -82f);
        panelRect.sizeDelta = new Vector2(520f, 620f);
        panel.GetComponent<Image>().color = new Color(.035f, .055f, .08f, .96f);
        var outline = panel.AddComponent<Outline>();
        outline.effectColor = new Color(.2f, .32f, .46f, 1f);
        outline.effectDistance = new Vector2(2f, -2f);

        TMP_Text title = CreateText("Title", panel.transform, 25f, TextAlignmentOptions.Left);
        title.text = "공장 처리량 계측";
        title.fontStyle = FontStyles.Bold;
        title.rectTransform.anchorMin = new Vector2(.045f, .91f);
        title.rectTransform.anchorMax = new Vector2(.95f, .98f);
        title.rectTransform.offsetMin = title.rectTransform.offsetMax = Vector2.zero;

        body = CreateText("Metrics", panel.transform, 18f, TextAlignmentOptions.TopLeft);
        body.color = new Color(.88f, .92f, .97f, 1f);
        body.lineSpacing = 6f;
        body.rectTransform.anchorMin = new Vector2(.045f, .04f);
        body.rectTransform.anchorMax = new Vector2(.955f, .9f);
        body.rectTransform.offsetMin = body.rectTransform.offsetMax = Vector2.zero;
        toggle.onClick.AddListener(() => panel.SetActive(!panel.activeSelf));
    }

    private static GameObject UiObject(string name, Transform parent, params System.Type[] components)
    {
        var go = new GameObject(name, typeof(RectTransform));
        foreach (System.Type component in components) go.AddComponent(component);
        go.transform.SetParent(parent, false);
        return go;
    }

    private static TMP_Text CreateText(string name, Transform parent, float fontSize,
        TextAlignmentOptions alignment)
    {
        var text = UiObject(name, parent, typeof(TextMeshProUGUI)).GetComponent<TextMeshProUGUI>();
        text.font = TMP_Settings.defaultFontAsset;
        text.fontSize = fontSize;
        text.alignment = alignment;
        text.color = Color.white;
        text.textWrappingMode = TextWrappingModes.Normal;
        text.raycastTarget = false;
        return text;
    }

    private static Button CreateButton(string name, Transform parent, string label)
    {
        var button = UiObject(name, parent, typeof(Image), typeof(Button)).GetComponent<Button>();
        button.image.color = new Color(.11f, .32f, .47f, .96f);
        TMP_Text text = CreateText("Label", button.transform, 20f, TextAlignmentOptions.Center);
        text.text = label;
        text.fontStyle = FontStyles.Bold;
        text.rectTransform.anchorMin = Vector2.zero;
        text.rectTransform.anchorMax = Vector2.one;
        text.rectTransform.offsetMin = text.rectTransform.offsetMax = Vector2.zero;
        return button;
    }
}
