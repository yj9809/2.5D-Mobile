using System.Collections;
using DG.Tweening;
using UnityEngine;

// Processes one visible ingredient into one visible packaged product.
public sealed class CompactManualStation : MonoBehaviour, IItemTransferEndpoint
{
    [SerializeField] private GameObject productPrefab;
    [SerializeField] private Sprite workIcon;
    [SerializeField] private Transform inputTray;
    [SerializeField] private Transform outputTray;
    [SerializeField] private float processingSeconds = 2.5f;
    private readonly ItemBuffer input = new ItemBuffer(100, ItemType.Ingredient);
    private readonly ItemBuffer output = new ItemBuffer(100, ItemType.Churu);
    private Coroutine processing;
    private Player operatorPlayer;
    private Player animatedPlayer;
    private CompactManualProgressView progressView;
    private bool workVisualsActive;
    public int InputCount => input.Count;
    public int OutputCount => output.Count;
    public int SavedInputCount => input.Count;
    public GameObject ProductPrefab => productPrefab;

    public void RestoreState(int inputCount, int outputCount, GameObject ingredientPrefab)
    {
        for (int i = 0; i < Mathf.Min(outputCount, output.Capacity); i++)
            if (!RestoreItem(productPrefab, output, outputTray, true)) break;
        for (int i = 0; i < Mathf.Min(inputCount, input.Capacity); i++)
            if (!RestoreItem(ingredientPrefab, input, inputTray, false)) break;
    }

    private bool RestoreItem(GameObject prefab, ItemBuffer destination, Transform tray,
        bool isOutput)
    {
        var spawned = PoolingManager.Instance.GetObj(prefab);
        int slot = destination.Count;
        if (spawned != null && spawned.TryGetComponent<Item>(out var item)
            && ItemTransferUtility.TryCollect(item, destination, tray, animate: false))
        {
            if (isOutput) PlaceOutput(item, slot, false);
            return true;
        }
        PoolingManager.Instance.ReturnObjecte(spawned);
        Debug.LogError("Cannot restore compact processing item.", this);
        return false;
    }

    public bool TryTransfer(CarrierInventory inventory, Transform carryParent)
    {
        if (inputTray == null || productPrefab == null
            || !ItemTransferUtility.TryMove(inventory, input, inputTray))
            return false;
        StartProcessingIfNeeded();
        return true;
    }

    public void SetOperator(Player player, bool present)
    {
        if (present)
        {
            if (player == null) return;
            if (operatorPlayer != player) StopWorkVisuals();
            operatorPlayer = player;
            StartProcessingIfNeeded();
        }
        else if (operatorPlayer == player)
        {
            StopWorkVisuals();
            operatorPlayer = null;
        }
    }

    private void StartProcessingIfNeeded()
    {
        if (operatorPlayer != null && processing == null && !input.IsEmpty)
            processing = StartCoroutine(Process());
    }

    private void SetWorkVisuals(float progress)
    {
        if (operatorPlayer == null) return;
        if (!workVisualsActive || animatedPlayer != operatorPlayer)
        {
            StopWorkVisuals();
            animatedPlayer = operatorPlayer;
            Vector3 workPosition = inputTray != null && outputTray != null
                ? (inputTray.position + outputTray.position) * .5f
                : transform.position;
            animatedPlayer.DoManualProcessingAnimation(workPosition);
            progressView = animatedPlayer.GetComponent<CompactManualProgressView>();
            if (progressView == null)
                progressView = animatedPlayer.gameObject.AddComponent<CompactManualProgressView>();
            progressView.SetIcon(workIcon);
            workVisualsActive = true;
        }
        progressView.SetProgress(progress);
    }

    private void StopWorkVisuals()
    {
        if (animatedPlayer != null)
            animatedPlayer.StopBoxPackagingAnimationPlayer();
        if (progressView != null)
            progressView.Hide();
        animatedPlayer = null;
        workVisualsActive = false;
    }

    public bool TryCollect(CarrierInventory inventory, Transform carryParent) =>
        ItemTransferUtility.TryMove(output, inventory, carryParent);

    private IEnumerator Process()
    {
        while (!input.IsEmpty)
        {
            float duration = Mathf.Max(.01f, processingSeconds);
            float remaining = duration;
            while (remaining > 0f || operatorPlayer == null || output.IsFull)
            {
                bool canWork = operatorPlayer != null && !output.IsFull;
                if (canWork)
                    SetWorkVisuals(1f - Mathf.Clamp01(remaining / duration));
                else
                    StopWorkVisuals();
                if (canWork && remaining > 0f)
                    remaining -= Time.deltaTime;
                yield return null;
            }
            SetWorkVisuals(1f);

            var product = PoolingManager.Instance.GetObj(productPrefab);
            if (product == null || !product.TryGetComponent<Item>(out var item)
                || outputTray == null)
            {
                PoolingManager.Instance.ReturnObjecte(product);
                Debug.LogError("Cannot create compact processed product.", this);
                break;
            }
            if (!input.TryPop(out var ingredient) || ingredient == null)
            {
                PoolingManager.Instance.ReturnObjecte(product);
                break;
            }
            PoolingManager.Instance.ReturnObjecte(ingredient.gameObject);
            int slot = output.Count;
            if (!output.TryAdd(item))
            {
                PoolingManager.Instance.ReturnObjecte(product);
                break;
            }
            PlaceOutput(item, slot, true);
        }
        StopWorkVisuals();
        processing = null;
    }

    private void OnDisable()
    {
        StopWorkVisuals();
        operatorPlayer = null;
        if (processing != null) StopCoroutine(processing);
        processing = null;
    }

    private void PlaceOutput(Item item, int slot, bool animate)
    {
        if (item.TryGetComponent<Rigidbody>(out var body)) Destroy(body);
        var target = item.transform;
        target.DOKill();
        target.SetParent(outputTray, false);
        float bottomOffset = item.TryGetComponent<BoxCollider>(out var box)
            ? box.size.y * .5f - box.center.y : 0f;
        int positionInLayer = slot % 6;
        int layer = slot / 6;
        float layerHeight = box != null ? box.size.y : .08f;
        target.localPosition = new Vector3((positionInLayer % 3 - 1) * .23f,
            bottomOffset + layer * layerHeight,
            (positionInLayer / 3 == 0 ? -.16f : .16f));
        target.localRotation = Quaternion.identity;
        target.localScale = animate ? Vector3.zero : Vector3.one;
        if (animate) target.DOScale(Vector3.one, .45f).SetEase(Ease.OutBack);
    }
}

public sealed class CompactManualOutput : MonoBehaviour, IItemTransferEndpoint
{
    [SerializeField] private CompactManualStation station;
    public bool TryTransfer(CarrierInventory inventory, Transform carryParent) =>
        station != null && station.TryCollect(inventory, carryParent);
}
