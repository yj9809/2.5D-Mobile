using System.Collections;
using Churub.Core;
using DG.Tweening;
using UnityEngine;

// Visible stage-zero source for the compact container's short feed tray.
public sealed class CompactSupplyStation : MonoBehaviour, IItemTransferEndpoint
{
    [SerializeField] private GameObject ingredientPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform pickupTray;
    private readonly ItemBuffer stock = new ItemBuffer(6, ItemType.Ingredient);
    private bool pendingSpawn;
    public int StockCount => stock.Count;
    public int SavedStockCount => stock.Count + (pendingSpawn ? 1 : 0);
    public GameObject IngredientPrefab => ingredientPrefab;

    public void RestoreStock(int count)
    {
        for (int i = 0; i < Mathf.Clamp(count, 0, stock.Capacity); i++)
        {
            var spawned = PoolingManager.Instance.GetObj(ingredientPrefab);
            if (spawned != null && spawned.TryGetComponent<Item>(out var item)
                && ItemTransferUtility.TryCollect(item, stock, pickupTray, animate: false))
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
            spawned.transform.SetParent(pickupTray, true);
            box.enabled = false;
            float height = box.size.y * stock.Count;
            pendingSpawn = true;
            spawned.transform.DOMove(pickupTray.position + Vector3.up * height, .8f)
                .SetEase(Ease.Linear);
            yield return new WaitForSeconds(.8f);
            pendingSpawn = false;
            if (!stock.TryAdd(item))
            {
                PoolingManager.Instance.ReturnObjecte(spawned);
                continue;
            }
            box.enabled = true;
        }
    }

    public bool TryTransfer(CarrierInventory inventory, Transform carryParent) =>
        ItemTransferUtility.TryMove(stock, inventory, carryParent);
}
