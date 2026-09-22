using System.Collections;
using DG.Tweening;
using UnityEngine;

// Processes one visible ingredient into one visible packaged product.
public sealed class CompactManualStation : MonoBehaviour, IItemTransferEndpoint
{
    [SerializeField] private GameObject productPrefab;
    [SerializeField] private Transform inputTray;
    [SerializeField] private Transform outputTray;
    [SerializeField] private float processingSeconds = 2.5f;
    private readonly ItemBuffer input = new ItemBuffer(1, ItemType.Ingredient);
    private readonly ItemBuffer output = new ItemBuffer(1, ItemType.Churu);
    private Coroutine processing;
    public int InputCount => input.Count;
    public int OutputCount => output.Count;
    public int SavedInputCount => input.Count +
        (processing != null && input.IsEmpty && output.IsEmpty ? 1 : 0);
    public GameObject ProductPrefab => productPrefab;

    public void RestoreState(int inputCount, int outputCount, GameObject ingredientPrefab)
    {
        if (outputCount > 0 && RestoreItem(productPrefab, output, outputTray))
            return;
        if (inputCount > 0 && RestoreItem(ingredientPrefab, input, inputTray))
            processing = StartCoroutine(Process());
    }

    private bool RestoreItem(GameObject prefab, ItemBuffer destination, Transform tray)
    {
        var spawned = PoolingManager.Instance.GetObj(prefab);
        if (spawned != null && spawned.TryGetComponent<Item>(out var item)
            && ItemTransferUtility.TryCollect(item, destination, tray, animate: false))
            return true;
        PoolingManager.Instance.ReturnObjecte(spawned);
        Debug.LogError("Cannot restore compact processing item.", this);
        return false;
    }

    public bool TryTransfer(CarrierInventory inventory, Transform carryParent)
    {
        if (processing != null || !output.IsEmpty || inputTray == null || productPrefab == null
            || !ItemTransferUtility.TryMove(inventory, input, inputTray))
            return false;
        processing = StartCoroutine(Process());
        return true;
    }

    public bool TryCollect(CarrierInventory inventory, Transform carryParent) =>
        ItemTransferUtility.TryMove(output, inventory, carryParent);

    private IEnumerator Process()
    {
        yield return new WaitForSeconds(processingSeconds);
        if (!input.TryPop(out var ingredient) || ingredient == null)
        {
            processing = null;
            yield break;
        }
        PoolingManager.Instance.ReturnObjecte(ingredient.gameObject);

        var product = PoolingManager.Instance.GetObj(productPrefab);
        if (product == null || !product.TryGetComponent<Item>(out var item) || outputTray == null)
        {
            PoolingManager.Instance.ReturnObjecte(product);
            processing = null;
            yield break;
        }
        if (product.TryGetComponent<Rigidbody>(out var body)) Destroy(body);
        product.transform.DOKill();
        product.transform.SetParent(outputTray, false);
        product.transform.localPosition = Vector3.zero;
        product.transform.localRotation = Quaternion.identity;
        product.transform.localScale = Vector3.zero;
        product.transform.DOScale(Vector3.one, .45f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(.45f);
        if (!output.TryAdd(item))
            PoolingManager.Instance.ReturnObjecte(product);
        processing = null;
    }
}

public sealed class CompactManualOutput : MonoBehaviour, IItemTransferEndpoint
{
    [SerializeField] private CompactManualStation station;
    public bool TryTransfer(CarrierInventory inventory, Transform carryParent) =>
        station != null && station.TryCollect(inventory, carryParent);
}
