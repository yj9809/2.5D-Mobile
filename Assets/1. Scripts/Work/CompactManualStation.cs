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
    private GameObject operatorActor;
    private Player animatedPlayer;
    private Employee animatedEmployee;
    private CompactManualProgressView progressView;
    private bool workVisualsActive;
    public int InputCount => input.Count;
    public int InputCapacity => input.Capacity;
    public int OutputCount => output.Count;
    public int OutputCapacity => output.Capacity;
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
        SetOperator(player != null ? player.gameObject : null, present);
    }

    public void SetOperator(Employee employee, bool present)
    {
        SetOperator(employee != null ? employee.gameObject : null, present);
    }

    private void SetOperator(GameObject actor, bool present)
    {
        if (present)
        {
            if (actor == null) return;
            if (operatorActor != null && operatorActor != actor)
            {
                bool incomingPlayer = actor.GetComponent<Player>() != null;
                bool currentPlayer = operatorActor.GetComponent<Player>() != null;
                if (!incomingPlayer || currentPlayer) return;
                StopWorkVisuals();
            }
            operatorActor = actor;
            StartProcessingIfNeeded();
        }
        else if (operatorActor == actor)
        {
            StopWorkVisuals();
            operatorActor = null;
        }
    }

    private void StartProcessingIfNeeded()
    {
        if (operatorActor != null && processing == null && !input.IsEmpty)
            processing = StartCoroutine(Process());
    }

    private void SetWorkVisuals(float progress)
    {
        if (operatorActor == null) return;
        Player player = operatorActor.GetComponent<Player>();
        Employee employee = operatorActor.GetComponent<Employee>();
        if (!workVisualsActive || animatedPlayer != player || animatedEmployee != employee)
        {
            StopWorkVisuals();
            animatedPlayer = player;
            animatedEmployee = employee;
            Vector3 workPosition = inputTray != null && outputTray != null
                ? (inputTray.position + outputTray.position) * .5f
                : transform.position;
            if (animatedPlayer != null)
                animatedPlayer.DoManualProcessingAnimation(workPosition);
            else if (animatedEmployee != null)
                animatedEmployee.DoManualProcessingAnimation(workPosition);
            progressView = operatorActor.GetComponent<CompactManualProgressView>();
            if (progressView == null)
                progressView = operatorActor.AddComponent<CompactManualProgressView>();
            progressView.SetIcon(workIcon);
            workVisualsActive = true;
        }
        progressView.SetProgress(progress);
    }

    private void StopWorkVisuals()
    {
        if (animatedPlayer != null)
            animatedPlayer.StopBoxPackagingAnimationPlayer();
        if (animatedEmployee != null)
            animatedEmployee.StopBoxPackagingAnimationEmployee();
        if (progressView != null)
            progressView.Hide();
        animatedPlayer = null;
        animatedEmployee = null;
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
            while (remaining > 0f || operatorActor == null || output.IsFull)
            {
                bool canWork = operatorActor != null && !output.IsFull;
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
            CompactTelemetryEvents.Record(CompactTelemetryMetric.ProductProcessed);
            if (operatorActor != null && operatorActor.GetComponent<Player>() != null)
                CompactProgressEvents.Raise(CompactProgressAction.ProductCompleted);
        }
        StopWorkVisuals();
        processing = null;
    }

    private void OnDisable()
    {
        StopWorkVisuals();
        operatorActor = null;
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
    public CompactManualStation Station => station;
    public bool TryTransfer(CarrierInventory inventory, Transform carryParent) =>
        station != null && station.TryCollect(inventory, carryParent);
}
