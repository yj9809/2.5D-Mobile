using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Churub.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public sealed class UpgradeGraphView : MonoBehaviour, IDragHandler, IScrollHandler
{
    private readonly struct NodeIconSpec
    {
        public NodeIconSpec(string resourcePath, Rect uv)
        {
            ResourcePath = resourcePath;
            Uv = uv;
        }

        public string ResourcePath { get; }
        public Rect Uv { get; }
    }

    private static Sprite circleMaskSprite;
    private UIManager owner;
    private UpgradeGraphService service;
    private GameObject panel;
    private RectTransform safeAreaRoot;
    private RectTransform viewport;
    private RectTransform content;
    private TMP_Text details;
    private TMP_Text graphStatus;
    private Button purchaseButton;
    private string selectedId;
    private string discoveryMessage;
    private float scale = 1f;
    private float previousPinchDistance;
    private Rect lastSafeArea;
    private int lastScreenWidth;
    private int lastScreenHeight;
    private readonly Dictionary<string, Button> nodes = new Dictionary<string, Button>();
    private readonly List<Image> connections = new List<Image>();

    public bool IsReady { get; private set; }

    public void Initialize(UIManager manager, UpgradeGraphService graphService)
    {
        owner = manager;
        service = graphService;
        if (panel == null) Build();
        IsReady = service != null && service.Definitions.Any();
        Refresh();
        Hide();
    }

    public void Show()
    {
        if (!IsReady) return;
        panel.SetActive(true);
        ApplySafeArea();
        Refresh();
    }

    public void Hide()
    {
        if (panel != null) panel.SetActive(false);
    }

    private void Update()
    {
        if (panel == null || !panel.activeSelf) return;
        if (lastScreenWidth != Screen.width || lastScreenHeight != Screen.height ||
            lastSafeArea != Screen.safeArea)
            ApplySafeArea();
        if (Input.touchCount != 2)
        {
            previousPinchDistance = 0f;
            return;
        }
        float distance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        if (previousPinchDistance > 0f)
            SetScale(scale + (distance - previousPinchDistance) * .0025f);
        previousPinchDistance = distance;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (content == null) return;
        content.anchoredPosition += eventData.delta;
    }

    public void OnScroll(PointerEventData eventData) => SetScale(scale + eventData.scrollDelta.y * .08f);

    private void SetScale(float value)
    {
        scale = Mathf.Clamp(value, .65f, 1.5f);
        content.localScale = Vector3.one * scale;
    }

    private void Build()
    {
        panel = UiObject("Incremental Upgrade Graph", transform, typeof(Image));
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = panelRect.offsetMax = Vector2.zero;
        panel.GetComponent<Image>().color = Hex("0A0E14", .985f);

        safeAreaRoot = UiObject("Safe Area", panel.transform).GetComponent<RectTransform>();
        safeAreaRoot.anchorMin = Vector2.zero;
        safeAreaRoot.anchorMax = Vector2.one;
        safeAreaRoot.offsetMin = safeAreaRoot.offsetMax = Vector2.zero;

        GameObject header = UiObject("Header", safeAreaRoot, typeof(Image));
        RectTransform headerRect = header.GetComponent<RectTransform>();
        headerRect.anchorMin = new Vector2(.035f, .875f);
        headerRect.anchorMax = new Vector2(.965f, .975f);
        headerRect.offsetMin = headerRect.offsetMax = Vector2.zero;
        header.GetComponent<Image>().color = Hex("141B25");
        AddOutline(header, Hex("293446"), 1.5f);

        TMP_Text title = CreateText("Title", header.transform, 29, TextAlignmentOptions.Left);
        title.text = "공장 성장";
        title.fontStyle = FontStyles.Bold;
        title.rectTransform.anchorMin = new Vector2(.04f, .47f);
        title.rectTransform.anchorMax = new Vector2(.55f, .92f);
        title.rectTransform.offsetMin = title.rectTransform.offsetMax = Vector2.zero;

        TMP_Text subtitle = CreateText("Subtitle", header.transform, 20, TextAlignmentOptions.Left);
        subtitle.text = "효율을 높이고 자동화를 연결하세요";
        subtitle.color = Hex("96A2B4");
        subtitle.rectTransform.anchorMin = new Vector2(.04f, .08f);
        subtitle.rectTransform.anchorMax = new Vector2(.64f, .48f);
        subtitle.rectTransform.offsetMin = subtitle.rectTransform.offsetMax = Vector2.zero;

        graphStatus = CreateText("Graph Status", header.transform, 20, TextAlignmentOptions.Right);
        graphStatus.color = Hex("73D6B1");
        graphStatus.fontStyle = FontStyles.Bold;
        graphStatus.rectTransform.anchorMin = new Vector2(.56f, .18f);
        graphStatus.rectTransform.anchorMax = new Vector2(.83f, .82f);
        graphStatus.rectTransform.offsetMin = graphStatus.rectTransform.offsetMax = Vector2.zero;

        Button close = CreateButton("Close", header.transform, "닫기", Hex("6B3036"));
        RectTransform closeRect = close.GetComponent<RectTransform>();
        closeRect.anchorMin = new Vector2(.85f, .18f);
        closeRect.anchorMax = new Vector2(.97f, .82f);
        closeRect.offsetMin = closeRect.offsetMax = Vector2.zero;
        close.onClick.AddListener(owner.CloseUpgradeUI);

        viewport = UiObject("Graph Viewport", safeAreaRoot, typeof(Image), typeof(RectMask2D))
            .GetComponent<RectTransform>();
        viewport.anchorMin = new Vector2(.035f, .32f);
        viewport.anchorMax = new Vector2(.965f, .855f);
        viewport.offsetMin = viewport.offsetMax = Vector2.zero;
        viewport.GetComponent<Image>().color = Hex("101620");
        AddOutline(viewport.gameObject, Hex("263143"), 1.5f);

        content = UiObject("Graph Content", viewport, typeof(Image)).GetComponent<RectTransform>();
        content.anchorMin = content.anchorMax = new Vector2(.5f, .58f);
        content.sizeDelta = new Vector2(1000, 760);
        content.GetComponent<Image>().color = Color.clear;

        TMP_Text gestureHint = CreateText("Gesture Hint", viewport, 18, TextAlignmentOptions.Center);
        gestureHint.text = "드래그로 이동  ·  두 손가락으로 확대";
        gestureHint.color = Hex("6F7C8F");
        gestureHint.rectTransform.anchorMin = new Vector2(.18f, .015f);
        gestureHint.rectTransform.anchorMax = new Vector2(.82f, .075f);
        gestureHint.rectTransform.offsetMin = gestureHint.rectTransform.offsetMax = Vector2.zero;

        GameObject detailPanel = UiObject("Node Details", safeAreaRoot, typeof(Image));
        RectTransform detailRect = detailPanel.GetComponent<RectTransform>();
        detailRect.anchorMin = new Vector2(.035f, .035f);
        detailRect.anchorMax = new Vector2(.965f, .295f);
        detailRect.offsetMin = detailRect.offsetMax = Vector2.zero;
        detailPanel.GetComponent<Image>().color = Hex("161E2A");
        AddOutline(detailPanel, Hex("344156"), 1.5f);
        details = CreateText("Details", detailPanel.transform, 22, TextAlignmentOptions.TopLeft);
        RectTransform detailsRect = details.rectTransform;
        detailsRect.anchorMin = new Vector2(.035f, .08f);
        detailsRect.anchorMax = new Vector2(.69f, .92f);
        detailsRect.offsetMin = detailsRect.offsetMax = Vector2.zero;

        purchaseButton = CreateButton("Purchase", detailPanel.transform, "업그레이드", Hex("238563"));
        RectTransform purchaseRect = purchaseButton.GetComponent<RectTransform>();
        purchaseRect.anchorMin = new Vector2(.73f, .2f);
        purchaseRect.anchorMax = new Vector2(.965f, .8f);
        purchaseRect.offsetMin = purchaseRect.offsetMax = Vector2.zero;
        purchaseButton.GetComponentInChildren<TMP_Text>().fontSize = 21;
        purchaseButton.GetComponentInChildren<TMP_Text>().fontStyle = FontStyles.Bold;
        purchaseButton.onClick.AddListener(PurchaseSelected);

        BuildConnections();
        BuildNodes();
        ApplySafeArea();
    }

    private void BuildConnections()
    {
        foreach (var child in service.Definitions)
        foreach (string parentId in child.RequiredNodeIds)
        {
            var parent = service.GetDefinition(parentId);
            if (parent == null) continue;
            Image line = UiObject(parentId + " -> " + child.Id, content, typeof(Image)).GetComponent<Image>();
            line.raycastTarget = false;
            RectTransform rect = line.rectTransform;
            Vector2 from = new Vector2(parent.X, parent.Y);
            Vector2 to = new Vector2(child.X, child.Y);
            Vector2 delta = to - from;
            rect.sizeDelta = new Vector2(delta.magnitude, 6);
            rect.anchoredPosition = (from + to) * .5f;
            rect.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg);
            connections.Add(line);
        }
    }

    private void BuildNodes()
    {
        foreach (var definition in service.Definitions)
        {
            string id = definition.Id;
            Button button = CreateButton(id, content, definition.DisplayName, Color.gray);
            TMP_Text label = button.GetComponentInChildren<TMP_Text>();
            label.fontSize = 22;
            if (TryGetNodeIcon(id, out NodeIconSpec icon))
            {
                CreateNodeIcon(button.transform, icon);
                label.alignment = TextAlignmentOptions.Left;
                label.rectTransform.anchorMin = new Vector2(.39f, 0f);
                label.rectTransform.anchorMax = new Vector2(.97f, 1f);
                label.rectTransform.offsetMin = new Vector2(0, 4);
                label.rectTransform.offsetMax = new Vector2(0, -4);
            }
            AddOutline(button.gameObject, Hex("445167"), 2f);
            var shadow = button.gameObject.AddComponent<Shadow>();
            shadow.effectColor = new Color(0, 0, 0, .38f);
            shadow.effectDistance = new Vector2(0, -4);
            RectTransform rect = button.GetComponent<RectTransform>();
            rect.anchorMin = rect.anchorMax = new Vector2(.5f, .5f);
            rect.anchoredPosition = new Vector2(definition.X, definition.Y);
            rect.sizeDelta = new Vector2(250, 104);
            button.onClick.AddListener(() => Select(id));
            nodes[id] = button;
        }
    }

    private void Refresh()
    {
        if (service == null || nodes.Count == 0) return;
        service.Synchronize();
        if (selectedId == null || service.GetState(selectedId) == UpgradeNodeState.Hidden)
            selectedId = nodes.Keys.FirstOrDefault(id => service.GetState(id) != UpgradeNodeState.Hidden);
        foreach (var pair in nodes)
        {
            UpgradeNodeState state = service.GetState(pair.Key);
            pair.Value.gameObject.SetActive(state != UpgradeNodeState.Hidden);
            pair.Value.image.color = StateColor(state);
            var progress = service.GetProgress(pair.Key);
            var definition = service.GetDefinition(pair.Key);
            pair.Value.GetComponentInChildren<TMP_Text>().text =
                $"<b>{definition.DisplayName}</b>\n" +
                $"<size=16>{StateLabel(state)}  ·  Lv.{progress.Level}/{progress.MaxLevel}</size>";
            RawImage artwork = pair.Value.GetComponentInChildren<RawImage>();
            if (artwork != null)
                artwork.color = state == UpgradeNodeState.Locked ? Hex("AAB2BE", .5f) : Color.white;
            Outline outline = pair.Value.GetComponent<Outline>();
            if (outline != null)
            {
                bool selected = pair.Key == selectedId;
                outline.effectColor = selected ? Hex("E7C46A") : StateOutlineColor(state);
                outline.effectDistance = selected ? new Vector2(3, -3) : new Vector2(1.5f, -1.5f);
            }
        }
        foreach (var line in connections)
        {
            string childId = line.name.Substring(line.name.IndexOf(" -> ") + 4);
            line.gameObject.SetActive(service.GetState(childId) != UpgradeNodeState.Hidden);
            line.color = service.GetState(childId) == UpgradeNodeState.Locked
                ? Hex("3A4658", .8f) : Hex("567BA6", .9f);
        }
        int revealedCount = nodes.Keys.Count(id => service.GetState(id) != UpgradeNodeState.Hidden);
        if (graphStatus != null)
            graphStatus.text = string.IsNullOrEmpty(discoveryMessage)
                ? $"해금 {revealedCount} / {nodes.Count}"
                : discoveryMessage;
        RefreshDetails();
    }

    private void Select(string id)
    {
        selectedId = id;
        Refresh();
    }

    private void RefreshDetails()
    {
        var node = service.GetDefinition(selectedId);
        if (node == null) return;
        var progress = service.GetProgress(selectedId);
        var state = service.GetState(selectedId);
        string requirements = node.RequiredNodeIds.Count == 0 ? "없음" : string.Join(", ",
            node.RequiredNodeIds.Select(id => service.GetDefinition(id)?.DisplayName ?? id));
        float current = BalanceTable.Effect(node.UpgradeType, progress.Level);
        float next = progress.IsMaxLevel ? current : BalanceTable.Effect(node.UpgradeType, progress.Level + 1);
        details.text = $"<size=16><color=#91A0B5>{CategoryLabel(node.Category)} · {StateLabel(state)}</color></size>\n" +
            $"<size=27><b>{node.DisplayName}</b></size>\n" +
            $"{node.Description}\n" +
            $"<color=#AEB9C8>효과</color>  <b>{current:0.##} → {next:0.##}</b>    " +
            $"<color=#AEB9C8>선행</color>  {requirements}";
        bool canPurchase = service.EvaluatePurchase(selectedId) == UpgradePurchaseStatus.Success;
        purchaseButton.interactable = canPurchase;
        purchaseButton.image.color = canPurchase ? Hex("238563") : Hex("343D49");
        purchaseButton.GetComponentInChildren<TMP_Text>().text = progress.IsMaxLevel
            ? "최대 레벨"
            : $"업그레이드\n<size=17>{NumberAbbreviator.Format(progress.Cost)}</size>";
    }

    private void PurchaseSelected()
    {
        if (string.IsNullOrEmpty(selectedId) || service.EvaluatePurchase(selectedId) != UpgradePurchaseStatus.Success)
        {
            RefreshDetails();
            return;
        }
        var before = new HashSet<string>(service.Definitions
            .Where(node => service.GetState(node.Id) != UpgradeNodeState.Hidden).Select(node => node.Id));
        UpgradeNodeDefinition definition = service.GetDefinition(selectedId);
        if (!owner.TryPurchaseUpgrade(definition.UpgradeType, out _)) return;
        service.NotifyPurchaseCommitted(selectedId);
        string[] revealed = service.Definitions.Select(node => node.Id)
            .Where(id => !before.Contains(id) && service.GetState(id) != UpgradeNodeState.Hidden)
            .ToArray();
        discoveryMessage = revealed.Length == 0 ? "" : "새 노드  " +
            string.Join(", ", revealed.Select(id => service.GetDefinition(id).DisplayName));
        Refresh();
        foreach (string id in revealed) StartCoroutine(AnimateReveal(nodes[id].transform));
        if (revealed.Length > 0) FocusNode(revealed[0]);
        if (revealed.Length > 0) StartCoroutine(ClearDiscoveryMessage());
    }

    private void FocusNode(string id)
    {
        var node = service.GetDefinition(id);
        if (node == null) return;
        content.anchoredPosition = -new Vector2(node.X, node.Y) * scale;
    }

    private static IEnumerator AnimateReveal(Transform target)
    {
        const float duration = .25f;
        float elapsed = 0f;
        target.localScale = Vector3.zero;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            target.localScale = Vector3.one * Mathf.SmoothStep(0f, 1f, t);
            yield return null;
        }
        target.localScale = Vector3.one;
    }

    private IEnumerator ClearDiscoveryMessage()
    {
        yield return new WaitForSecondsRealtime(2.2f);
        discoveryMessage = "";
        Refresh();
    }

    private static Color StateColor(UpgradeNodeState state)
    {
        switch (state)
        {
            case UpgradeNodeState.Locked: return Hex("303845");
            case UpgradeNodeState.Purchasable: return Hex("247657");
            case UpgradeNodeState.Purchased: return Hex("315D91");
            case UpgradeNodeState.Maxed: return Hex("936D28");
            default: return Color.clear;
        }
    }

    private static Color StateOutlineColor(UpgradeNodeState state)
    {
        switch (state)
        {
            case UpgradeNodeState.Locked: return Hex("4B5667");
            case UpgradeNodeState.Purchasable: return Hex("5AD3A3");
            case UpgradeNodeState.Purchased: return Hex("72A9E8");
            case UpgradeNodeState.Maxed: return Hex("E7C46A");
            default: return Color.clear;
        }
    }

    private static string StateLabel(UpgradeNodeState state)
    {
        switch (state)
        {
            case UpgradeNodeState.Locked: return "잠김";
            case UpgradeNodeState.Purchasable: return "구매 가능";
            case UpgradeNodeState.Purchased: return "성장 중";
            case UpgradeNodeState.Maxed: return "완료";
            default: return "숨김";
        }
    }

    private static string CategoryLabel(UpgradeNodeCategory category)
    {
        switch (category)
        {
            case UpgradeNodeCategory.Manual: return "직접 작업";
            case UpgradeNodeCategory.Sales: return "판매";
            case UpgradeNodeCategory.Automation: return "자동화";
            case UpgradeNodeCategory.Transport: return "운반";
            default: return category.ToString();
        }
    }

    private void ApplySafeArea()
    {
        if (safeAreaRoot == null || Screen.width <= 0 || Screen.height <= 0) return;
        Rect area = Screen.safeArea;
        safeAreaRoot.anchorMin = new Vector2(area.xMin / Screen.width, area.yMin / Screen.height);
        safeAreaRoot.anchorMax = new Vector2(area.xMax / Screen.width, area.yMax / Screen.height);
        safeAreaRoot.offsetMin = safeAreaRoot.offsetMax = Vector2.zero;
        lastSafeArea = area;
        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
    }

    private static void AddOutline(GameObject target, Color color, float distance)
    {
        var outline = target.AddComponent<Outline>();
        outline.effectColor = color;
        outline.effectDistance = new Vector2(distance, -distance);
        outline.useGraphicAlpha = true;
    }

    private static bool TryGetNodeIcon(string nodeId, out NodeIconSpec icon)
    {
        switch (nodeId)
        {
            case "manual.speed":
                icon = new NodeIconSpec("UpgradeIcons/upgrade_sheet", new Rect(0f, .5f, 1f / 3f, .5f));
                return true;
            case "sales.price":
                icon = new NodeIconSpec("UpgradeIcons/upgrade_sheet", new Rect(1f / 3f, 0f, 1f / 3f, .5f));
                return true;
            case "automation.employee":
                icon = new NodeIconSpec("UpgradeIcons/employee_hire", new Rect(0f, 0f, 1f, 1f));
                return true;
            default:
                icon = default;
                return false;
        }
    }

    private static void CreateNodeIcon(Transform parent, NodeIconSpec spec)
    {
        Texture2D texture = Resources.Load<Texture2D>(spec.ResourcePath);
        if (texture == null)
        {
            Debug.LogWarning($"Upgrade icon texture is missing: Resources/{spec.ResourcePath}");
            return;
        }

        GameObject maskObject = UiObject("Icon", parent, typeof(Image), typeof(Mask));
        RectTransform maskRect = maskObject.GetComponent<RectTransform>();
        maskRect.anchorMin = maskRect.anchorMax = new Vector2(0f, .5f);
        maskRect.pivot = new Vector2(0f, .5f);
        maskRect.anchoredPosition = new Vector2(12f, 0f);
        maskRect.sizeDelta = new Vector2(80f, 80f);
        maskObject.GetComponent<Image>().sprite = GetCircleMaskSprite();
        maskObject.GetComponent<Mask>().showMaskGraphic = false;

        RawImage artwork = UiObject("Artwork", maskObject.transform, typeof(RawImage)).GetComponent<RawImage>();
        artwork.texture = texture;
        artwork.uvRect = spec.Uv;
        artwork.raycastTarget = false;
        artwork.rectTransform.anchorMin = Vector2.zero;
        artwork.rectTransform.anchorMax = Vector2.one;
        artwork.rectTransform.offsetMin = artwork.rectTransform.offsetMax = Vector2.zero;
    }

    private static Sprite GetCircleMaskSprite()
    {
        if (circleMaskSprite != null) return circleMaskSprite;
        const int size = 64;
        var texture = new Texture2D(size, size, TextureFormat.RGBA32, false)
        {
            name = "Upgrade Icon Circle Mask",
            filterMode = FilterMode.Bilinear,
            wrapMode = TextureWrapMode.Clamp,
            hideFlags = HideFlags.HideAndDontSave
        };
        var pixels = new Color32[size * size];
        Vector2 center = new Vector2((size - 1) * .5f, (size - 1) * .5f);
        float radius = size * .5f - 1f;
        for (int y = 0; y < size; y++)
        for (int x = 0; x < size; x++)
        {
            float edge = Mathf.Clamp01(radius + 1f - Vector2.Distance(new Vector2(x, y), center));
            pixels[y * size + x] = new Color32(255, 255, 255, (byte)(edge * 255f));
        }
        texture.SetPixels32(pixels);
        texture.Apply(false, true);
        circleMaskSprite = Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(.5f, .5f), 100f);
        circleMaskSprite.name = "Upgrade Icon Circle Mask";
        circleMaskSprite.hideFlags = HideFlags.HideAndDontSave;
        return circleMaskSprite;
    }

    private static Color Hex(string rgb, float alpha = 1f)
    {
        if (!ColorUtility.TryParseHtmlString("#" + rgb, out Color color))
            color = Color.white;
        color.a = alpha;
        return color;
    }

    private static GameObject UiObject(string name, Transform parent, params System.Type[] components)
    {
        var go = new GameObject(name, typeof(RectTransform));
        foreach (var component in components) go.AddComponent(component);
        go.transform.SetParent(parent, false);
        return go;
    }

    private static TMP_Text CreateText(string name, Transform parent, float size, TextAlignmentOptions alignment)
    {
        var text = UiObject(name, parent, typeof(TextMeshProUGUI)).GetComponent<TextMeshProUGUI>();
        text.font = TMP_Settings.defaultFontAsset;
        text.fontSize = size;
        text.alignment = alignment;
        text.color = Color.white;
        text.textWrappingMode = TextWrappingModes.Normal;
        return text;
    }

    private static Button CreateButton(string name, Transform parent, string label, Color color)
    {
        var button = UiObject(name, parent, typeof(Image), typeof(Button)).GetComponent<Button>();
        button.image.color = color;
        var colors = button.colors;
        colors.highlightedColor = Color.Lerp(color, Color.white, .12f);
        colors.pressedColor = Color.Lerp(color, Color.black, .18f);
        colors.selectedColor = colors.highlightedColor;
        colors.disabledColor = Hex("343D49", .72f);
        colors.fadeDuration = .08f;
        button.colors = colors;
        TMP_Text text = CreateText("Label", button.transform, 20, TextAlignmentOptions.Center);
        text.text = label;
        text.raycastTarget = false;
        text.rectTransform.anchorMin = Vector2.zero;
        text.rectTransform.anchorMax = Vector2.one;
        text.rectTransform.offsetMin = new Vector2(8, 4);
        text.rectTransform.offsetMax = new Vector2(-8, -4);
        return button;
    }
}
