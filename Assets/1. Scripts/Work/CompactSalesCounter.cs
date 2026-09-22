using System.Collections.Generic;
using Churub.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

// Customers buy only products that the player has placed on the display.
public sealed class CompactSalesCounter : MonoBehaviour, IItemTransferEndpoint, IObjectDataSave
{
    [SerializeField] private Transform[] displaySlots;
    private ItemBuffer stock;
    private bool[] occupiedSlots;
    private readonly Dictionary<Item, int> itemSlots = new Dictionary<Item, int>();
    private CompactSupplyStation supply;
    private CompactManualStation station;
    private Player player;
    private BaseCost saveState;
    public int StockCount => stock == null ? 0 : stock.Count;

    private void Awake()
    {
        int capacity = displaySlots == null ? 0 : displaySlots.Length;
        stock = new ItemBuffer(capacity, ItemType.Churu);
        occupiedSlots = new bool[capacity];
    }

    private void Start()
    {
        supply = FindObjectOfType<CompactSupplyStation>();
        station = FindObjectOfType<CompactManualStation>();
        player = GameManager.Instance.P;
        saveState = DataManager.Instance.baseCost;
        if (supply == null || station == null || player == null || saveState == null)
        {
            Debug.LogError("Compact save participants are incomplete.", this);
            return;
        }
        RestoreState();
        DataManager.Instance.AddObjStackCountList(this);

        var legacyStoreInteraction = FindObjectOfType<StoreInteraction>();
        if (legacyStoreInteraction != null)
            legacyStoreInteraction.enabled = false;

        var spawner = FindObjectOfType<SpawnPoint>();
        var entry = GameObject.Find("Future Customer Entry");
        var queue = GameObject.Find("Customer Queue");
        var exit = GameObject.Find("Future Customer Exit");
        if (spawner != null) spawner.enabled = false;
        if (spawner == null || entry == null || queue == null || exit == null)
        {
            Debug.LogError("Compact customer route is incomplete.", this);
            return;
        }
        var sidewalk = GameObject.Find("Customer Sidewalk");
        if (sidewalk == null || sidewalk.GetComponent<MeshFilter>() == null)
        {
            Debug.LogError("Compact customer sidewalk is missing.", this);
            return;
        }
        var surface = sidewalk.GetComponent<NavMeshSurface>();
        if (surface == null) surface = sidewalk.AddComponent<NavMeshSurface>();
        surface.collectObjects = CollectObjects.Children;
        surface.layerMask = 1 << sidewalk.layer;
        surface.useGeometry = NavMeshCollectGeometry.RenderMeshes;
        surface.agentTypeID = 0;
        surface.BuildNavMesh();
        spawner.ConfigureCompactSales(this, entry.transform, queue.transform, exit.transform);
    }

    public void ObjectDataSave()
    {
        if (saveState == null || supply == null || station == null || player == null)
            return;
        var values = saveState.objectData;
        values[GameDataSchema.Objects.CompactSupplyCount] = supply.SavedStockCount;
        values[GameDataSchema.Objects.CompactInputCount] = station.SavedInputCount;
        values[GameDataSchema.Objects.CompactOutputCount] = station.OutputCount;
        values[GameDataSchema.Objects.CompactSalesCount] = StockCount;
        var carry = player.Inventory;
        values[GameDataSchema.Objects.CompactCarryCount] = carry.Count;
        values[GameDataSchema.Objects.CompactCarryType] = carry.TryPeek(out var item)
            && item != null ? (int)item.Type : -1;
    }

    private void RestoreState()
    {
        var values = saveState.objectData;
        supply.RestoreStock(ReadCount(values, GameDataSchema.Objects.CompactSupplyCount));
        station.RestoreState(ReadCount(values, GameDataSchema.Objects.CompactInputCount),
            ReadCount(values, GameDataSchema.Objects.CompactOutputCount), supply.IngredientPrefab);
        RestoreSales(ReadCount(values, GameDataSchema.Objects.CompactSalesCount));
        RestoreCarry(ReadCount(values, GameDataSchema.Objects.CompactCarryCount),
            values.TryGetValue(GameDataSchema.Objects.CompactCarryType, out int type) ? type : -1);
    }

    private static int ReadCount(Dictionary<string, int> values, string key) =>
        values.TryGetValue(key, out int count) ? Mathf.Max(0, count) : 0;

    private void RestoreSales(int count)
    {
        for (int i = 0; i < Mathf.Min(count, stock.Capacity); i++)
        {
            if (displaySlots[i] == null) break;
            var spawned = PoolingManager.Instance.GetObj(station.ProductPrefab);
            if (spawned != null && spawned.TryGetComponent<Item>(out var item)
                && stock.TryAdd(item))
            {
                PlaceItem(item, i);
                continue;
            }
            PoolingManager.Instance.ReturnObjecte(spawned);
            Debug.LogError("Cannot restore compact sales stock.", this);
            break;
        }
    }

    private void RestoreCarry(int count, int type)
    {
        GameObject prefab = type == (int)ItemType.Ingredient ? supply.IngredientPrefab :
            type == (int)ItemType.Churu ? station.ProductPrefab : null;
        if (prefab == null) return;
        var carry = player.Inventory;
        for (int i = 0; i < Mathf.Min(count, carry.Capacity); i++)
        {
            var spawned = PoolingManager.Instance.GetObj(prefab);
            if (spawned != null && spawned.TryGetComponent<Item>(out var item)
                && ItemTransferUtility.TryCollect(item, carry, player.CarryParent, animate: false))
                continue;
            PoolingManager.Instance.ReturnObjecte(spawned);
            Debug.LogError("Cannot restore compact carried items.", this);
            break;
        }
    }

    public bool TrySellOne()
    {
        if (stock == null || !stock.TryPop(out var item) || item == null)
            return false;

        if (itemSlots.TryGetValue(item, out int slot))
        {
            occupiedSlots[slot] = false;
            itemSlots.Remove(item);
        }
        PoolingManager.Instance.ReturnObjecte(item.gameObject);
        UIManager.Instance.AddGold(Mathf.RoundToInt(BalanceTable.StallIncome));
        return true;
    }

    public bool TryTransfer(CarrierInventory inventory, Transform carryParent)
    {
        if (inventory == null || stock == null || stock.IsFull)
            return false;

        int slot = -1;
        for (int i = 0; i < occupiedSlots.Length; i++)
        {
            if (!occupiedSlots[i] && displaySlots[i] != null)
            {
                slot = i;
                break;
            }
        }
        if (slot < 0 || !inventory.TryMoveTo(stock, out var item))
            return false;

        PlaceItem(item, slot);
        return true;
    }

    private void PlaceItem(Item item, int slot)
    {
        occupiedSlots[slot] = true;
        itemSlots[item] = slot;
        item.transform.DOKill();
        if (item.TryGetComponent<Rigidbody>(out var body)) Destroy(body);
        item.transform.SetParent(displaySlots[slot], false);
        float bottomOffset = item.TryGetComponent<BoxCollider>(out var box)
            ? box.size.y * .5f - box.center.y : 0f;
        item.transform.localPosition = Vector3.up * bottomOffset;
        item.transform.localRotation = Quaternion.identity;
        item.transform.localScale = Vector3.one;
    }
}
