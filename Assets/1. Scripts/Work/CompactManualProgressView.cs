using UnityEngine;
using UnityEngine.UI;

// World-space work indicator that follows the player's head without blocking input.
public sealed class CompactManualProgressView : MonoBehaviour
{
    private const float CanvasScale = .009f;
    private const float IndicatorDiameter = 98f;
    private const float ClearanceAboveHead = .25f;
    private static Sprite circleSprite;
    private Canvas canvas;
    private Image fill;
    private CharacterController controller;
    private Animator animator;
    private Transform head;
    private SkinnedMeshRenderer bodyRenderer;
    private Camera viewCamera;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        bodyRenderer = GetComponentInChildren<SkinnedMeshRenderer>(true);
        if (animator != null && animator.isHuman)
            head = animator.GetBoneTransform(HumanBodyBones.Head);

        var root = new GameObject("Manual Work Progress", typeof(RectTransform), typeof(Canvas));
        root.transform.SetParent(transform, false);
        canvas = root.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.overrideSorting = true;
        canvas.sortingOrder = 100;
        RectTransform rect = (RectTransform)root.transform;
        rect.sizeDelta = new Vector2(100f, 100f);
        rect.localScale = Vector3.one * CanvasScale;

        CreateCircle("Shadow", rect, 98f, new Color(0f, 0f, 0f, .25f));
        CreateCircle("Background", rect, 90f, new Color(.12f, .2f, .22f, .92f));
        CreateCircle("Ring Track", rect, 78f, new Color(1f, 1f, 1f, .22f));
        fill = CreateCircle("Work Progress", rect, 78f, new Color(1f, .55f, .2f, 1f));
        fill.type = Image.Type.Filled;
        fill.fillMethod = Image.FillMethod.Radial360;
        fill.fillOrigin = (int)Image.Origin360.Top;
        fill.fillClockwise = true;
        fill.fillAmount = 0f;
        CreateCircle("Icon Backing", rect, 58f, new Color(.12f, .2f, .22f, 1f));

        Color white = new Color(1f, .96f, .88f, 1f);
        CreateBar("Package Outer", rect, new Vector2(28f, 24f), Vector2.zero, white);
        CreateBar("Package Inner", rect, new Vector2(23f, 19f), Vector2.zero,
            new Color(.12f, .2f, .22f, 1f));
        CreateBar("Package Seam", rect, new Vector2(3f, 19f), Vector2.zero, white);
        CreateBar("Package Top", rect, new Vector2(28f, 4f), new Vector2(0f, 10f), white);
        root.SetActive(false);
    }

    public void SetProgress(float progress)
    {
        if (canvas == null) return;
        fill.fillAmount = Mathf.Clamp01(progress);
        if (!canvas.gameObject.activeSelf)
            canvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (canvas != null)
            canvas.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (canvas == null || !canvas.gameObject.activeSelf) return;
        if (viewCamera == null) viewCamera = Camera.main;
        float headTop = controller != null
            ? transform.TransformPoint(controller.center + Vector3.up * (controller.height * .5f)).y
            : transform.position.y + 1.2f;
        if (bodyRenderer != null && bodyRenderer.enabled)
            headTop = Mathf.Max(headTop, bodyRenderer.bounds.max.y);
        Vector3 anchor = head != null ? head.position : transform.position;
        float offset = IndicatorDiameter * CanvasScale * .5f + ClearanceAboveHead;
        canvas.transform.position = new Vector3(anchor.x, headTop + offset, anchor.z);
        if (viewCamera != null)
            canvas.transform.rotation = viewCamera.transform.rotation;
    }

    private static Image CreateCircle(string name, RectTransform parent, float size, Color color)
    {
        Image image = CreateBar(name, parent, new Vector2(size, size), Vector2.zero, color);
        image.sprite = CircleSprite;
        return image;
    }

    private static Image CreateBar(string name, RectTransform parent, Vector2 size,
        Vector2 position, Color color)
    {
        var obj = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        RectTransform rect = (RectTransform)obj.transform;
        rect.SetParent(parent, false);
        rect.sizeDelta = size;
        rect.anchoredPosition = position;
        Image image = obj.GetComponent<Image>();
        image.color = color;
        image.raycastTarget = false;
        return image;
    }

    private static Sprite CircleSprite
    {
        get
        {
            if (circleSprite != null) return circleSprite;
            const int size = 128;
            var texture = new Texture2D(size, size, TextureFormat.RGBA32, false);
            var pixels = new Color32[size * size];
            for (int y = 0; y < size; y++)
                for (int x = 0; x < size; x++)
                {
                    float dx = x + .5f - size * .5f;
                    float dy = y + .5f - size * .5f;
                    byte alpha = (byte)(Mathf.Clamp01(size * .5f - Mathf.Sqrt(dx * dx + dy * dy)) * 255f);
                    pixels[y * size + x] = new Color32(255, 255, 255, alpha);
                }
            texture.SetPixels32(pixels);
            texture.Apply(false, true);
            texture.hideFlags = HideFlags.HideAndDontSave;
            circleSprite = Sprite.Create(texture, new Rect(0f, 0f, size, size),
                new Vector2(.5f, .5f), 100f);
            circleSprite.hideFlags = HideFlags.HideAndDontSave;
            return circleSprite;
        }
    }
}
