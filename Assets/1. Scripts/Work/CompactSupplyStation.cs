using System.Collections;
using Churub.Core;
using DG.Tweening;
using UnityEngine;

// Visible stage-zero source for the compact container's short feed tray.
public sealed class CompactSupplyStation : MonoBehaviour, IItemTransferEndpoint
{
    private const int MaxStock = 50;
    private const int ItemsPerLayer = 3;
    private const float TraySlotSpacing = .54f;
    private const float TrayApproachDistance = .48f;

    [SerializeField] private GameObject ingredientPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform pickupTray;
    [SerializeField] private float feedSpeed = 1.3f;
    [SerializeField] private float traySettleSeconds = .35f;
    private readonly ItemBuffer stock = new ItemBuffer(MaxStock, ItemType.Ingredient);
    private bool pendingSpawn;
    public int StockCount => stock.Count;
    public int SavedStockCount => stock.Count + (pendingSpawn ? 1 : 0);
    public int StockCapacity => stock.Capacity;
    public GameObject IngredientPrefab => ingredientPrefab;

    public void RestoreStock(int count)
    {
        for (int i = 0; i < Mathf.Clamp(count, 0, stock.Capacity); i++)
        {
            var spawned = PoolingManager.Instance.GetObj(ingredientPrefab);
            int slot = stock.Count;
            if (spawned != null && spawned.TryGetComponent<Item>(out var item)
                && TryStore(item, slot))
                continue;
            PoolingManager.Instance.ReturnObjecte(spawned);
            Debug.LogError("Cannot restore compact supply stock.", this);
            break;
        }
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(BalanceTable.IngredientInterval);
            if (stock.IsFull || ingredientPrefab == null || spawnPoint == null || pickupTray == null)
                continue;

            var spawned = PoolingManager.Instance.GetObj(ingredientPrefab);
            if (spawned == null || !spawned.TryGetComponent<Item>(out var item)
                || !spawned.TryGetComponent<BoxCollider>(out var box))
            {
                PoolingManager.Instance.ReturnObjecte(spawned);
                continue;
            }
            if (spawned.TryGetComponent<Rigidbody>(out var body))
            {
                body.isKinematic = true;
                Destroy(body);
            }
            spawned.transform.DOKill();
            spawned.transform.position = spawnPoint.position;
            spawned.transform.rotation = spawnPoint.rotation;
            spawned.transform.localScale = Vector3.one;
            spawned.transform.SetParent(transform, true);
            box.enabled = false;

            Vector3 approachDirection = spawnPoint.position - pickupTray.position;
            approachDirection.y = 0f;
            if (approachDirection.sqrMagnitude < .001f) approachDirection = Vector3.forward;
            Vector3 beltExit = pickupTray.position +
                approachDirection.normalized * TrayApproachDistance;
            beltExit.y = spawnPoint.position.y;
            float beltSeconds = Vector3.Distance(spawnPoint.position, beltExit) /
                Mathf.Max(.01f, feedSpeed);

            pendingSpawn = true;
            yield return spawned.transform.DOMove(beltExit, beltSeconds)
                .SetEase(Ease.Linear).WaitForCompletion();

            // Reserve the current top slot only after the belt trip. A player may
            // remove stock while this item is travelling, so a spawn-time slot is stale.
            int slot = stock.Count;
            if (!stock.TryAdd(item))
            {
                pendingSpawn = false;
                PoolingManager.Instance.ReturnObjecte(spawned);
                continue;
            }
            pendingSpawn = false;
            box.enabled = true;
            PlaceOnTray(item, slot, true);
            CompactTelemetryEvents.Record(CompactTelemetryMetric.IngredientProduced);
        }
    }

    private bool TryStore(Item item, int slot)
    {
        if (item == null || pickupTray == null || !stock.TryAdd(item)) return false;
        if (item.TryGetComponent<Rigidbody>(out var body)) Destroy(body);
        if (item.TryGetComponent<BoxCollider>(out var box)) box.enabled = true;
        PlaceOnTray(item, slot, false);
        return true;
    }

    private void PlaceOnTray(Item item, int slot, bool animate)
    {
        var target = item.transform;
        target.DOKill();
        GetTrayLocalPose(slot, item.GetComponent<BoxCollider>(),
            out var localPosition, out var localRotation);
        target.SetParent(pickupTray, animate);
        target.localScale = Vector3.one;
        if (animate)
        {
            float duration = Mathf.Max(.01f, traySettleSeconds);
            target.DOLocalMove(localPosition, duration).SetEase(Ease.OutQuad);
            target.DOLocalRotateQuaternion(localRotation, duration).SetEase(Ease.OutQuad);
        }
        else
        {
            target.localPosition = localPosition;
            target.localRotation = localRotation;
        }
    }

    private static void GetTrayLocalPose(int slot, BoxCollider box,
        out Vector3 position, out Quaternion rotation)
    {
        int positionInLayer = slot % ItemsPerLayer;
        int layer = slot / ItemsPerLayer;
        float x = positionInLayer == 0 ? 0f :
            (positionInLayer == 1 ? -TraySlotSpacing : TraySlotSpacing);
        float bottomOffset = box != null ? box.size.y * .5f - box.center.y : 0f;
        float layerHeight = box != null ? box.size.y : .14f;
        position = new Vector3(x, bottomOffset + layer * layerHeight, 0f);
        rotation = Quaternion.Euler(0f, 90f, 0f);
    }

    public bool TryTransfer(CarrierInventory inventory, Transform carryParent) =>
        ItemTransferUtility.TryMove(stock, inventory, carryParent);
}
