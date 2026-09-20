using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class UnlockProgressView : MonoBehaviour
{
    private const string PadResourcePath = "UI/Unlock/Unlock_Construction_Pad";
    private const string PawResourcePath = "UI/Unlock/Unlock_Paw_Active";

    private static readonly Vector2[] PawAnchors =
    {
        new Vector2(0.50f, 0.84f),
        new Vector2(0.76f, 0.75f),
        new Vector2(0.86f, 0.50f),
        new Vector2(0.76f, 0.24f),
        new Vector2(0.50f, 0.15f),
        new Vector2(0.24f, 0.24f),
        new Vector2(0.14f, 0.50f),
        new Vector2(0.24f, 0.75f)
    };

    private static readonly float[] PawRotations = { 0f, -45f, -90f, -135f, 180f, 135f, 90f, 45f };

    private readonly Image[] pawImages = new Image[PawAnchors.Length];
    private TMP_Text captionText;
    private TMP_Text amountText;
    private RectTransform progressFill;
    private int activePawCount;
    private string displayedCaption;
    private string displayedAmount;
    private float displayedProgress = -1f;
    private bool displayedLockState;
    private bool hasDisplayedLockState;
    private bool isReady;

    public bool Initialize(Image legacyFill)
    {
        if (isReady) return true;
        if (legacyFill == null) return false;

        Sprite padSprite = Resources.Load<Sprite>(PadResourcePath);
        Sprite pawSprite = Resources.Load<Sprite>(PawResourcePath);
        if (padSprite == null || pawSprite == null)
        {
            Debug.LogWarning("Unlock progress sprites are missing. The legacy fill display will be used.", this);
            return false;
        }

        RectTransform legacyRect = legacyFill.rectTransform;
        RectTransform root = CreateRect("Unlock Progress", legacyRect.parent);
        CopyRect(legacyRect, root);

        Image pad = CreateImage("Construction Pad", root, padSprite);
        Stretch(pad.rectTransform);

        for (int i = 0; i < pawImages.Length; i++)
        {
            Image paw = CreateImage("Paw " + (i + 1), root, pawSprite);
            RectTransform pawRect = paw.rectTransform;
            pawRect.anchorMin = PawAnchors[i];
            pawRect.anchorMax = PawAnchors[i];
            pawRect.anchoredPosition = Vector2.zero;
            pawRect.sizeDelta = new Vector2(0.52f, 0.52f);
            pawRect.localRotation = Quaternion.Euler(0f, 0f, PawRotations[i]);
            paw.color = new Color(1f, 1f, 1f, 0f);
            pawImages[i] = paw;
        }

        TMP_FontAsset font = ResolveFont();
        captionText = CreateText("Remaining Caption", root, font, 0.18f);
        SetRect(captionText.rectTransform, new Vector2(0.5f, 0.64f), new Vector2(1.7f, 0.3f));

        amountText = CreateText("Remaining Amount", root, font, 0.42f);
        SetRect(amountText.rectTransform, new Vector2(0.5f, 0.53f), new Vector2(1.8f, 0.55f));

        Image progressBackground = CreateSolidImage("Progress Background", root, new Color(0.24f, 0.12f, 0.07f, 0.28f));
        SetRect(progressBackground.rectTransform, new Vector2(0.5f, 0.38f), new Vector2(1.45f, 0.12f));

        Image fill = CreateSolidImage("Progress Fill", progressBackground.rectTransform, new Color(0.91f, 0.35f, 0.26f, 1f));
        progressFill = fill.rectTransform;
        progressFill.anchorMin = Vector2.zero;
        progressFill.anchorMax = new Vector2(0f, 1f);
        progressFill.pivot = new Vector2(0f, 0.5f);
        progressFill.offsetMin = Vector2.zero;
        progressFill.offsetMax = Vector2.zero;

        legacyFill.gameObject.SetActive(false);
        isReady = true;
        SetProgress(0f, false);
        return true;
    }

    public void SetProgress(float progress, bool animate = true)
    {
        if (!isReady) return;

        int nextCount = progress <= 0f
            ? 0
            : Mathf.Clamp(Mathf.CeilToInt(Mathf.Clamp01(progress) * pawImages.Length), 0, pawImages.Length);

        for (int i = 0; i < pawImages.Length; i++)
        {
            bool shouldBeVisible = i < nextCount;
            Image paw = pawImages[i];
            paw.DOKill();
            paw.rectTransform.DOKill();

            if (!shouldBeVisible)
            {
                paw.color = new Color(1f, 1f, 1f, 0f);
                paw.rectTransform.localScale = Vector3.one;
                continue;
            }

            if (animate && i >= activePawCount)
            {
                paw.color = new Color(1f, 1f, 1f, 0f);
                paw.rectTransform.localScale = Vector3.one * 0.55f;
                paw.DOFade(1f, 0.15f);
                paw.rectTransform.DOScale(Vector3.one, 0.22f).SetEase(Ease.OutBack);
            }
            else
            {
                paw.color = Color.white;
                paw.rectTransform.localScale = Vector3.one;
            }
        }

        activePawCount = nextCount;
    }

    public void SetInvestment(int investedAmount, int totalAmount, string lockReason)
    {
        if (!isReady) return;

        int safeTotal = Mathf.Max(1, totalAmount);
        int safeInvested = Mathf.Clamp(investedAmount, 0, safeTotal);
        int remaining = safeTotal - safeInvested;
        bool isLocked = !string.IsNullOrEmpty(lockReason);
        string nextCaption = isLocked ? "해금 조건" : "남은 금액";
        string nextAmount = isLocked ? lockReason : remaining.ToString("N0");

        if (displayedCaption != nextCaption)
        {
            captionText.text = nextCaption;
            displayedCaption = nextCaption;
        }

        if (displayedAmount != nextAmount)
        {
            amountText.text = nextAmount;
            displayedAmount = nextAmount;
        }

        if (!hasDisplayedLockState || displayedLockState != isLocked)
        {
            amountText.fontSizeMax = isLocked ? 0.23f : 0.42f;
            amountText.fontSizeMin = isLocked ? 0.11f : 0.2f;
            displayedLockState = isLocked;
            hasDisplayedLockState = true;
        }

        float progress = (float)safeInvested / safeTotal;
        if (!Mathf.Approximately(displayedProgress, progress))
        {
            progressFill.anchorMax = new Vector2(progress, 1f);
            displayedProgress = progress;
        }
    }

    private static RectTransform CreateRect(string objectName, Transform parent)
    {
        GameObject child = new GameObject(objectName, typeof(RectTransform));
        child.layer = parent.gameObject.layer;
        RectTransform rect = child.GetComponent<RectTransform>();
        rect.SetParent(parent, false);
        return rect;
    }

    private static Image CreateImage(string objectName, Transform parent, Sprite sprite)
    {
        GameObject child = new GameObject(objectName, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        child.layer = parent.gameObject.layer;
        RectTransform rect = child.GetComponent<RectTransform>();
        rect.SetParent(parent, false);

        Image image = child.GetComponent<Image>();
        image.sprite = sprite;
        image.preserveAspect = true;
        image.raycastTarget = false;
        return image;
    }

    private static Image CreateSolidImage(string objectName, Transform parent, Color color)
    {
        GameObject child = new GameObject(objectName, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        child.layer = parent.gameObject.layer;
        RectTransform rect = child.GetComponent<RectTransform>();
        rect.SetParent(parent, false);

        Image image = child.GetComponent<Image>();
        image.color = color;
        image.raycastTarget = false;
        return image;
    }

    private static TMP_Text CreateText(string objectName, Transform parent, TMP_FontAsset font, float maximumSize)
    {
        GameObject child = new GameObject(objectName, typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
        child.layer = parent.gameObject.layer;
        RectTransform rect = child.GetComponent<RectTransform>();
        rect.SetParent(parent, false);

        TextMeshProUGUI text = child.GetComponent<TextMeshProUGUI>();
        text.font = font;
        text.color = new Color(0.25f, 0.12f, 0.07f, 1f);
        text.alignment = TextAlignmentOptions.Center;
        text.fontStyle = FontStyles.Bold;
        text.enableAutoSizing = true;
        text.fontSizeMin = 0.1f;
        text.fontSizeMax = maximumSize;
        text.textWrappingMode = TextWrappingModes.NoWrap;
        text.overflowMode = TextOverflowModes.Ellipsis;
        text.raycastTarget = false;
        return text;
    }

    private static TMP_FontAsset ResolveFont()
    {
        foreach (TMP_FontAsset font in Resources.FindObjectsOfTypeAll<TMP_FontAsset>())
            if (font != null && font.name == "Typo_DodamM SDF") return font;

        return TMP_Settings.defaultFontAsset;
    }

    private static void CopyRect(RectTransform source, RectTransform target)
    {
        target.anchorMin = source.anchorMin;
        target.anchorMax = source.anchorMax;
        target.anchoredPosition = source.anchoredPosition;
        target.sizeDelta = source.sizeDelta;
        target.pivot = source.pivot;
        target.localRotation = source.localRotation;
        target.localScale = source.localScale;
    }

    private static void Stretch(RectTransform rect)
    {
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    private static void SetRect(RectTransform rect, Vector2 anchor, Vector2 size)
    {
        rect.anchorMin = anchor;
        rect.anchorMax = anchor;
        rect.anchoredPosition = Vector2.zero;
        rect.sizeDelta = size;
        rect.pivot = new Vector2(0.5f, 0.5f);
    }
}
